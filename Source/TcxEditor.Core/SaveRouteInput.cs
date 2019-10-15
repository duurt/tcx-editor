using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class SaveRouteInput : IInput
    {
        public Route Route { get; set; }
        public string SourceName { get; }
        public string DestinationPath { get; }

        public SaveRouteInput(Route route, string sourcepPath, string destinationPath)
        {
            Route = route;
            SourceName = sourcepPath;
            DestinationPath = destinationPath;
        }
    }
}
