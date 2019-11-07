using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
