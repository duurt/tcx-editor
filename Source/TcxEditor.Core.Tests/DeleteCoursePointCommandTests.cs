using NUnit.Framework;
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
    public class DeleteCoursePointCommandTests
    {
        private DeleteCoursePointCommand _sut;

        [SetUp]
        public void Init_SUT()
        {
            _sut = new DeleteCoursePointCommand();

        }

        [Test]
        public void Execute_should_throw_errors_with_null_route()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _sut.Execute(new DeleteCoursePointInput { Route = null }));
        }

        [Test]
        public void Execute_should_throw_errors_when_zero_coursePoints()
        {
            Assert.Throws<TcxCoreException>(() =>
                _sut.Execute(new DeleteCoursePointInput { Route = new Route() }));
        }

        [Test]
        public void Execute_should_delete_point_if_1_point()
        {
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(3)
                .WithCoursePointsAt(1)
                .Build();

            var result = _sut.Execute(
                new DeleteCoursePointInput
                {
                    Route = inputRoute,
                    TimeStamp = TestRouteBuilder.GetTimeStamp(1)
                });

            result.Route.CoursePoints.ShouldBeEmpty();
        }

        [Test]
        public void Execute_should_delete_point_if_many_points()
        {
            var inputRoute = new TestRouteBuilder()
                .WithTrackPointCount(10)
                .WithCoursePointsAt(3, 6, 9)
                .Build();

            var result = _sut.Execute(
                new DeleteCoursePointInput
                {
                    Route = inputRoute,
                    TimeStamp = TestRouteBuilder.GetTimeStamp(6)
                });

            result.Route.CoursePoints.Count.ShouldBe(2);
            result.Route.CoursePoints[0].TimeStamp.ShouldBe(TestRouteBuilder.GetTimeStamp(3));
            result.Route.CoursePoints[1].TimeStamp.ShouldBe(TestRouteBuilder.GetTimeStamp(9));
        }
    }
}
