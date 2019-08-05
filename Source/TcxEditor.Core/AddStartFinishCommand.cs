using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Exceptions;

namespace TcxEditor.Core
{
    public class AddStartFinishCommand
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
        }

        private static void AddFinishPoint(Route route)
        {
            route.CoursePoints.Add(new CoursePoint(
                route.TrackPoints.Last().Lattitude,
                route.TrackPoints.Last().Longitude)
                    { TimeStamp = route.TrackPoints.Last().TimeStamp });
        }

        private static void AddStartPoint(Route input)
        {
            input.CoursePoints.Insert(0, new CoursePoint(
                input.TrackPoints[0].Lattitude,
                input.TrackPoints[0].Longitude)
                    { TimeStamp = input.TrackPoints[0].TimeStamp });
        }
    }
}
