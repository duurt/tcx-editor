﻿using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;

namespace TcxEditor.Core.Tests
{
    public class AddCoursePointCommandTests
    {
        private AddCoursePointCommand _sut;

        [SetUp]
        public void Setup_Sut()
        {
            _sut = new AddCoursePointCommand();
        }

        [Test]
        public void Execute_should_throw_error_with_null_input()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(null));
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(new AddCoursePointInput { Route = null }));
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(
                    new AddCoursePointInput
                    {
                        Route = new Route(),
                        NewCoursePoint = null
                    }));
        }

        [Test]
        public void Execute_Should_throw_error_when_there_is_no_matching_track_point()
        {
            double lat1 = 1;
            double lon1 = 2;
            DateTime t1 = new DateTime(2019, 8, 16, 12, 45, 59);

            var inputRoute = GetRouteWithOneTrackPoint(lat1, lon1, t1);

            ExecuteShouldThrowError(inputRoute, GetCoursePoint(lat1 + 1, lon1, t1));
            ExecuteShouldThrowError(inputRoute, GetCoursePoint(lat1, lon1 + 1, t1));
            ExecuteShouldThrowError(inputRoute, GetCoursePoint(lat1, lon1, t1.AddSeconds(1)));
        }

        [Test]
        public void Execute_Should_add_point()
        {
            double lat1 = 1;
            double lon1 = 2;
            DateTime t1 = new DateTime(2019, 8, 16, 12, 45, 59);

            var inputRoute = GetRouteWithOneTrackPoint(lat1, lon1, t1);
            var result =
                _sut.Execute(
                new AddCoursePointInput
                {
                    Route = inputRoute,
                    NewCoursePoint = GetCoursePoint(lat1, lon1, t1)
                });

            result.Route.CoursePoints.Count.ShouldBe(1);
            CoursePoint coursePoint = result.Route.CoursePoints.First();
            VerifyCoursePoint(coursePoint, lat1, lon1, t1);
        }

        [Test]
        public void Execute_Should_insert_point_at_time_order()
        {
            double lat1 = 1;
            double lon1 = 2;
            DateTime t1 = new DateTime(2019, 8, 16, 12, 45, 59);

            double lat0 = lat1 - 0.1;
            double lon0 = lon1 - 0.1;
            DateTime t0 = t1.AddSeconds(-1);

            double lat2 = lat1 + 0.1;
            double lon2 = lat1 + 0.1;
            DateTime t2 = t1.AddSeconds(1);


            var inputRoute = new Route();
            inputRoute.TrackPoints.Add(GetTrackPoint(lat0, lon0, t0));
            inputRoute.TrackPoints.Add(GetTrackPoint(lat1, lon1, t1));
            inputRoute.TrackPoints.Add(GetTrackPoint(lat2, lon2, t2));

            inputRoute.CoursePoints.Add(GetCoursePoint(lat0, lon0, t0));
            inputRoute.CoursePoints.Add(GetCoursePoint(lat2, lon2, t2));

            var result =
                _sut.Execute(
                new AddCoursePointInput
                {
                    Route = inputRoute,
                    NewCoursePoint = GetCoursePoint(lat1, lon1, t1)
                });

            result.Route.CoursePoints.Count.ShouldBe(3);
            result.Route.CoursePoints[0].TimeStamp.ShouldBe(t0);
            result.Route.CoursePoints[1].TimeStamp.ShouldBe(t1);
            result.Route.CoursePoints[2].TimeStamp.ShouldBe(t2);
        }

        private static void VerifyCoursePoint(CoursePoint coursePoint, double lat1, double lon1, DateTime t1)
        {
            coursePoint.Lattitude.ShouldBe(lat1);
            coursePoint.Longitude.ShouldBe(lon1);
            coursePoint.TimeStamp.ShouldBe(t1);
        }

        private void ExecuteShouldThrowError(Route inputRoute, CoursePoint inputCoursePoint)
        {
            Assert.Throws<TcxCoreException>(
                () => _sut.Execute(
                    new AddCoursePointInput
                    {
                        Route = inputRoute,
                        NewCoursePoint = inputCoursePoint
                    }));
        }

        private static CoursePoint GetCoursePoint(double lat1, double lon1, DateTime t1)
        {
            return new CoursePoint(lat1, lon1)
            {
                TimeStamp = t1,
                Name = "some name",
                Notes = "some notes",
                Type = CoursePoint.PointType.Food
            };
        }

        private static Route GetRouteWithOneTrackPoint(double lat1, double lon1, DateTime t1)
        {
            var trackPoint1 =
                GetTrackPoint(lat1, lon1, t1);
            var inputRoute = new Route();
            inputRoute.TrackPoints.Add(trackPoint1);
            return inputRoute;
        }

        private static TrackPoint GetTrackPoint(double lat, double lon, DateTime t)
        {
            return new TrackPoint(lat, lon)
            {
                TimeStamp = t,
            };
        }
    }
}
