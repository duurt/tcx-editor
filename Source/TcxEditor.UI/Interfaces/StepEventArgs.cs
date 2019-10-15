namespace TcxEditor.UI.Interfaces
{
    public class StepEventArgs
    {
        public int Step { get; }

        public StepEventArgs(int step)
        {
            Step = step;
        }
    }
}