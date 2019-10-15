using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcxEditor.Core.Interfaces
{
    public interface IAddCoursePointCommand : 
        ITcxEditorCommand<AddCoursePointInput, AddCoursePointResponse>
    {
    }
}
