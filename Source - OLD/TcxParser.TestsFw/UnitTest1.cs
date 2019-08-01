using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xmlschemas.TrainingCenterDatabase.V2;

namespace TcxParser.TestsFw
{
    [TestClass]
    public class UnitTest1
    {
        private string _pathTcx1 = Path.Combine("TestData", "0- tcx_Test_ORIG.tcx");

        [TestMethod]
        public void TryoutGeneratedCode()
        {
            Stream tcxStream = File.OpenRead(_pathTcx1);

            var serializer = new XmlSerializer(typeof(TrainingCenterDatabase_T));

            var x = serializer.Deserialize(tcxStream) as TrainingCenterDatabase_T;

            int a = 1;
            a.ShouldBe
        }
    }
}
