using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class AddPointEventArgs
    {
        // todo: check consistency in GUI: am i using entities every where?
        public CoursePoint NewPoint { get; set; }
        public Route Route { get; internal set; }
    }
}