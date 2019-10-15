using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointInput : IInput
    {
        public Route Route { get; set; }
        public Position ReferencePoint { get; set; }
    }
}