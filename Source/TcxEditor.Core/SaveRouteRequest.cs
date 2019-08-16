using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;

namespace TcxEditor.Core
{
    public class SaveRouteRequest
    {
        public Route Route { get; set; }
        public string Name { get; }

        public SaveRouteRequest(Route route, string name)
        {
            Route = route;
            Name = name;
        }
    }
}
