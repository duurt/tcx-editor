using NUnit.Framework;
using Shouldly;
using System;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;

namespace TcxEditor.Core.Tests
{
    public class GetNearestTrackPointCommandTests
    {
        private GetNearestTrackPointCommand _sut;

        [SetUp]
        public void PrepareSut()
        {
            _sut = new GetNearestTrackPointCommand();

        }

        [Test]
        public void Execute_should_throw_error_with_null_inputs()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(null));
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(new GetNearestTrackPointInput { Route = null }));
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(new GetNearestTrackPointInput {
                    Route = new Route(), ReferencePoint = null }));
        }

        [Test]
        public void Execute_Should_throw_error_with_NO_trackpoints()
        {
            Assert.Throws<TcxCoreException>(
               () => _sut.Execute(
                   new GetNearestTrackPointInput
                   {
                       Route = new Route(),
                       ReferencePoint = new Position(1,1)
                   }));
        }

        [Test]
        public void Execute_Should_return_point_with_one_trackpoint()
        {
            TrackPoint trackPoint = new TrackPoint(2, 2)
            {
                TimeStamp = new DateTime(2019, 8, 16, 12, 45, 59)
            };
            Route route = new Route();
            route.TrackPoints.Add(trackPoint);

            var result = _sut.Execute(
                new GetNearestTrackPointInput
                {
                    Route = route,
                    ReferencePoint = new Position(1, 1)
                });
            AssertNearestResult(result, trackPoint);
        }

        [Test]
        public void Execute_Should_return_nearest_point()
        {
            TrackPoint p0 = new TrackPoint(1, 1)
            {
                TimeStamp = new DateTime(2019, 8, 16, 12, 44, 59)
            };
            TrackPoint p1_nearest = new TrackPoint(2, 2)
            {
                TimeStamp = new DateTime(2019, 8, 16, 12, 45, 59)
            };
            TrackPoint p2 = new TrackPoint(3, 3)
            {
                TimeStamp = new DateTime(2019, 8, 16, 12, 46, 59)
            };
            Route route = new Route();
            route.TrackPoints.AddRange(new[] { p0, p1_nearest, p2 });

            var result = _sut.Execute(
                new GetNearestTrackPointInput
                {
                    Route = route,
                    ReferencePoint = new Position(1.9, 1.9)
                });

            result.Nearest.Lattitude.ShouldBe(p1_nearest.Lattitude);
            result.Nearest.Longitude.ShouldBe(p1_nearest.Longitude);
            result.Nearest.TimeStamp.ShouldBe(p1_nearest.TimeStamp);
        }

        [Test]
        public void Execute_Should_return_input_route()
        {
            TrackPoint trackPoint = new TrackPoint(2, 2)
            {
                TimeStamp = new DateTime(2019, 8, 16, 12, 45, 59)
            };
            Route inputRoute = new Route();
            inputRoute.TrackPoints.Add(trackPoint);

            var result = _sut.Execute(
                new GetNearestTrackPointInput
                {
                    Route = inputRoute,
                    ReferencePoint = new Position(1, 1)
                });
            result.Route.ShouldBeSameAs(inputRoute);
        }

        private static void AssertNearestResult(GetNearestTrackPointResponse result, TrackPoint trackPoint)
        {
            result.Nearest.Lattitude.ShouldBe(trackPoint.Lattitude);
            result.Nearest.Longitude.ShouldBe(trackPoint.Longitude);
            result.Nearest.TimeStamp.ShouldBe(trackPoint.TimeStamp);
        }
    }
}
