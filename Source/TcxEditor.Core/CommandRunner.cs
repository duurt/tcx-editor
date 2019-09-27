using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class CommandRunner
    {
        private readonly IEnumerable<ITcxEditorCommand> _commands;

        public CommandRunner(IEnumerable<ITcxEditorCommand> commands)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));

            if (!commands.Any())
                throw new ArgumentException("There must be at least 1 command provided.");
        }

        public IOutput Execute(IInput input)
        {
            var command = GetCapableCommand(input)
                ?? throw new CommandNotFoundException(
                    $"There is no command found that can handle input of type {input.GetType().Name}");

            return Run(command, input);
        }

        private static IOutput Run(ITcxEditorCommand command, IInput input)
        {
            return (IOutput)Enumerable.First<System.Reflection.MethodInfo>((System.Reflection.MethodInfo[])command.GetType().GetMethods(), m => m.Name.Equals(nameof(Core.Interfaces.ITcxEditorCommand<IInput, IOutput>.Execute)))
                .Invoke(command, new[] { input });
        }

        private ITcxEditorCommand GetCapableCommand(IInput input)
        {
            return _commands.FirstOrDefault(
                c => CommandCanHandle(input, c));
        }

        private static bool CommandCanHandle(IInput input, ITcxEditorCommand c)
        {
            var interfaces = c.GetType().GetInterfaces();

            return interfaces[1]
                .GenericTypeArguments[0].Name
                .Equals(input.GetType().Name);
        }
    }
}
