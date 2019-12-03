using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddCoursePointResponse : IOutput
    {
        public Route Route { get; set; }
    }
}
