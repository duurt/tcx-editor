using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.UI.Tests
{
    public class CommandRunnerTests
    {
        [Test]
        public void Constructor_should_throw_error_with_null_or_empty_argument()
        {
            Assert.Throws<ArgumentNullException>(
                () =>  new CommandRunner(null));
            Assert.Throws<ArgumentException>(
                () => new CommandRunner(new ITcxEditorCommand[0]));
        }

        [Test]
        public void Execute_should_throw_error_if_command_is_not_found()
        {
            var sut = new CommandRunner(new ITcxEditorCommand[] { new CommandA() });

            Assert.Throws<CommandNotFoundException>(
                () => sut.Execute(new InputB()));
        }

        [Test]
        public void Execute_should_execute_command()
        {
            string stringToBeUpdatedByCommand = "";
            var sut = new CommandRunner(new ITcxEditorCommand[] {
                    new CommandA { Action = () => stringToBeUpdatedByCommand += "yes!"} });

            sut.Execute(new InputA());

            stringToBeUpdatedByCommand.ShouldBe("yes!");
        }
    }

    public interface ICommandA : ITcxEditorCommand<InputA, OutputA> { }
    public class InputA : IInput { }
    public class OutputA : IOutput { }
    public interface ICommandB : ITcxEditorCommand<InputB, OutputB> { }
    public class InputB : IInput { }
    public class OutputB : IOutput { }

    public class CommandA : ICommandA
    {
        public Action Action { get; set; }

        public OutputA Execute(InputA input)
        {
            Action.Invoke();
            return null;
        }
    }

    public class CommandB : ICommandA
    {
        public Action Action { get; set; }

        public OutputA Execute(InputA input)
        {
            Action.Invoke();
            return null;
        }
    }

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
            var command = GetCommand(input)
                ?? throw new CommandNotFoundException(
                    $"There is no command found that can handle input of type {input.GetType().Name}");
            
            return Run(command, input);
        }

        private static IOutput Run(ITcxEditorCommand command, IInput input)
        {
            return (IOutput)Enumerable.First<System.Reflection.MethodInfo>((System.Reflection.MethodInfo[])command.GetType().GetMethods(), m => m.Name.Equals(nameof(Core.Interfaces.ITcxEditorCommand<IInput, IOutput>.Execute)))
                .Invoke(command, new[] { input });
        }

        private ITcxEditorCommand GetCommand(IInput input)
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

    [Serializable]
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() { }
        public CommandNotFoundException(string message) : base(message) { }
        public CommandNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CommandNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
