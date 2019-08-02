using System;
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

    public class Position
    {
        public double Lattitude { get; private set; }
        public double Longitude { get; private set; }

        public Position(double lat, double lon)
        {
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException($"{nameof(lat)}: {lat}");
            if (lon < -180 || lon > 180)
                throw new ArgumentOutOfRangeException($"{nameof(lon)}: {lon}");

            Lattitude = lat;
            Longitude = lon;
        }
    }

    public class TrackPoint : Position
    {
        public TrackPoint(double lat, double lon)
            : base(lat, lon) { }
        
        public DateTime TimeStamp { get; set; }
    }

    public class CoursePoint : TrackPoint
    {
        public CoursePoint(double lat, double lon)
            : base(lat, lon) { }

        public PointType Type { get; set; }

        public enum PointType
        {
            Generic = 1,
            Summit,
            Valley,
            Water,
            Food,
            Danger,
            Left,
            Right,
            Straight,
            FirstAid,
            ClimbCat4,
            ClimbCat3,
            ClimbCat2,
            ClimbCat1,
            ClimbCatHors,
            Sprint,
        }
    }
}
