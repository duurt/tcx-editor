using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddStartFinishInput : IInput
    {
        public Route Route { get; }

        public AddStartFinishInput(Route route)
        {
            Route = route;
        }
    }
}
