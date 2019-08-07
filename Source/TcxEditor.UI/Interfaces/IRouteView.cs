using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public interface IRouteView
    {
        void ShowRoute(Route route);
        event EventHandler<OpenRouteEventArgs> OpenFileEvent;
    }
}