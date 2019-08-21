using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class DeleteCoursePointInput
    {
        public Route Route { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
