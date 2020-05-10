using NUnit.Framework;
using Shouldly;
using System;
using System.Reflection;
using TcxEditor.Core.Exceptions;
using TcxEditor.Core.Interfaces;

namespace TcxEditor.Core.Tests
{
    public class CommandRunnerTests
    {
        [Test]
        public void Constructor_should_throw_error_with_null_or_empty_argument()
        {
            Assert.Throws<ArgumentNullException>(
                () => new CommandRunner(null));
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

        [Test]
        public void Execute_should_throw_TcxCoreExpception_from_command()
        {
            var sut = new CommandRunner(
                new ITcxEditorCommand[] { new CommandThrowingException(new TcxCoreException("xyz")) });

            Should.Throw<TcxCoreException>(() => sut.Execute(new InputC()))
                .Message.ShouldBe("xyz");
        }

        [Test]
        public void Execute_should_rethrow_TargetInvocationException_for_any_other_type_of_Expception_from_command()
        {
            var sut = new CommandRunner(
                new ITcxEditorCommand[] { new CommandThrowingException(new ArithmeticException("xyz")) });

            var actualException = Should.Throw<TargetInvocationException>(
                () => sut.Execute(new InputC()));
            
            actualException.InnerException.ShouldBeOfType<ArithmeticException>();
            actualException.InnerException.Message.ShouldBe("xyz");
        }
    }

    public class InputA : IInput { }
    public class OutputA : IOutput { public string Val { get; set; } }

    public class InputB : IInput { }
    public class OutputB : IOutput { public string Val { get; set; } }

    public class InputC : IInput { }
    public class OutputC : IOutput { public string Val { get; set; } }

    public class CommandA : ITcxEditorCommand<InputA, OutputA>
    {
        private int _callCount = 1;
        public InputA LastInput { get; private set; }

        public OutputA Execute(InputA input)
        {
            LastInput = input;

            return new OutputA { Val = "A" + _callCount++ };
        }
    }

    public class CommandB : ITcxEditorCommand<InputB, OutputB>
    {
        private int _callCount = 1;

        public OutputB Execute(InputB input)
        {
            return new OutputB { Val = "B" + _callCount++ };
        }
    }

    public class CommandThrowingException : ITcxEditorCommand<InputC, OutputC>
    {
        private readonly Exception _exception;

        public CommandThrowingException(Exception exToThrow)
        {
            _exception = exToThrow;
        }

        public OutputC Execute(InputC input)
        {
            throw _exception;
        }
    }
}
