using System;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class DeleteCoursePointInput : IInput
    {
        public Route Route { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
