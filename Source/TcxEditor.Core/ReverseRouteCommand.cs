using System;
using System.Collections.Generic;
using System.Linq;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    // todo: do we need the 'intermediate' interfaces? (here: IReverseRouteCommand)
    public class ReverseRouteCommand : IReverseRouteCommand
    {
        public ReverseRouteResponse Execute(ReverseRouteInput input)
        {
            ReversePoints(input.Route.TrackPoints);
            ReversePoints(input.Route.CoursePoints);
            TransformCoursePoints(input.Route);

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

        private void TransformCoursePoints(Route route)
        {
            route.CoursePoints.ForEach(Map);
        }

        private void Map(CoursePoint cp)
        {
            cp.Type = Map(cp.Type);
            cp.Name = Map(cp.Name);
            cp.Notes = Map(cp.Notes);
        }

        private static CoursePoint.PointType Map(CoursePoint.PointType input)
        {
            switch (input)
            {
                case CoursePoint.PointType.Left: return CoursePoint.PointType.Right;
                case CoursePoint.PointType.Right: return CoursePoint.PointType.Left;
                
                default: return input;
            }
        }

        private string Map(string input)
        {
            switch (input)
            {
                case "Right": return "Left";
                case "Left": return "Right";

                default: return input;
            }
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
