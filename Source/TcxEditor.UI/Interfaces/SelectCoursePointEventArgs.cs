using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class SelectCoursePointEventArgs
    {
        public Position Position { get; set; }

        public SelectCoursePointEventArgs(Position position)
        {
            Position = position;
        }
    }
}