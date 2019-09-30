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

namespace TcxEditor.UI
{
    public class Presenter
    {
        private readonly IRouteView _routeView;
        private readonly IErrorView _errorView;
        private readonly IGuiStateSetter _guiControls;
        private readonly ICommandRunner _commandRunner;

        private Route _route = null;
        // todo: this must be trackpoint (includes timestamp: needed to distinghuish between overlapping parts of route)
        // todo: same for all event args that pass positions...
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
            _routeView.SelectTrackPointEvent += OnSelectTrackPointEvent;
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

        private void OnSelectTrackPointEvent(object sender, SelectPointEventArgs e)
        {
            _selectedTimeStamp = e.TimeStamp;
            _routeView.ShowEditTrackPointMarker(
                _route.TrackPoints.First(
                    p => p.TimeStamp == e.TimeStamp));

            _guiControls.Apply(new GuiState
            {
                SaveEnabled = true,
                AddCoursePoint = true,
                DeleteCoursePoint = false,
                ScrollRoute = true
            });
        }

        private void OnSelectCoursePointEvent(object sender, SelectPointEventArgs e)
        {
            _selectedTimeStamp = e.TimeStamp;
            _routeView.ShowEditCoursePointMarker(_route.CoursePoints.First(
                    p => p.TimeStamp == e.TimeStamp));

            _guiControls.Apply(new GuiState
            {
                SaveEnabled = true,
                AddCoursePoint = false,
                DeleteCoursePoint = true,
                ScrollRoute = true
            });
        }

        private void OnDeletePointEvent(object sender, DeletePointEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new DeleteCoursePointInput
                    {
                        Route = e.Route,
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

        //private DateTime GetTimeStampSelectedCoursePoint(DeletePointEventArgs e)
        //{
        //    return e.Route.CoursePoints.FirstOrDefault(
        //        p => p.TimeStamp == _selectedTimeStamp).TimeStamp;
        //}

        private void OnAddPointEvent(object sender, AddPointEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(
                    new AddCoursePointInput
                    {
                        Route = e.Route,
                        NewCoursePoint = e.NewPoint
                    }) as AddCoursePointResponse;
                _route = result.Route;
                _selectedTimeStamp = e.NewPoint.TimeStamp;

                _routeView.ShowRoute(result.Route);
                _routeView.ShowEditCoursePointMarker(
                    result.Route.TrackPoints.First(
                        p => p.TimeStamp == e.NewPoint.TimeStamp));

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
                        Route = e.Route,
                        ReferencePoint = e.ReferencePoint
                    }) as GetNearestTrackPointResponse;
                _route = result.Route;

                _routeView.ShowRoute(result.Route);
                _routeView.ShowPointToEdit(result.Nearest);

                _guiControls.Apply(new GuiState
                {
                    SaveEnabled = true,
                    AddCoursePoint = true,
                    // todo: make smarter: only if it is coursepoint
                    DeleteCoursePoint = false,
                    ScrollRoute = true
                });
            });
        }

        private void OnSaveRouteEvent(object sender, SaveRouteEventargs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(new SaveRouteInput(e.Route, e.Name))
                    as SaveRouteResponse;
                _route = result.Route;

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnAddStartFinishEvent(object sender, AddStartFinishEventargs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(new AddStartFinishInput(e.Route))
                    as AddStartFinishResponse;
                _route = result.Route;
                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnOpenFileEvent(object sender, OpenRouteEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _commandRunner.Execute(new OpenRouteInput { Name = e.Name })
                    as OpenRouteResponse;
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
    }
}
