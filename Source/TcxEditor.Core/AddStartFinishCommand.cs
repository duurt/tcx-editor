using System;
using System.Linq;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class AddStartFinishCommand : IAddStartFinishCommand
    {
        public AddStartFinishResponse Execute(AddStartFinishInput input)
        {
            ValidateInput(input);
            AddStartPoint(input.Route);
            AddFinishPoint(input.Route);

            return new AddStartFinishResponse(input.Route);
        }

        private static void ValidateInput(AddStartFinishInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.Route.TrackPoints.Count < 2)
                throw new TcxCoreException(
                    $"There must be at least 2 track points. " +
                    $"Found: {input.Route.TrackPoints.Count}");

            if (input.Route.CoursePoints.Any())
            {
                if (input.Route.TrackPoints[0].TimeStamp == input.Route.CoursePoints[0].TimeStamp)
                    throw new TcxCoreException(
                        $"Start point is already present");

                if (input.Route.TrackPoints.Last().TimeStamp == input.Route.CoursePoints.Last().TimeStamp)
                    throw new TcxCoreException(
                        $"Start point is already present");
            }
        }

        private static void AddStartPoint(Route input)
        {
            input.CoursePoints.Insert(0, new CoursePoint(
                input.TrackPoints[0].Lattitude,
                input.TrackPoints[0].Longitude)
            {
                Name = "start",
                Notes = "Go! Have fun :-)",
                TimeStamp = input.TrackPoints[0].TimeStamp,
                Type = CoursePoint.PointType.Generic
            });
        }

        private static void AddFinishPoint(Route route)
        {
            route.CoursePoints.Add(new CoursePoint(
                route.TrackPoints.Last().Lattitude,
                route.TrackPoints.Last().Longitude)
            {
                Name = "finish",
                Notes = "Finished!",
                TimeStamp = route.TrackPoints.Last().TimeStamp,
                Type = CoursePoint.PointType.Generic
            });
        }
    }
}
