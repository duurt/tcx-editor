using System;

namespace TcxEditor.Core.Entities
{
    public class TrackPoint : Position
    {
        public DateTime TimeStamp { get; set; }

        public TrackPoint(double lat, double lon)
            : base(lat, lon) { }
    }
}
