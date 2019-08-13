using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class SaveRouteCommand : ISaveRouteCommand
    {
        private readonly IStreamSaver _saver;

        public SaveRouteCommand(IStreamSaver saver) 
        {
            _saver = saver;
        }

        public SaveRouteResponse Execute(SaveRouteRequest input)
        {
            throw new ArgumentNullException(nameof(input));
        }
    }
}