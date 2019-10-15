using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
