using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;

namespace TcxEditor.Core.Tests
{
    public class AddStartFinishCommandTests
    {
        private static readonly DateTime _t0 = new DateTime(2019, 8, 5, 12, 45, 59);
        private readonly double[] _la = new double[] { 1, 2, 3, 4 };
        private readonly double[] _lo = new double[] { 10, 20, 30, 40 };
        private readonly DateTime[] _times = new DateTime[]
            {
                _t0.AddMinutes(1),
                _t0.AddMinutes(2),
                _t0.AddMinutes(3),
                _t0.AddMinutes(4)
            };
        private AddStartFinishInput _input;
        private AddStartFinishCommand _sut;

        [SetUp]
        public void Set_up_the_tests()
        {
            _sut = GetAddStartFinishCommand();
            _input = GetEmptyInput();
        }

        private static AddStartFinishCommand GetAddStartFinishCommand()
        {
            return new AddStartFinishCommand();
        }

        private IEnumerable<TrackPoint> Get4TrackPoints()
        {
            return Enumerable.Range(0, 4).Select(
                i => new TrackPoint(_la[i], _lo[i]) { TimeStamp = _times[i] });
        }

        private IEnumerable<CoursePoint> Get2MiddleCoursePoints()
        {
            return Enumerable.Range(1, 2).Select(
                i => new CoursePoint(_la[i], _lo[i]) { TimeStamp = _times[i] });
        }

        private static AddStartFinishInput GetEmptyInput()
        {
            return new AddStartFinishInput(new Route());
        }

        [Test]
        public void Execute_should_not_accept_null()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [Test]
        public void Execute_should_throw_error_when_ZERO_trackpoints()
        {
            _input.Route.TrackPoints.Clear();

            Assert.Throws<TcxCoreException>(
                () => _sut.Execute(GetEmptyInput()));
        }

        [Test]
        public void Execute_should_throw_error_when_ONE_trackpoint()
        {
            _input.Route.TrackPoints.Add(new TrackPoint(1, 1));

            Assert.Throws<TcxCoreException>(
                () => _sut.Execute(_input));
        }

        [Test]
        public void Execute_should_Add_start_point_when_no_course_points()
        {
            _input.Route.TrackPoints.AddRange(Get4TrackPoints());

            var result = _sut.Execute(_input);

            StartMustMatch1stTrackpoint(result);
        }

        [Test]
        public void Execute_should_Add_start_point_when_few_course_points_present()
        {
            _input.Route.TrackPoints.AddRange(Get4TrackPoints());
            _input.Route.CoursePoints.AddRange(Get2MiddleCoursePoints());

            var result = _sut.Execute(_input);

            StartMustMatch1stTrackpoint(result);
        }

        [Test]
        public void Execute_should_Add_finish_point_when_no_course_points()
        {
            _input.Route.TrackPoints.AddRange(Get4TrackPoints());

            var result = _sut.Execute(_input);

            FinishMustMatchLastTrackpoint(result);
        }

        [Test]
        public void Execute_should_Add_finish_point_when_few_course_points_present()
        {
            _input.Route.TrackPoints
                .AddRange(Get4TrackPoints());
            _input.Route.CoursePoints
                .AddRange(Get2MiddleCoursePoints());

            var result = _sut.Execute(_input);

            FinishMustMatchLastTrackpoint(result);
        }

        [Test]
        public void Execute_should_throw_error_when_start_point_already_exists()
        {
            _input.Route.TrackPoints
                .AddRange(Get4TrackPoints());
            _input.Route.CoursePoints
                .Add(new CoursePoint(_la[0], _lo[0]) { TimeStamp = _times[0] });

            Assert.Throws<TcxCoreException>(
                () => _sut.Execute(_input));
        }

        [Test]
        public void Execute_should_throw_error_when_finish_point_already_exists()
        {
            _input.Route.TrackPoints
                .AddRange(Get4TrackPoints());
            _input.Route.CoursePoints
                .Add(new CoursePoint(_la.Last(), _lo.Last()) { TimeStamp = _times.Last() });

            Assert.Throws<TcxCoreException>(
                () => _sut.Execute(_input));
        }

        [Test]
        public void Execute_should_Add_points_with_correct_name_type_description()
        {
            _input.Route.TrackPoints.AddRange(Get4TrackPoints());

            var result = _sut.Execute(_input);

            result.Route.CoursePoints.First().Name.ShouldBe("start");
            result.Route.CoursePoints.First().Type.ShouldBe(CoursePoint.PointType.Generic);
            result.Route.CoursePoints.First().Notes.ShouldBe("Go! Have fun :-)");

            result.Route.CoursePoints.Last().Name.ShouldBe("finish");
            result.Route.CoursePoints.Last().Type.ShouldBe(CoursePoint.PointType.Generic);
            result.Route.CoursePoints.Last().Notes.ShouldBe("Finished!");
        }

        private void StartMustMatch1stTrackpoint(AddStartFinishResponse result)
        {
            result.Route.CoursePoints[0].TimeStamp.ShouldBe(_times[0]);
            result.Route.CoursePoints[0].Lattitude.ShouldBe(_la[0]);
            result.Route.CoursePoints[0].Longitude.ShouldBe(_lo[0]);
        }

        private void FinishMustMatchLastTrackpoint(AddStartFinishResponse result)
        {
            result.Route.CoursePoints.Last().TimeStamp.ShouldBe(_times.Last());
            result.Route.CoursePoints.Last().Lattitude.ShouldBe(_la.Last());
            result.Route.CoursePoints.Last().Longitude.ShouldBe(_lo.Last());
        }
    }
}
