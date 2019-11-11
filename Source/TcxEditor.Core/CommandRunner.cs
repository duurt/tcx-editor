using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core
{
    public class CommandRunner : ICommandRunner
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
            try
            {
                return (IOutput)Enumerable
                    .First(command.GetType().GetMethods(), m => m.Name.Equals(nameof(ITcxEditorCommand<IInput, IOutput>.Execute)))
                    .Invoke(command, new[] { input });
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException is TcxCoreException)
                    throw e.InnerException;
                else
                    throw;
            }
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
