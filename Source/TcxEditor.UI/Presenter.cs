using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    public class Presenter
    {
        private readonly IRouteView _routeView;
        private readonly IErrorView _errorView;
        private readonly IOpenRouteCommand _opener;
        private readonly IAddStartFinishCommand _startFinishAdder;
        private readonly ISaveRouteCommand _saver;
        private readonly IGetNearestTrackPointCommand _nearestFinder;
        private readonly IAddCoursePointCommand _pointAdder;
        private readonly IDeleteCoursePointCommand _pointDeleter;

        public Presenter(
            IRouteView routeView,
            IErrorView errorView,
            IOpenRouteCommand opener,
            IAddStartFinishCommand startFinishAdder,
            ISaveRouteCommand saver,
            IGetNearestTrackPointCommand nearestFinder,
            IAddCoursePointCommand pointAdder,
            IDeleteCoursePointCommand pointDeleter)
        {
            _routeView = routeView;
            _errorView = errorView;

            _opener = opener;
            _startFinishAdder = startFinishAdder;
            _saver = saver;
            _nearestFinder = nearestFinder;
            _pointAdder = pointAdder;
            _pointDeleter = pointDeleter;

            _routeView.OpenFileEvent += OnOpenFileEvent;
            _routeView.AddStartFinishEvent += OnAddStartFinishEvent;
            _routeView.SaveRouteEvent += OnSaveRouteEvent;
            _routeView.GetNearestEvent += OnGetNearestEvent;
            _routeView.AddPointEvent += OnAddPointEvent;
            _routeView.DeletePointEvent += OnDeletePointEvent;
        }

        private void OnDeletePointEvent(object sender, DeletePointEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _pointDeleter.Execute(
                    new DeleteCoursePointInput
                    {
                        Route = e.Route,
                        TimeStamp = e.Route.CoursePoints.FirstOrDefault(
                            p => p.Lattitude == e.Position.Lattitude
                            && p.Longitude == e.Position.Longitude).TimeStamp
                    });

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnAddPointEvent(object sender, AddPointEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _pointAdder.Execute(
                    new AddCoursePointInput
                    {
                        Route = e.Route,
                        NewCoursePoint = e.NewPoint
                    });

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnGetNearestEvent(object sender, GetNearestEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _nearestFinder.Execute(
                    new GetNearestTrackPointInput
                    {
                        Route = e.Route,
                        ReferencePoint = e.ReferencePoint
                    });

                _routeView.ShowRoute(result.Route);
                _routeView.ShowPointToEdit(result.Nearest);
            });
        }

        private void OnSaveRouteEvent(object sender, SaveRouteEventargs e)
        {
            TryCatch(() =>
            {
                var result = _saver.Execute(new SaveRouteRequest(e.Route, e.Name));

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnAddStartFinishEvent(object sender, AddStartFinishEventargs e)
        {
            TryCatch(() =>
            {
                var result = _startFinishAdder.Execute(new AddStartFinishInput(e.Route));

                _routeView.ShowRoute(result.Route);
            });
        }

        private void OnOpenFileEvent(object sender, OpenRouteEventArgs e)
        {
            TryCatch(() =>
            {
                var result = _opener.Execute(new OpenRouteInput { Name = e.Name });
                _routeView.ShowRoute(result.Route);
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
