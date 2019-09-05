using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcxEditor.Core.Interfaces
{
    public interface ITcxEditorCommand<TIn, TOut>
    {
        TOut Execute(TIn input);
    }
}
