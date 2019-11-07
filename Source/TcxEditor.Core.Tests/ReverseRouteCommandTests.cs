using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Tests
{
    public class ReverseRouteCommandTests
    {
        [Test]
        public void Execute_should_returns_empty_route_if_input_is_empty()
        {
            Route inputRoute = new Route();
            var reversedRoute = ReverseRoute(inputRoute);
            reversedRoute.ShouldBeSameAs(inputRoute);
            reversedRoute.TrackPoints.ShouldBeEmpty();
            reversedRoute.CoursePoints.ShouldBeEmpty();
        }

        [Test]
        public void Execute_should_keep_all_time_stamps_in_same_order()
        {
            var coursePointIndeces = new[] { 0, 3, 4 };
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(5)
                .WithCoursePointsAt(coursePointIndeces)
                .Build();

            var reversedRoute = ReverseRoute(inputRoute);

            AssertTimeStamps(
                reversedRoute.TrackPoints, 
                TestRouteBuilder.GetDefaultTimeStamps(5));
            AssertTimeStamps(
                reversedRoute.CoursePoints, 
                TestRouteBuilder.GetDefaultTimeStamps(new[] { 0, 3, 4 }));
        }

        [Test]
        public void Execute_should_reverse_order_of_all_positions()
        {
            var coursePointIndeces = new[] { 0, 3, 4 };
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(5)
                .WithCoursePointsAt(coursePointIndeces)
                .Build();

            var reversedRoute = ReverseRoute(inputRoute);

            AssertPositions(
                reversedRoute.TrackPoints, 
                TestRouteBuilder.GetDefaultPositions(5).Reverse());
            AssertPositions(
                reversedRoute.CoursePoints,
                TestRouteBuilder.GetDefaultPositions(coursePointIndeces).Reverse());
        }

        [TestCase(CoursePoint.PointType.Undefined, CoursePoint.PointType.Undefined)]
        [TestCase(CoursePoint.PointType.Left, CoursePoint.PointType.Right)]
        [TestCase(CoursePoint.PointType.Right, CoursePoint.PointType.Left)]
        [TestCase(CoursePoint.PointType.Straight, CoursePoint.PointType.Straight)]
        [TestCase(CoursePoint.PointType.Food, CoursePoint.PointType.Food)]
        [TestCase(CoursePoint.PointType.Generic, CoursePoint.PointType.Generic)]
        [TestCase(CoursePoint.PointType.Sprint, CoursePoint.PointType.Sprint)]
        [TestCase(CoursePoint.PointType.ClimbCat4, CoursePoint.PointType.ClimbCat4)]
        [TestCase(CoursePoint.PointType.ClimbCat3, CoursePoint.PointType.ClimbCat3)]
        [TestCase(CoursePoint.PointType.ClimbCat2, CoursePoint.PointType.ClimbCat2)]
        [TestCase(CoursePoint.PointType.ClimbCat1, CoursePoint.PointType.ClimbCat1)]
        [TestCase(CoursePoint.PointType.ClimbCatHors, CoursePoint.PointType.ClimbCatHors)]
        [TestCase(CoursePoint.PointType.Summit, CoursePoint.PointType.Summit)]
        [TestCase(CoursePoint.PointType.Valley, CoursePoint.PointType.Valley)]
        [TestCase(CoursePoint.PointType.Danger, CoursePoint.PointType.Danger)]
        [TestCase(CoursePoint.PointType.FirstAid, CoursePoint.PointType.FirstAid)]
        public void Execute_should_update_the_coursePoint_type(
            CoursePoint.PointType typeBefore, 
            CoursePoint.PointType expectedTypeAfter) 
        {
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(1)
                .WithCoursePointsAt(0).Build();
            inputRoute.CoursePoints[0].Type = typeBefore;

            var reversedRoute = ReverseRoute(inputRoute);

            reversedRoute.CoursePoints[0].Type.ShouldBe(expectedTypeAfter);
        }

        [TestCase("Left", "Right")]
        [TestCase("Right", "Left")]
        public void Execute_should_update_name_and_notes(string before, string expectedAfter)
        {
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(1)
                .WithCoursePointsAt(0).Build();
            inputRoute.CoursePoints[0].Type = CoursePoint.PointType.Right;
            inputRoute.CoursePoints[0].Name = before;
            inputRoute.CoursePoints[0].Notes = before;

            var reversedRoute = ReverseRoute(inputRoute);

            reversedRoute.CoursePoints[0].Name.ShouldBe(expectedAfter);
            reversedRoute.CoursePoints[0].Notes.ShouldBe(expectedAfter);
        }

        private static void AssertPositions<T>(
            List<T> points, 
            IEnumerable<Position> expectedPostions)
            where T : TrackPoint
        {
            points.Select(tp => tp.Lattitude)
                .ShouldBe(expectedPostions.Select(p => p.Lattitude));

            points.Select(tp => tp.Longitude)
                .ShouldBe(expectedPostions.Select(p => p.Longitude));
        }

        private static void AssertTimeStamps<T>(List<T> points, DateTime[] expectedTimeStamps) 
            where T : TrackPoint
        {
            var actualTimeStamps = points.Select(tp => tp.TimeStamp);
            actualTimeStamps.ShouldBe(expectedTimeStamps);
        }

        private static Route ReverseRoute(Route inputRoute)
        {
            return new ReverseRouteCommand()
                .Execute(new ReverseRouteInput(inputRoute))
                .Route;
        }
    }
}
