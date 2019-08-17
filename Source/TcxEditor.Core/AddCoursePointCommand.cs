using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddCoursePointCommand : IAddCoursePointCommand
    {
        public AddCoursePointResponse Execute(AddCoursePointInput input)
        {
            if (input == null || input.Route == null || input.NewCoursePoint == null)
                throw new ArgumentNullException(nameof(input));

            if (!input.Route.TrackPoints.Any(tp => AreCoinciding(tp, input.NewCoursePoint)))
                throw new TcxCoreException("Fout!!");

            int index = input.Route.CoursePoints.FindIndex(
                cp => cp.TimeStamp > input.NewCoursePoint.TimeStamp);

            input.Route.CoursePoints.Insert(
                index < 0 ? 0 : index, 
                input.NewCoursePoint);

            return 
                new AddCoursePointResponse
                {
                    Route = input.Route
                };
        }

        private bool AreCoinciding(TrackPoint tp, CoursePoint newCoursePoint)
        {
            return
                tp.Lattitude == newCoursePoint.Lattitude
                && tp.Longitude == newCoursePoint.Longitude
                && tp.TimeStamp == newCoursePoint.TimeStamp;
        }
    }
}
