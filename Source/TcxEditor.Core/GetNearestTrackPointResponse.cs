using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class GetNearestTrackPointResponse
    {
        // todo: should all reponse properties have internal setters?
        public TrackPoint Nearest { get; set; }
    }
}