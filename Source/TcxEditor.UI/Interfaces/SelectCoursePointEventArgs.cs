using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class SelectPointEventArgs
    {
        public Position Position { get; set; }

        public SelectPointEventArgs(Position position)
        {
            Position = position;
        }
    }
}