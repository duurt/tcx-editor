using Duurt.TcxParser.Xsd.Generated;
using System;
using System.IO;
using System.Linq;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

// todo: write unit tests
namespace TcxEditor.Parser.Infrastructure
{
    public class TcxParserAdapter : ITcxParser
    {
        public Route ParseTcx(Stream input)
        {
            var parsedRoute = new XmlParser().Parse(input);

            var result = new Route();
            result.TrackPoints.AddRange(
                parsedRoute.Courses[0].Track.Select(x => Map(x)));

            result.CoursePoints.AddRange(
                parsedRoute.Courses[0].CoursePoint?.Select(x => Map(x))
                ?? new CoursePoint[0]);

            return result;
        }

        private TrackPoint Map(Trackpoint_t x)
        {
            return new TrackPoint(x.Position.LatitudeDegrees, x.Position.LongitudeDegrees)
            {
                TimeStamp = x.Time
            };
        }

        private static CoursePoint Map(Duurt.TcxParser.Xsd.Generated.CoursePoint_t x)
        {
            return new CoursePoint(
                x.Position.LatitudeDegrees,
                x.Position.LongitudeDegrees)
            {
                TimeStamp = x.Time,
                Type = Map(x.PointType),
                Notes = x.Notes
            };
        }

        private static CoursePoint.PointType Map(CoursePointType_t pointType)
        {
            switch (pointType)
            {
                case CoursePointType_t.Generic: return CoursePoint.PointType.Generic;
                case CoursePointType_t.Left: return CoursePoint.PointType.Left;
                case CoursePointType_t.Right: return CoursePoint.PointType.Right;
                case CoursePointType_t.Straight: return CoursePoint.PointType.Straight;
                case CoursePointType_t.Food: return CoursePoint.PointType.Food;
                case CoursePointType_t.Summit: return CoursePoint.PointType.Summit;
                case CoursePointType_t.Valley: return CoursePoint.PointType.Valley;
                case CoursePointType_t.Water: return CoursePoint.PointType.Water;
                case CoursePointType_t.FirstAid: return CoursePoint.PointType.FirstAid;
                case CoursePointType_t.Danger: return CoursePoint.PointType.Danger;
                case CoursePointType_t.Item1stCategory: return CoursePoint.PointType.ClimbCat1;
                case CoursePointType_t.Item2ndCategory: return CoursePoint.PointType.ClimbCat2;
                case CoursePointType_t.Item3rdCategory: return CoursePoint.PointType.ClimbCat3;
                case CoursePointType_t.Item4thCategory: return CoursePoint.PointType.ClimbCat4;
                case CoursePointType_t.HorsCategory: return CoursePoint.PointType.ClimbCatHors;

                case CoursePointType_t.SlightRight: return CoursePoint.PointType.SlightRight;
                case CoursePointType_t.SlightLeft: return CoursePoint.PointType.SlightLeft;

                default: throw new ArgumentOutOfRangeException(nameof(pointType), pointType.ToString());
            }
        }
    }
}
