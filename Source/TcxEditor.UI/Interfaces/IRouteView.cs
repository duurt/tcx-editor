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
        void ShowPointToEdit(TrackPoint nearest);

        event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        event EventHandler<AddStartFinishEventargs> AddStartFinishEvent;
        event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        event EventHandler<GetNearestEventArgs> GetNearestEvent;
        event EventHandler<AddPointEventArgs> AddPointEvent;
    }
}