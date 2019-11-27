using NUnit.Framework;
using Shouldly;
using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Tests.Entities
{
    public class PositionTests
    {
        [TestCase(-90.1, 0, false)]
        [TestCase(-90, 0, true)]
        [TestCase(-45, 0, true)]
        [TestCase(-0, 0, true)]
        [TestCase(45, 0, true)]
        [TestCase(90, 0, true)]
        [TestCase(90.1, 0, false)]

        [TestCase(0, -180.1, false)]
        [TestCase(0, -180, true)]
        [TestCase(0, -90, true)]
        [TestCase(0, -0, true)]
        [TestCase(0, 90, true)]
        [TestCase(0, 180, true)]
        [TestCase(0, 180.1, false)]
        public void Test_Range_of_lat_and_lon(double lat, double lon, bool allFine)
        {
            if (allFine)
                new Position(lat, lon);
            else
                Assert.Throws<ArgumentOutOfRangeException>(
                 () => new Position(lat, lon));
        }

        [Test]
        public void Constructor_sets_properties_Lat_and_Lon()
        {
            var pos = new Position(10, 20);

            pos.Lattitude.ShouldBe(10);
            pos.Longitude.ShouldBe(20);
        }
    }
}
