using Duurt.TcxParser.Xsd.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxParser.Infrastructure
{
    public class RouteSaver : IRouteSaver
    {
        public void SaveCoursePoints(List<CoursePoint> points, string name)
        {
            using (var routeFile = File.OpenRead(name))
            {
                var parsedFile = new XmlParser().Parse(routeFile);

                parsedFile.Courses[0].CoursePoint
                    = points.Select(p => new CoursePoint_t
                    {
                        Name = p.Name,
                        PointType = Map(p.Type),
                        Time = p.TimeStamp,
                        Position = new Position_t
                        {
                            LatitudeDegrees = p.Lattitude,
                            LongitudeDegrees = p.Longitude
                        },
                        Notes = p.Notes
                    }).ToArray();

                string outFilePath = @"c:\tmp\routes\Route_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".tcx";
                using (var outFile = new FileStream(outFilePath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    new XmlSerializer(typeof(TrainingCenterDatabase_t)).Serialize(outFile, parsedFile);
                }
            }
        }

        private CoursePointType_t Map(CoursePoint.PointType type)
        {
            switch (type)
            {
                case CoursePoint.PointType.Generic: return CoursePointType_t.Generic;
                case CoursePoint.PointType.Left: return CoursePointType_t.Left;
                case CoursePoint.PointType.Right: return CoursePointType_t.Right;
                case CoursePoint.PointType.Straight: return CoursePointType_t.Straight;
                case CoursePoint.PointType.Food: return CoursePointType_t.Food;
                case CoursePoint.PointType.Summit: return CoursePointType_t.Summit;
                case CoursePoint.PointType.Valley: return CoursePointType_t.Valley;
                case CoursePoint.PointType.ClimbCat1: return CoursePointType_t.Item1stCategory;
                case CoursePoint.PointType.ClimbCat2: return CoursePointType_t.Item2ndCategory;
                case CoursePoint.PointType.ClimbCat3: return CoursePointType_t.Item3rdCategory;
                case CoursePoint.PointType.ClimbCat4: return CoursePointType_t.Item4thCategory;
                case CoursePoint.PointType.ClimbCatHors: return CoursePointType_t.HorsCategory;
                case CoursePoint.PointType.Danger: return CoursePointType_t.Danger;
                case CoursePoint.PointType.Water: return CoursePointType_t.Water;
                case CoursePoint.PointType.Sprint: return CoursePointType_t.Sprint;
                case CoursePoint.PointType.FirstAid: return CoursePointType_t.FirstAid;

                default:
                    throw new ArgumentOutOfRangeException($"unkown type {type.ToString()}");
            }
            
        }
    }
}
