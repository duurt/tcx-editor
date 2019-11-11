using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddCoursePointInput : IInput
    {
        public Route Route { get; set; }
        public CoursePoint NewCoursePoint { get; set; }
    }
}
