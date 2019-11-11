using Duurt.TcxParser.Xsd.Generated;
using NUnit.Framework;
using Shouldly;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace TcxEditor.Parser.Tests
{
    [TestFixture]
    public class SanityChecks
    {
        private static readonly string _exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", "");
        private readonly string _pathTcx1 = Path.Combine(
            _exeDir,
            "TestData",
            "0- tcx_Test_ORIG.tcx");
        private readonly string _pathTcx2 = Path.Combine(
            _exeDir,
            "TestData",
            "Hunebedroute.tcx");

        [Test]
        public void TryoutGeneratedCode()
        {
            Stream tcxStream = File.OpenRead(_pathTcx1);
            var serializer = new XmlSerializer(typeof(TrainingCenterDatabase_t));
            var x = serializer.Deserialize(tcxStream) as TrainingCenterDatabase_t;

            x.Courses.Length.ShouldBe(1);
            x.Courses[0].Track.Length.ShouldBe(38);
            x.Courses[0].CoursePoint.Length.ShouldBe(6);

            x.Courses[0].Name = "edited by c#, hooray!";
            serializer.Serialize(File.OpenWrite(Path.Combine(_exeDir, "out.tcx")), x);
        }

        [Test]
        public void Check_That_every_coursePoint_coincides_with_a_track_point()
        {
            Stream tcxStream = File.OpenRead(_pathTcx2);
            var serializer = new XmlSerializer(typeof(TrainingCenterDatabase_t));
            var x = serializer.Deserialize(tcxStream) as TrainingCenterDatabase_t;

            foreach (var cue in x.Courses[0].CoursePoint)
            {
                Assert.True(x.Courses[0].Track.Any(tp => tp.Time.Equals(cue.Time)));
                Assert.True(x.Courses[0].Track.Any(
                    tp => tp.Position.LatitudeDegrees.Equals(cue.Position.LatitudeDegrees)));
                Assert.True(x.Courses[0].Track.Any(
                    tp => tp.Position.LongitudeDegrees.Equals(cue.Position.LongitudeDegrees)));
            }
        }
    }
}
