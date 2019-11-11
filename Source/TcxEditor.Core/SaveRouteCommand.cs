using System;
using System.Linq;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class SaveRouteCommand :
        ITcxEditorCommand<SaveRouteInput, SaveRouteResponse>
    {
        private readonly IRouteSaver _saver;

        public SaveRouteCommand(IRouteSaver saver)
        {
            _saver = saver;
        }

        public SaveRouteResponse Execute(SaveRouteInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.Route == null)
                throw new ArgumentNullException(nameof(input.Route));

            if (!input.Route.CoursePoints.Any())
                throw new TcxCoreException(
                    "The route does not contain any navigation ques or special points. You can only save a route after these are added.");

            _saver.SaveCoursePoints(
                input.Route.CoursePoints,
                input.SourceName,
                input.DestinationPath);

            return new SaveRouteResponse { Route = input.Route };
        }
    }
}