using System.IO;
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
