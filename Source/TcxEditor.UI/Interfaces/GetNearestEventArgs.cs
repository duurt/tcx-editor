using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class GetNearestEventArgs
    {
        public Route Route { get; internal set; }
        public Position ReferencePoint { get; internal set; }
    }
}