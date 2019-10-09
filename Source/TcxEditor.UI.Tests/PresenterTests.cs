using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI.Tests
{
    public partial class PresenterTests
    {
        private Presenter GetPresenter()
        {
            return new Presenter(this, this, this, new CommandRunnerSpy());
        }
        
        [Test]
        public void Ctor_hooks_up_all_UI_events()
        {
            var sut = GetPresenter();

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
            var sut = new Presenter(this, this, this, new CommandRunnerSpy());

            GuiState.AddCoursePoint.ShouldBe(false);
            GuiState.SaveEnabled.ShouldBe(false);
            GuiState.ScrollRoute.ShouldBe(false);
            GuiState.DeleteCoursePoint.ShouldBe(false);
        }
    }

    internal class CommandRunnerSpy : ICommandRunner
    {
        public IOutput Execute(IInput input)
        {
            throw new NotImplementedException();
        }
    }

    public partial class PresenterTests : IRouteView, IErrorView, IGuiStateSetter
    {
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

        public void ShowEditCoursePointMarker(TcxEditor.Core.Entities.TrackPoint position)
        {
            throw new NotImplementedException();
        }

        public void ShowEditTrackPointMarker(TcxEditor.Core.Entities.TrackPoint position)
        {
            throw new NotImplementedException();
        }

        public void ShowErrorMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowPointToEdit(TcxEditor.Core.Entities.TrackPoint point)
        {
            throw new NotImplementedException();
        }

        public void ShowRoute(TcxEditor.Core.Entities.Route route)
        {
            throw new NotImplementedException();
        }
    }
}
