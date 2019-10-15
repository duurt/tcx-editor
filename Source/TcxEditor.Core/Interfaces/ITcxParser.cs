using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Interfaces
{
    public interface ITcxParser
    {
        Route ParseTcx(Stream input);
    }
}
