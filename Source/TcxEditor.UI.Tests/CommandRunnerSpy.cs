using TcxEditor.Core.Interfaces;

namespace TcxEditor.UI.Tests
{
    internal class CommandRunnerSpy : ICommandRunner
    {
        private IOutput _response;
        public IInput LastCall { get; private set; }

        public IOutput Execute(IInput input)
        {
            LastCall = input;
            return _response;
        }

        internal void SetResponse(IOutput openRouteResponse)
        {
            _response = openRouteResponse;
        }
    }
}
