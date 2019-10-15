using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;


// todo: unit tests..? Eventually the presenter will hold the state, and the actual GUI is 
// just showing stuff. I think a selfshunt test class implementing the view interface(s) 
// will be a nice way.
namespace TcxEditor.UI
{
    public class Presenter
    {
        private readonly IRouteView _routeView;
        private readonly IErrorView _errorView;
        private readonly IGuiStateSetter _guiControls;
        private readonly ICommandRunner _commandRunner;
        private string _openedRoutePath;

        private Route _route = null;
        private DateTime _selectedTimeStamp;

        public Presenter(
            IRouteView routeView,
            IErrorView errorView,
            IGuiStateSetter guiControls,
            ICommandRunner commandRunner
            )
        {
            _routeView = routeView;
            _errorView = errorView;
            _guiControls = guiControls;
            _commandRunner = commandRunner;

            HookupEvents();
            InitializeGuiState();
        }

        private void HookupEvents()
        {
            _routeView.OpenFileEvent += OnOpenFileEvent;
            _routeView.AddStartFinishEvent += OnAddStartFinishEvent;
            _routeView.SaveRouteEvent += OnSaveRouteEvent;
            _routeView.GetNearestEvent += OnGetNearestEvent;
            _routeView.AddPointEvent += OnAddPointEvent;
            _routeView.DeletePointEvent += OnDeletePointEvent;
            _routeView.SelectCoursePointEvent += OnSelectCoursePointEvent;
            _routeView.StepEvent += OnStepEvent;
        }

        private void InitializeGuiState()
        {
            _guiControls.Apply(new GuiState
            {
                SaveEnabled = false,
                AddCoursePoint = false,
                DeleteCoursePoint = false,
                ScrollRoute = false
            });
        }

        private void OnSelectCoursePointEvent(object sender, SelectPointEventArgs e)
        {
            _selectedTimeStamp = e.TimeStamp;
            _routeView.ShowEditCoursePointMarker(
                _route.CoursePoints.First(
                    p => p.TimeStamp == e.TimeStamp));

            _guiControls.Apply(new GuiState
            {
                SaveEnabled = true,
                AddCoursePoint = false,
                DeleteCoursePoint = true,
                ScrollRoute = true
            });
        }

        private void OnDeletePointEvent(object sender, EventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new DeleteCoursePointInput
                    {
                        Route = _route,
                        TimeStamp = _selectedTimeStamp
                    }) as DeleteCoursePointResponse;

                _route = result.Route;

                _routeView.ShowRoute(result.Route);
                _routeView.ShowPointToEdit(
                    result.Route.TrackPoints.First(
                        p => p.TimeStamp == _selectedTimeStamp));

                _guiControls.Apply(new GuiState
                {
                    SaveEnabled = true,
                    AddCoursePoint = true,
                    DeleteCoursePoint = false,
                    ScrollRoute = true
                });
            });
        }

        private void OnAddPointEvent(object sender, AddPointEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new AddCoursePointInput
                    {
                        Route = _route,
                        NewCoursePoint = new CoursePoint(
                            GetTrackPoint(_selectedTimeStamp).Lattitude, GetTrackPoint(_selectedTimeStamp).Longitude)
                        {
                            Name = e.Name,
                            Notes = e.Notes,
                            TimeStamp = _selectedTimeStamp,
                            Type = e.PointType
                        }
                    }) as AddCoursePointResponse;
                _route = result.Route;

                _routeView.ShowRoute(result.Route);
                _routeView.ShowEditCoursePointMarker(
                    result.Route.TrackPoints.First(
                        p => p.TimeStamp == _selectedTimeStamp));

                _guiControls.Apply(new GuiState
                {
                    SaveEnabled = true,
                    AddCoursePoint = false,
                    DeleteCoursePoint = true,
                    ScrollRoute = true
                });
            });
        }

        private void OnGetNearestEvent(object sender, GetNearestEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new GetNearestTrackPointInput
                    {
                        Route = _route,
                        ReferencePoint = e.ReferencePoint
                    }) as GetNearestTrackPointResponse;
                _route = result.Route;
                _selectedTimeStamp = result.Nearest.TimeStamp;

                _routeView.ShowRoute(result.Route);
                UpdateMarkerInView(_selectedTimeStamp);
            });
        }

        private void UpdateMarkerInView(DateTime timeStamp)
        {
            bool hasCoursePoint = CoursePointExists(timeStamp);

            if (hasCoursePoint)
                _routeView.ShowEditCoursePointMarker(GetTrackPoint(timeStamp));
            else
                _routeView.ShowPointToEdit(GetTrackPoint(timeStamp));

            _guiControls.Apply(new GuiState
            {
                SaveEnabled = true,
                AddCoursePoint = !hasCoursePoint,
                DeleteCoursePoint = hasCoursePoint,
                ScrollRoute = true
            });
        }

        private TrackPoint GetTrackPoint(DateTime timeStamp)
        {
            return _route.TrackPoints.First(p => p.TimeStamp == timeStamp);
        }

        private bool CoursePointExists(DateTime t)
        {
            return _route.CoursePoints.Any(p => p.TimeStamp == t);
        }

        private void OnSaveRouteEvent(object sender, SaveRouteEventargs e)
        {
            // todo: check that route is not null? 
            // How about explcitily working with states..?
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new SaveRouteInput(_route, _openedRoutePath, e.DestinationPath))
                        as SaveRouteResponse;
                _route = result.Route;

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnAddStartFinishEvent(object sender, EventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(new AddStartFinishInput(_route))
                    as AddStartFinishResponse;
                _route = result.Route;
                // todo: should we update, or should the form NOT remove the markers every time?
                UpdateMarkerInView(_selectedTimeStamp);
                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnOpenFileEvent(object sender, OpenRouteEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(new OpenRouteInput { Name = e.Name })
                    as OpenRouteResponse;
                _openedRoutePath = e.Name;
                _route = result.Route;

                _routeView.ShowRoute(result.Route);

                _guiControls.Apply(new GuiState
                {
                    SaveEnabled = true,
                    AddCoursePoint = false,
                    DeleteCoursePoint = false,
                    ScrollRoute = false
                });
            });
        }

        private void TryCatch(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (TcxCoreException ex)
            {
                _errorView.ShowErrorMessage(ex.Message);
            }
        }

        private void OnStepEvent(object sender, StepEventArgs e)
        {
            int step = e.Step;

            int maxIndex = _route.TrackPoints.Count - 1;

            int index = _route.TrackPoints.FindIndex(p => p.TimeStamp == _selectedTimeStamp);
            int nextIndex = index + step;

            if (nextIndex < 0 || nextIndex > maxIndex)
                return;

            _selectedTimeStamp = _route.TrackPoints[nextIndex].TimeStamp;
            UpdateMarkerInView(_selectedTimeStamp);
        }
    }
}
