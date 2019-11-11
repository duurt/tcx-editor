using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class OpenRouteCommand : IOpenRouteCommand
    {
        private readonly IStreamCreator _streamCreator;
        private readonly ITcxParser _parser;

        // todo: validate input (mediatr?)
        public OpenRouteCommand(IStreamCreator streamCreator, ITcxParser parser)
        {
            _streamCreator = streamCreator;
            _parser = parser;
        }

        public OpenRouteResponse Execute(OpenRouteInput input)
        {
            Route result = _parser.ParseTcx(_streamCreator.GetStream(input.Name));

            return new OpenRouteResponse { Route = result };
        }
    }
}
