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
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.Route.TrackPoints.Count < 2)
                throw new TcxCoreException(
                    $"There must be at least 2 track points. " +
                    $"Found: {input.Route.TrackPoints.Count}");

            input.Route.CoursePoints.Insert(0, new CoursePoint(
                input.Route.TrackPoints[0].Lattitude,
                input.Route.TrackPoints[0].Longitude)
            { TimeStamp = input.Route.TrackPoints[0].TimeStamp });

            input.Route.CoursePoints.Add(new CoursePoint(
                input.Route.TrackPoints.Last().Lattitude,
                input.Route.TrackPoints.Last().Longitude)
            { TimeStamp = input.Route.TrackPoints.Last().TimeStamp });

            return new AddStartFinishResponse(input.Route);
        }
    }
}
