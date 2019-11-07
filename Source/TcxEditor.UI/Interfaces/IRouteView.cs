using System;
using TcxEditor.Core.Entities;

namespace TcxEditor.UI.Interfaces
{
    public interface IRouteView
    {
        void ShowRoute(Route route);
        void ShowPointToEdit(TrackPoint point);
        void ShowEditTrackPointMarker(TrackPoint position);
        void ShowEditCoursePointMarker(TrackPoint position);

        event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        event EventHandler AddStartFinishEvent;
        event EventHandler<SaveRouteEventArgs> SaveRouteEvent;
        event EventHandler<GetNearestEventArgs> GetNearestEvent;
        event EventHandler<AddPointEventArgs> AddPointEvent;
        event EventHandler DeletePointEvent;
        event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        event EventHandler<StepEventArgs> StepEvent;
        event EventHandler ReverseRoute;
    }
}