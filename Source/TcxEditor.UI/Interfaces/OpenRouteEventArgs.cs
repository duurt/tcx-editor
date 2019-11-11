using System;

namespace TcxEditor.UI.Interfaces
{
    public class OpenRouteEventArgs : EventArgs
    {
        public string Name { get; }

        public OpenRouteEventArgs(string name)
        {
            Name = name;
        }
    }
}