using System;

namespace TcxEditor.Core.Entities
{
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
}
