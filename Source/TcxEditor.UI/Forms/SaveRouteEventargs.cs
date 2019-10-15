using TcxEditor.Core.Entities;

namespace TcxEditor.UI
{
    public class SaveRouteEventargs
    {
       // public Route Route { get; }
        public string DestinationPath { get; set; }

        public SaveRouteEventargs(string name)
        {
            DestinationPath = name;
        }
    }
}