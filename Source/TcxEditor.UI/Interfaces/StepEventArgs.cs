using System;

namespace TcxEditor.UI.Interfaces
{
    public class StepEventArgs : EventArgs
    {
        public int Step { get; }

        public StepEventArgs(int step)
        {
            Step = step;
        }
    }
}