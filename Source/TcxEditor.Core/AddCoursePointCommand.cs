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
            Validate(input);
            AddNewPointToCoursePoints(input.Route.CoursePoints, input.NewCoursePoint);

            return
                new AddCoursePointResponse
                {
                    Route = input.Route
                };
        }

        private void Validate(AddCoursePointInput input)
        {
            if (input == null || input.Route == null || input.NewCoursePoint == null)
                throw new ArgumentNullException(nameof(input));

            if (input.Route.CoursePoints.Any(tp => AreCoinciding(tp, input.NewCoursePoint)))
                throw new TcxCoreException("The CoursePoint cannot be added, because a CoursePoint with the same position and time already exists.");

            if (!input.Route.TrackPoints.Any(tp => AreCoinciding(tp, input.NewCoursePoint)))
                throw new TcxCoreException("The CoursePoint cannot be added, because there is no corresponding TrackPoint.");
        }

        private bool AreCoinciding(TrackPoint tp, TrackPoint newCoursePoint)
        {
            return
                tp.Lattitude == newCoursePoint.Lattitude
                && tp.Longitude == newCoursePoint.Longitude
                && tp.TimeStamp == newCoursePoint.TimeStamp;
        }

        private static void AddNewPointToCoursePoints(List<CoursePoint> coursePoints, CoursePoint newPoint)
        {
            int index = coursePoints.FindIndex(
                cp => cp.TimeStamp > newPoint.TimeStamp);

            coursePoints.Insert(
                index < 0 ? 0 : index,
                newPoint);
        }
    }
}
