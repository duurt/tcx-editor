using Duurt.TcxParser.Xsd.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
