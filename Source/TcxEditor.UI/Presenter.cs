using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    public class Presenter
    {
        private readonly IRouteView _routeView;
        private readonly IOpenRouteCommand _opener;
        private readonly IAddStartFinishCommand _startFinishAdder;
        private readonly ISaveRouteCommand _saver;
        private readonly IGetNearestTrackPointCommand _nearestFinder;

        public Presenter(
            IRouteView routeView, 
            IOpenRouteCommand opener,
            IAddStartFinishCommand startFinishAdder,
            ISaveRouteCommand saver,
            IGetNearestTrackPointCommand nearestFinder) 
        {
            _routeView = routeView;
            _opener = opener;
            _startFinishAdder = startFinishAdder;
            _saver = saver;
            _nearestFinder = nearestFinder;

            _routeView.OpenFileEvent += OnOpenFileEvent;
            _routeView.AddStartFinishEvent += OnAddStartFinishEvent;
            _routeView.SaveRouteEvent += OnSaveRouteEvent;
            _routeView.GetNearestEvent += OnGetNearestEvent;
        }

        private void OnGetNearestEvent(object sender, GetNearestEventArgs e)
        {
            var result = _nearestFinder.Execute(
                new GetNearestTrackPointInput
                {
                    Route = e.Route,
                    ReferencePoint = e.ReferencePoint
                });

            _routeView.ShowRoute(result.Route);
            _routeView.ShowPointToEdit(result.Nearest);
        }

        private void OnSaveRouteEvent(object sender, SaveRouteEventargs e)
        {
            var result = _saver.Execute(new SaveRouteRequest(e.Route, e.Name));

            _routeView.ShowRoute(result.Route);
        }

        private void OnAddStartFinishEvent(object sender, AddStartFinishEventargs e)
        {
            var result = _startFinishAdder.Execute(new AddStartFinishInput(e.Route));

            _routeView.ShowRoute(result.Route);
        }

        private void OnOpenFileEvent(object sender, OpenRouteEventArgs e)
        {
            var result = _opener.Execute(new OpenRouteInput { Name = e.Name });

            _routeView.ShowRoute(result.Route);
        }
    }
}
