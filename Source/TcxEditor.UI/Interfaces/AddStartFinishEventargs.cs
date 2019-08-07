using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class AddStartFinishEventargs : EventArgs
    {
        public Route Route { get; }

        public AddStartFinishEventargs(Route route)
        {
            Route = route;
        }
    }
}