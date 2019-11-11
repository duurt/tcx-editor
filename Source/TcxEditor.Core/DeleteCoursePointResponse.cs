using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class DeleteCoursePointResponse : IOutput
    {
        public Route Route{ get; set; }
    }
}
