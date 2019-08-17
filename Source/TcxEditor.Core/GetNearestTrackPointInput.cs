using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointInput
    {
        public Route Route { get; set; }
        public Position ReferencePoint { get; set; }
    }
}