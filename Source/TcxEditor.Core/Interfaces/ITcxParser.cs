using System.IO;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Interfaces
{
    public interface ITcxParser
    {
        Route ParseTcx(Stream input);
    }
}
