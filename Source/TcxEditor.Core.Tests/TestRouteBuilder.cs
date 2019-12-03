using System;
using System.Linq;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Tests
{
    public class TestRouteBuilder
    {
        private readonly Route _route = new Route();

        public TestRouteBuilder WithTrackPointCount(int n)
        {
            if (_route.TrackPoints.Any())
                throw new InvalidOperationException("TrackPoints are already set. You cannot set them more than once.");

            _route.TrackPoints.AddRange(
                Enumerable.Range(0, n).Select(i => GetTrackPoint(i)));

            return this;
        }

        public TestRouteBuilder WithCoursePointsAt(params int[] coursePointIndices)
        {
            if (!_route.TrackPoints.Any())
                throw new InvalidOperationException("TrackPoints are not yet set. You must set them before you can add course points.");

            if (_route.CoursePoints.Any())
                throw new InvalidOperationException("CoursePoints are already set. You cannot set them more than once.");

            _route.CoursePoints.AddRange(coursePointIndices.Select(i => GetCoursePoint(i)));

            return this;
        }

        public static TrackPoint GetTrackPoint(int i)
        {
            return
                new TrackPoint(GetLat(i), GetLon(i))
                {
                    TimeStamp = GetTimeStamp(i)
                };
        }

        public static CoursePoint GetCoursePoint(int i)
        {
            return
                new CoursePoint(GetLat(i), GetLon(i))
                {
                    TimeStamp = GetTimeStamp(i),
                    Name = GetName(i),
                    Notes = GetNotes(i),
                    Type = GetType(i)
                };
        }

        private static CoursePoint.PointType GetType(int i)
        {
            return (CoursePoint.PointType)i;
        }

        private static string GetNotes(int i)
        {
            return $"notes {i}";
        }

        private static string GetName(int i)
        {
            return $"name {i}";
        }

        public static double GetLat(int i) => 10 + 0.1 * i;

        public static double GetLon(int i) => 20 + 0.1 * i;

        public static DateTime GetTimeStamp(int i) => new DateTime(2019, 8, 21, 12, 0, 0).AddSeconds(i);

        public Route Build()
        {
            return _route;
        }
    }
}
