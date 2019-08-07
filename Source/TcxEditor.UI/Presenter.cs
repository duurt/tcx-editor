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

        public Presenter(IRouteView routeView, IOpenRouteCommand opener) 
        {
            _routeView = routeView;
            _opener = opener;

            _routeView.OpenFileEvent += OnOpenFileEvent;
        }

        private void OnOpenFileEvent(object sender, OpenRouteEventArgs e)
        {
            var result = _opener.Execute(new OpenRouteInput { Name = e.Name });

            _routeView.ShowRoute(result.Route);
        }
    }
}
