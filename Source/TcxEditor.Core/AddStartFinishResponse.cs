using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class AddStartFinishResponse
    {
        public Route Route { get; set; }

        public AddStartFinishResponse(Route route)
        {
            Route = route;
        }
    }
}