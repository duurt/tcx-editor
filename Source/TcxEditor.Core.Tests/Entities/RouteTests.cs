using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
