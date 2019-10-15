using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddStartFinishResponse : IOutput
    {
        public Route Route { get; set; }

        public AddStartFinishResponse(Route route)
        {
            Route = route;
        }
    }
}