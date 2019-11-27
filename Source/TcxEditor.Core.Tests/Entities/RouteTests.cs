using NUnit.Framework;
using Shouldly;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Tests.Entities
{
    public class EntityTests
    {
        [Test]
        public void Route_constructor_creates_empty_lists()
        {
            var route = new Route();
            route.CoursePoints.ShouldBeEmpty();
            route.TrackPoints.ShouldBeEmpty();
        }

        [Test]
        public void CoursePoint_constructor_creates_empty_string_for_notes()
        {
            var p = new CoursePoint(1, 1);
            p.Notes.ShouldBe("");
        }

        [Test]
        public void CoursePoint_constructor_creates_type_undefined()
        {
            var p = new CoursePoint(1, 1);
            p.Type.ShouldBe(CoursePoint.PointType.Undefined);
        }
    }
}
