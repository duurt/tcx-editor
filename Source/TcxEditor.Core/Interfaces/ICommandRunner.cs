namespace TcxEditor.Core.Interfaces
{
    public interface ICommandRunner
    {
        IOutput Execute(IInput input);
    }
}