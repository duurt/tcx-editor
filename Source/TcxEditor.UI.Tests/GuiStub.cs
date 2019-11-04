using System;
using TcxEditor.Core.Entities;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI.Tests
{
    public class GuiStub : IRouteView, IErrorView, IGuiStateSetter
    {
        public Route Route { get; set; }
        public GuiState GuiState { get; private set; }
        public TrackPoint EditPoint { get; private set; }
        public string ErrorMessage { get; private set; }

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler AddStartFinishEvent;
        public event EventHandler<SaveRouteEventArgs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        public event EventHandler<StepEventArgs> StepEvent;

        public void RaiseOpenFileEvent(OpenRouteEventArgs args) => OpenFileEvent.Invoke(this, args);
        public void RaiseAddStartFinishEvent() => AddStartFinishEvent.Invoke(this, EventArgs.Empty);
        public void RaiseClickSaveRouteEvent(SaveRouteEventArgs args) => SaveRouteEvent.Invoke(this, args);
        public void RaiseGetNearestEvent(GetNearestEventArgs args) => GetNearestEvent.Invoke(this, args);
        public void RaiseAddPointEvent(AddPointEventArgs args) => AddPointEvent.Invoke(this, args);
        public void RaiseDeletePointEvent() => DeletePointEvent.Invoke(this, EventArgs.Empty);
        public void RaiseStepEvent(int step) => StepEvent.Invoke(this, new StepEventArgs(step));

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
            ErrorMessage = msg;
        }

        public void ShowPointToEdit(TrackPoint point)
        {
            EditPoint = point;
        }

        public void ShowRoute(Route route)
        {
            Route = route;
        }
    }
}
