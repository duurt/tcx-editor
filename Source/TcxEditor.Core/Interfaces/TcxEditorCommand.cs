using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcxEditor.Core.Interfaces
{
    public interface ITcxEditorCommand
    {
    }

    public interface ITcxEditorCommand<TIn, TOut> : ITcxEditorCommand 
        where TIn : IInput
        where TOut : IOutput
    {
        TOut Execute(TIn input);
    }

    public interface IInput { }
    public interface IOutput { }
}
