using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class SelectPointEventArgs
    {
        public DateTime TimeStamp { get; set; }

        public SelectPointEventArgs(DateTime timeStamp)
        {
            TimeStamp = timeStamp;
        }
    }
}