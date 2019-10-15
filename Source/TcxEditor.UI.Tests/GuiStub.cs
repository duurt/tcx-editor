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

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler AddStartFinishEvent;
        public event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        public event EventHandler<StepEventArgs> StepEvent;

        public void ClickOpenFile(OpenRouteEventArgs args) => OpenFileEvent.Invoke(this, args);
        public void ClickAddStartFinish() => AddStartFinishEvent.Invoke(this, EventArgs.Empty);
        public void ClickSaveRoute(SaveRouteEventargs args) => SaveRouteEvent.Invoke(this, args);
        public void ClickGetNearest(GetNearestEventArgs args) => GetNearestEvent.Invoke(this, args);
        public void ClickAddPoint(AddPointEventArgs args) => AddPointEvent.Invoke(this, args);
        public void ClickDeletePoint() => DeletePointEvent.Invoke(this, EventArgs.Empty);

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
            EditPoint = point;
        }

        public void ShowRoute(Route route)
        {
            Route = route;
        }
    }
}
