using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcxEditor.Core.Interfaces
{
    public interface IStreamSaver
    {
        void Save(Stream data);
    }
}
