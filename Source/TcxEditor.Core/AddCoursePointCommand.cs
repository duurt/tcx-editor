using System;
using System.Collections.Generic;
using System.Linq;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddCoursePointCommand : 
        ITcxEditorCommand<AddCoursePointInput, AddCoursePointResponse>
    {
        public AddCoursePointResponse Execute(AddCoursePointInput input)
        {
            Validate(input);
            AddNewPoint(input.NewCoursePoint, input.Route.CoursePoints);

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

        private bool AreCoinciding(TrackPoint tp, TrackPoint newPoint)
        {
            return
                tp.Lattitude == newPoint.Lattitude
                && tp.Longitude == newPoint.Longitude
                && tp.TimeStamp == newPoint.TimeStamp;
        }

        private static void AddNewPoint(CoursePoint newPoint, List<CoursePoint> coursePoints)
        {
            coursePoints.Add(newPoint);
            coursePoints.Sort((x, y) => x.TimeStamp.CompareTo(y.TimeStamp));
        }
    }
}
