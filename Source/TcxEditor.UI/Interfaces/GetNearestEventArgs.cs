using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public class GetNearestEventArgs : EventArgs
    {
        public Position ReferencePoint { get; set; }
    }
}