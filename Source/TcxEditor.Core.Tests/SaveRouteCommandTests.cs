using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core.Tests
{
    public class SaveRouteCommandTests
    {
        private RouteSaverSpy _saverSpy;
        private SaveRouteCommand _sut;

        [SetUp]
        public void Setup_SUT()
        {
            _saverSpy = new RouteSaverSpy();
            _sut = new SaveRouteCommand(_saverSpy);
        }

        [Test]
        public void Execute_with_null_input_should_throw_error()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [Test]
        public void Execute_with_null_throws_error()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(null));
        }

        [Test]
        public void Execute_with_null_Route_throws_error()
        {
            Assert.Throws<ArgumentNullException>(
                () => _sut.Execute(new SaveRouteInput(null, "source", "dest")));
        }

        [Test]
        public void Execute_with_ZERO_points_throws_error()
        {
            Route routeInput = new Route();
            Assert.Throws<TcxCoreException>(() =>
                _sut.Execute(new SaveRouteInput(routeInput, "source", "dest")));

            _saverSpy.CallCount.ShouldBe(0);
        }

        [Test]
        public void Execute_should_pass_input_to_saver()
        {
            Route routeInput = new Route();
            routeInput.CoursePoints.Add(new CoursePoint (1,1));

            var result = _sut.Execute(new SaveRouteInput(routeInput, "source", "dest"));

            result.Route.ShouldBeSameAs(routeInput);
            _saverSpy.SourcePath.ShouldBe("source");
            _saverSpy.DestinationPath.ShouldBe("dest");
            _saverSpy.Points.ShouldBeSameAs(routeInput.CoursePoints);
            _saverSpy.CallCount.ShouldBe(1);
        }
    }

    internal class RouteSaverSpy : IRouteSaver
    {
        public int CallCount { get; private set; }
        public List<CoursePoint> Points { get; private set; }
        public string SourcePath { get; private set; }
        public string DestinationPath { get; private set; }

        public void SaveCoursePoints(
            List<CoursePoint> points, 
            string sourcePath,
            string destinationPath)
        {
            Points = points;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            CallCount++;
        }
    }
}