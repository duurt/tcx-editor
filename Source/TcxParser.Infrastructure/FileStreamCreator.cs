using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Parser.Infrastructure
{
    public class FileStreamCreator : IStreamCreator
    {
        public Stream GetStream(string name)
        {
            return new FileStream(name, FileMode.Open, FileAccess.Read);
        }
    }
}
