using Duurt.TcxParser.Xsd.Generated;
using System.IO;
using System.Xml.Serialization;

namespace TcxEditor.Parser
{
    public class XmlParser
    {
        public TrainingCenterDatabase_t Parse(Stream input)
        {
            return new XmlSerializer(typeof(TrainingCenterDatabase_t))
                .Deserialize(input) as TrainingCenterDatabase_t;
        }
    }
}
