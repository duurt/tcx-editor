using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class SaveRouteCommand : ISaveRouteCommand
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

            if (input.Route.CoursePoints.Any())
                _saver.SaveCoursePoints(input.Route.CoursePoints, input.Name);

            return new SaveRouteResponse { Route = input.Route };
        }
    }
}