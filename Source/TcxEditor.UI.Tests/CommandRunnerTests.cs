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
        public void Execute_should_pass_input_to_command()
        {
            CommandA commandASpy = new CommandA();
            var sut = new CommandRunner(
                new ITcxEditorCommand[] { commandASpy });

            InputA input = new InputA();
            sut.Execute(input);

            commandASpy.LastInput.ShouldBeSameAs(input);
        }

        [Test]
        public void Execute_should_return_response_of_command()
        {
            var sut = new CommandRunner(
                new ITcxEditorCommand[] { new CommandA() });

            var result = sut.Execute(new InputA()) as OutputA;

            result.Val.ShouldBe("A1");
        }

        [Test]
        public void Execute_should_work_multiple_times_with_multiple_commands()
        {
            var sut = new CommandRunner(
                new ITcxEditorCommand[] { new CommandA(), new CommandB() });

            var result1 = sut.Execute(new InputB()) as OutputB;
            var result2 = sut.Execute(new InputA()) as OutputA;
            var result3 = sut.Execute(new InputA()) as OutputA;
            var result4 = sut.Execute(new InputB()) as OutputB;
            var result5 = sut.Execute(new InputB()) as OutputB;

            result1.Val.ShouldBe("B1");
            result2.Val.ShouldBe("A1");
            result3.Val.ShouldBe("A2");
            result4.Val.ShouldBe("B2");
            result5.Val.ShouldBe("B3");
        }
    }

    public interface ICommandA : ITcxEditorCommand<InputA, OutputA> { }
    public class InputA : IInput { }
    public class OutputA : IOutput { public string Val { get; set; } }
    public interface ICommandB : ITcxEditorCommand<InputB, OutputB> { }
    public class InputB : IInput { }
    public class OutputB : IOutput { public string Val { get; set; } }

    public class CommandA : ICommandA
    {
        private int _callCount = 1;
        public InputA LastInput { get; private set; }

        public OutputA Execute(InputA input)
        {
            LastInput = input;

            return new OutputA{ Val = "A" + _callCount++ };
        }
    }

    public class CommandB : ICommandB
    {
        private int _callCount = 1;

        public OutputB Execute(InputB input)
        {
            return new OutputB { Val = "B" + _callCount++ };
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
