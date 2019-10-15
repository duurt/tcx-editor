using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core.Interfaces
{
    public interface IRouteSaver
    {
        void SaveCoursePoints(
            List<CoursePoint> points, 
            string sourcePath, 
            string destinationPath);
    }
}
