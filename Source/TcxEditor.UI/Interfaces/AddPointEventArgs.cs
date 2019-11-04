using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class AddPointEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public CoursePoint.PointType PointType { get; set; }
    }
}