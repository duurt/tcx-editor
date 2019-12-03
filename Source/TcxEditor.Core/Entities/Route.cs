using System.Collections.Generic;

namespace TcxEditor.Core.Entities
{
    public class Route
    {
        public List<TrackPoint> TrackPoints { get; } 
            = new List<TrackPoint>();
        public List<CoursePoint> CoursePoints { get; } 
            = new List<CoursePoint>();
    }
}
