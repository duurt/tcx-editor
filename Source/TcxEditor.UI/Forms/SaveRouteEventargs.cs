using System;

namespace TcxEditor.UI
{
    public class SaveRouteEventArgs : EventArgs
    {
        public string DestinationPath { get; set; }

        public SaveRouteEventArgs(string name)
        {
            DestinationPath = name;
        }
    }
}