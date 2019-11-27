using System.IO;

namespace TcxEditor.Core.Interfaces
{
    public interface IStreamCreator
    {
        Stream GetStream(string name);
    }
}
