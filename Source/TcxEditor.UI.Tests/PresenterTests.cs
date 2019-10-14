using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI.Tests
{
    public partial class PresenterTests
    {
        private CommandRunnerSpy _commandSpy;
        private Presenter _sut;

        public PresenterTests()
        {
            // todo: the tests class IS the GUI stub, but is created only once. 
            // Be careful with tear down! 
            // Perhaps the self shunt form here is not so good... 
            InitializePresenter();
        }

        private void InitializePresenter()
        {
            _commandSpy = new CommandRunnerSpy();
            _sut = new Presenter(this, this, this, _commandSpy);
        }
        
        [Test]
        public void Ctor_hooks_up_all_UI_events()
        {
            OpenFileEvent.ShouldNotBeNull();
            AddStartFinishEvent.ShouldNotBeNull();
            SaveRouteEvent.ShouldNotBeNull();
            GetNearestEvent.ShouldNotBeNull();
            AddPointEvent.ShouldNotBeNull();
            DeletePointEvent.ShouldNotBeNull();
            SelectCoursePointEvent.ShouldNotBeNull();
            StepEvent.ShouldNotBeNull();
        }

        [Test]
        public void Ctor_sets_initial_GUI_state()
        {
            GuiState.AddCoursePoint.ShouldBe(false);
            GuiState.SaveEnabled.ShouldBe(false);
            GuiState.ScrollRoute.ShouldBe(false);
            GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void OpenFileEvent_calls_CommandRunner()
        {
            _commandSpy.SetResponse(
                new OpenRouteResponse{ Route = new Route() });

            OpenFileEvent.Invoke(
                this, 
                new OpenRouteEventArgs("some file name"));

            OpenRouteInput commandInput = (_commandSpy.LastCall as OpenRouteInput);
            commandInput.Name.ShouldBe("some file name");
        }

        [Test]
        public void OpenFileEvent_updates_gui_state()
        {
            _commandSpy.SetResponse(
                new OpenRouteResponse { Route = new Route() });

            OpenFileEvent.Invoke(
                this,
                new OpenRouteEventArgs("some file name"));

            GuiState.AddCoursePoint.ShouldBe(false);
            GuiState.SaveEnabled.ShouldBe(true);
            GuiState.ScrollRoute.ShouldBe(false);
            GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void OpenFileEvent_shows_route_in_GUI()
        {
            Route testRoute = new Route();
            _commandSpy.SetResponse(
                new OpenRouteResponse { Route = testRoute });

            OpenFileEvent.Invoke(
                this,
                new OpenRouteEventArgs("some file name"));

            _route.ShouldBeSameAs(testRoute);
        }
    }

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

    public partial class PresenterTests : IRouteView, IErrorView, IGuiStateSetter
    {
        private Route _route;
        public GuiState GuiState { get; private set; }

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler<AddStartFinishEventArgs> AddStartFinishEvent;
        public event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        public event EventHandler<StepEventArgs> StepEvent;

        public void Apply(GuiState state)
        {
            GuiState = state;
        }

        public void ShowEditCoursePointMarker(TrackPoint position)
        {
            throw new NotImplementedException();
        }

        public void ShowEditTrackPointMarker(TrackPoint position)
        {
            throw new NotImplementedException();
        }

        public void ShowErrorMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowPointToEdit(TrackPoint point)
        {
            throw new NotImplementedException();
        }

        public void ShowRoute(Route route)
        {
            _route = route;
        }
    }
}
