using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointResponse
    {
        // todo: should all reponse properties have internal setters?
        public Route Route { get; set; }
        public TrackPoint Nearest { get; set; }
    }
}