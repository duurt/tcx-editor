using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class DeleteCoursePointCommand : IDeleteCoursePointCommand
    {
        public DeleteCoursePointResponse Execute(DeleteCoursePointInput input)
        {
            if (input.Route == null)
                throw new ArgumentNullException(nameof(input), nameof(input.Route));

            if (!input.Route.CoursePoints.Any(p => p.TimeStamp == input.TimeStamp))
                throw new TcxCoreException($"Cannot delete point with timestamp {input.TimeStamp}. Point not found.");

            input.Route.CoursePoints.RemoveAll(p => p.TimeStamp == input.TimeStamp);

            return
                new DeleteCoursePointResponse
                {
                    Route = input.Route
                };
        }
    }
}
