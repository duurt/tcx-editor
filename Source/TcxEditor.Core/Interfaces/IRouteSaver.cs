using System.Collections.Generic;
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
