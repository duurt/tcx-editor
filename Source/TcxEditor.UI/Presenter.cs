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

        public Presenter(
            IRouteView routeView, 
            IOpenRouteCommand opener,
            IAddStartFinishCommand startFinishAdder) 
        {
            _routeView = routeView;
            _opener = opener;
            _startFinishAdder = startFinishAdder;

            _routeView.OpenFileEvent += OnOpenFileEvent;
            _routeView.AddStartFinishEvent += OnAddStartFinishEvent;
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
