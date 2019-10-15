using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointResponse : IOutput
    {
        // todo: should all reponse properties have internal setters?
        public Route Route { get; set; }
        public TrackPoint Nearest { get; set; }
    }
}