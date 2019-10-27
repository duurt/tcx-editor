using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcxEditor.Core;
using TcxEditor.Core.Entities;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI.Tests
{

    // Todo: these tests were created after Presenter was written; not yet complete
    public partial class PresenterTests
    {
        private GuiStub _gui;
        private CommandRunnerSpy _commandSpy;

        [SetUp]
        public void InitializePresenter()
        {
            _gui = new GuiStub();
            _commandSpy = new CommandRunnerSpy();
            // Note: the tests NEVER interact with the presenter directly,
            // but only through the GuiStub. So simply wiring everything up here 
            // is enough. 
            new Presenter(_gui, _gui, _gui, _commandSpy);
        }

        [Test]
        public void Ctor_sets_initial_GUI_state()
        {
            _gui.GuiState.AddCoursePoint.ShouldBe(false);
            _gui.GuiState.SaveEnabled.ShouldBe(false);
            _gui.GuiState.ScrollRoute.ShouldBe(false);
            _gui.GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void OpenFileEvent_calls_CommandRunner()
        {
            OpenRoute();

            OpenRouteInput commandInput = (_commandSpy.LastCall as OpenRouteInput);
            commandInput.Name.ShouldBe("some file name");
        }

        [Test]
        public void OpenFileEvent_updates_gui_state()
        {
            OpenRoute();

            _gui.GuiState.AddCoursePoint.ShouldBe(false);
            _gui.GuiState.SaveEnabled.ShouldBe(true);
            _gui.GuiState.ScrollRoute.ShouldBe(false);
            _gui.GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void OpenFileEvent_shows_route_in_GUI()
        {
            Route testRoute = GetDefaultRoute();
            _commandSpy.SetResponse(
                new OpenRouteResponse { Route = testRoute });

            _gui.RaiseOpenFileEvent(
                new OpenRouteEventArgs("some file name"));

            _gui.Route.ShouldBeSameAs(testRoute);
        }

        [Test]
        public void AddStartFinishEvent_calls_CommandRunner()
        {
            OpenRoute();
            SelectATrackPoint();

            int routeId = _gui.Route.GetHashCode();
            _commandSpy.SetResponse(
                new AddStartFinishResponse(GetDefaultRoute()));

            _gui.RaiseAddStartFinishEvent();

            AddStartFinishInput commandInput = (_commandSpy.LastCall as AddStartFinishInput);
            commandInput.Route.GetHashCode().ShouldBe(routeId);
        }

        [Test]
        public void AddStartFinishEvent_updates_GUI_state()
        {
            OpenRoute();
            SelectATrackPoint();

            _commandSpy.SetResponse(
                new AddStartFinishResponse(GetDefaultRoute()));

            _gui.RaiseAddStartFinishEvent();

            _gui.GuiState.AddCoursePoint.ShouldBe(true);
            _gui.GuiState.SaveEnabled.ShouldBe(true);
            _gui.GuiState.ScrollRoute.ShouldBe(true);
            _gui.GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void These_events_show_error_message_when_no_route_loaded()
        {
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseClickSaveRouteEvent(new SaveRouteEventArgs("filename...")));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseAddPointEvent(GetDefaultAddPointEventArgs()));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseGetNearestEvent(GetDefaultGetNearestEventArgs()));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseAddStartFinishEvent());
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseDeletePointEvent());
            //TODO: add test for step event.
        }

        [Test]
        public void These_events_show_error_message_when_no_point_selected()
        {
            OpenRoute();
            _commandSpy.LastCall = null;

            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseAddPointEvent(GetDefaultAddPointEventArgs()));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseDeletePointEvent());
        }

        [Test]
        public void AddCoursePointEvent_shows_error_message_when_no_point_selected()
        {
            OpenRoute();
            _gui.RaiseAddPointEvent(
                new AddPointEventArgs
                {
                    Name = "name",
                    Notes = "notes",
                    PointType = CoursePoint.PointType.Food
                });

            _gui.ErrorMessage.ShouldNotBeNullOrEmpty();
            _commandSpy.LastCall.ShouldBeOfType(typeof(OpenRouteInput));
        }

        private void OpenRoute()
        {
            _commandSpy.SetResponse(
                new OpenRouteResponse { Route = GetDefaultRoute() });

            _gui.RaiseOpenFileEvent(
                new OpenRouteEventArgs("some file name"));
        }

        private void SelectATrackPoint()
        {
            _commandSpy.SetResponse(
                new GetNearestTrackPointResponse
                {
                    Route = GetDefaultRoute(),
                    Nearest = GetDefaultRoute().TrackPoints[1]
                });
            _gui.RaiseGetNearestEvent(
                new GetNearestEventArgs { ReferencePoint = new Position(1, 1) });
        }

        private static Route GetDefaultRoute()
        {
            var result = new Route();
            result.TrackPoints.AddRange(
                Enumerable.Range(0, 3).Select(i =>
                new TrackPoint(i, i)
                {
                    TimeStamp = new DateTime(2019, 1, 1, 12, 0, i)
                }).ToList());

            return result;
        }

        private static GetNearestEventArgs GetDefaultGetNearestEventArgs()
        {
            return new GetNearestEventArgs { ReferencePoint = new Position(1, 1) };
        }

        private static AddPointEventArgs GetDefaultAddPointEventArgs()
        {
            return new AddPointEventArgs
            {
                Name = "name",
                Notes = "notes",
                PointType = CoursePoint.PointType.Food
            };
        }

        private void TestErrorMessageAndNoCommand(Action action)
        {
            action.Invoke();

            _gui.ErrorMessage.ShouldNotBeNullOrEmpty();
            _commandSpy.LastCall.ShouldBeNull();
        }
    }
}
