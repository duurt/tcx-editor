using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class AddStartFinishInput
    {
        public Route Route { get; }

        public AddStartFinishInput(Route route)
        {
            Route = route;
        }
    }
}
