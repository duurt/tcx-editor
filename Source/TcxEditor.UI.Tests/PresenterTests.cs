using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;
using TcxEditor.Core;
using TcxEditor.Core.Entities;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI.Tests
{

    // Todo: these tests were created after Presenter was written; not yet complete
    // todo: add tests to check that state is kept correctly by presenter (route and selected points)
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
        public void AddStartFinishEvent_show_route_in_gui()
        {
            OpenRoute();
            SelectATrackPoint();

            Route route = GetDefaultRoute();
            _commandSpy.SetResponse(
                new AddStartFinishResponse(route));
            _gui.Route = null;

            _gui.RaiseAddStartFinishEvent();

            _gui.Route.ShouldBeSameAs(route);
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
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseStepEvent(-1));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseStepEvent(1));
            TestErrorMessageAndNoCommand(() =>
                _gui.RaiseReverseRouteEvent());
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
        public void SaveRouteEvent_calls_commandRunner()
        {
            var route = OpenRoute();

            _commandSpy.SetResponse(new SaveRouteResponse { Route = GetDefaultRoute() });
            _gui.RaiseClickSaveRouteEvent(new SaveRouteEventArgs("some name"));

            SaveRouteInput commandInput = (_commandSpy.LastCall as SaveRouteInput);
            commandInput.DestinationPath.ShouldBe("some name");
            commandInput.Route.ShouldBeSameAs(route);
        }

        [Test]
        public void SaveRouteEvent_shows_route_in_gui()
        {
            OpenRoute();
            Route routeAfterSave = GetDefaultRoute();
            _commandSpy.SetResponse(new SaveRouteResponse { Route = routeAfterSave });
            
            _gui.RaiseClickSaveRouteEvent(new SaveRouteEventArgs("some name"));

            _gui.Route.ShouldBeSameAs(routeAfterSave);
        }

        [Test]
        public void SaveRouteEvent_does_NOT_update_gui_state()
        {
            OpenRoute();

            var guiStateBefore = _gui.GuiState.GetHashCode();
            _commandSpy.SetResponse(new SaveRouteResponse { Route = GetDefaultRoute() });

            _gui.RaiseClickSaveRouteEvent(new SaveRouteEventArgs("some name"));

            var guiStateAfter =_gui.GuiState.GetHashCode();
            guiStateAfter.ShouldBe(guiStateBefore);
        }

        [Test]
        public void GetNearestEvent_calls_commandRunner()
        {
            var openedRoute = OpenRoute();
            
            _commandSpy.SetResponse(new GetNearestTrackPointResponse { 
                Route = GetDefaultRoute(), 
                Nearest =  new TrackPoint(2,2) });

            TrackPoint refPoint = new TrackPoint(3, 3);
            _gui.RaiseGetNearestEvent(new GetNearestEventArgs { ReferencePoint = refPoint });

            var commandInput = _commandSpy.LastCall as GetNearestTrackPointInput;
            commandInput.ReferencePoint.ShouldBeSameAs(refPoint);
            commandInput.Route.ShouldBeSameAs(openedRoute);
        }

        [Test]
        public void GetNearestEvent_shows_route_in_gui()
        {
            OpenRoute();
            Route returnedRoute = GetDefaultRoute();
            _commandSpy.SetResponse(new GetNearestTrackPointResponse
            {
                Route = returnedRoute,
                Nearest = new TrackPoint(2, 2)
            });
            
            _gui.RaiseGetNearestEvent(new GetNearestEventArgs { ReferencePoint = new TrackPoint(3, 3) });

            _gui.Route.ShouldBeSameAs(returnedRoute);
        }

        [Test]
        public void GetNearestEvent_sets_edit_marker_in_view()
        {
            OpenRoute();
            Route returnedRoute = GetDefaultRoute();
            TrackPoint returnedPoint = returnedRoute.TrackPoints[1];
            _commandSpy.SetResponse(new GetNearestTrackPointResponse
            {
                Route = returnedRoute,
                Nearest = returnedPoint
            });

            _gui.RaiseGetNearestEvent(new GetNearestEventArgs { ReferencePoint = new TrackPoint(1,2) });

            _gui.EditPoint.ShouldBeSameAs(returnedPoint);
        }

        [Test]
        public void GetNearestEvent_sets_gui_state()
        {
            OpenRoute();
            _commandSpy.SetResponse(new GetNearestTrackPointResponse
            {
                Route = GetDefaultRoute(),
                Nearest = GetDefaultRoute().TrackPoints[1]
            });

            _gui.RaiseGetNearestEvent(new GetNearestEventArgs { ReferencePoint = new TrackPoint(1, 2) });

            _gui.GuiState.AddCoursePoint.ShouldBe(true);
            _gui.GuiState.SaveEnabled.ShouldBe(true);
            _gui.GuiState.ScrollRoute.ShouldBe(true);
            _gui.GuiState.DeleteCoursePoint.ShouldBe(false);
        }

        [Test]
        public void ReverseRouteEvent_calls_commandRunner()
        {
            var openedRoute = OpenRoute();
            _commandSpy.SetResponse(new ReverseRouteResponse(GetDefaultRoute()));

            _gui.RaiseReverseRouteEvent();

            var commandInput = _commandSpy.LastCall as ReverseRouteInput;
            commandInput.Route.ShouldBe(openedRoute);
        }

        [Test]
        public void ReverseRouteEvent_shows_route_in_gui()
        {
            OpenRoute();
            Route newRoute = GetDefaultRoute();
            _commandSpy.SetResponse(new ReverseRouteResponse(newRoute));

            _gui.RaiseReverseRouteEvent();

            _gui.Route.ShouldBe(newRoute);
        }

        private Route OpenRoute()
        {
            Route route = GetDefaultRoute();
            _commandSpy.SetResponse(
                new OpenRouteResponse { Route = route });

            _gui.RaiseOpenFileEvent(
                new OpenRouteEventArgs("some file name"));

            return route;
        }

        private TrackPoint SelectATrackPoint()
        {
            TrackPoint nearestPoint = GetDefaultRoute().TrackPoints[1];
            _commandSpy.SetResponse(
                new GetNearestTrackPointResponse
                {
                    Route = GetDefaultRoute(),
                    Nearest = nearestPoint
                });
            _gui.RaiseGetNearestEvent(
                new GetNearestEventArgs { ReferencePoint = new Position(1, 1) });

            return nearestPoint;
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
