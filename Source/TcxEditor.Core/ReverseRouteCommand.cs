using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class ReverseRouteCommand : ITcxEditorCommand<ReverseRouteInput, ReverseRouteResponse>
    {
        public ReverseRouteResponse Execute(ReverseRouteInput input)
        {
            ReversePoints(input.Route.TrackPoints);
            ReversePoints(input.Route.CoursePoints);

            return new ReverseRouteResponse(input.Route);
        }

        private static void ReversePoints<T>(List<T> points)
            where T : TrackPoint
        {
            DateTime[] tpTimeStamps = ExtractTimeStamps(points);
            points.Reverse();
            SetTimeStamps(tpTimeStamps, points);
        }

        private static DateTime[] ExtractTimeStamps<T>(List<T> points)
            where T : TrackPoint
        {
            return points.Select(tp => tp.TimeStamp).ToArray();
        }

        private static void SetTimeStamps<T>(DateTime[] tpTimeStamps, List<T> points)
            where T : TrackPoint
        {
            for (int i = 0; i < tpTimeStamps.Length; i++)
                points[i].TimeStamp = tpTimeStamps[i];
        }
    }

    public class ReverseRouteInput : IInput
    {
        public Route Route { get; set; }

        public ReverseRouteInput(Route route) => Route = route;
    }

    public class ReverseRouteResponse : IOutput
    {
        public Route Route { get; set; }
        public ReverseRouteResponse(Route route) => Route = route;

    }
}
