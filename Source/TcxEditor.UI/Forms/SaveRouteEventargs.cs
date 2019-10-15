using TcxEditor.Core.Entities;

namespace TcxEditor.UI
{
    public class SaveRouteEventargs
    {
        public Route Route { get; }
        public string Name { get; set; }

        public SaveRouteEventargs(string name)
        {
            Name = name;
        }
    }
}