using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcxEditor.Core;
using TcxEditor.Core.Entities;
using TcxEditor.Core.Interfaces;
using TcxEditor.UI.Interfaces;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace TcxEditor.UI
{
    public partial class MainForm : Form, IRouteView, IErrorView, IGuiStateSetter
    {
        private string _fileName;

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler<AddStartFinishEventargs> AddStartFinishEvent;
        public event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler<DeletePointEventArgs> DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectTrackPointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;

        public MainForm()
        {
            InitializeComponent();
            InitTypesComboBox();
            mapControl1.MapClickEvent += MapControl1_MapClickEvent;
            mapControl1.TrackPointSelectEvent += OnTrackPointClick;
            mapControl1.CoursePointSelectEvent += OnCoursePointClick;
            KeyPreview = true;
        }

        private void OnTrackPointClick(object sender, PointSelectEventArgs e)
        {
            SelectTrackPointEvent?.Invoke(this, new SelectPointEventArgs(e._point));
        }

        private void OnCoursePointClick(object sender, PointSelectEventArgs e)
        {
            SelectCoursePointEvent?.Invoke(this, new SelectPointEventArgs(e._point));
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Left)
                RaiseAddPointEvent("Left", CoursePoint.PointType.Left);
            else if (e.KeyCode == Keys.Right)
                RaiseAddPointEvent("Right", CoursePoint.PointType.Right);
            else if (e.KeyCode == Keys.Up)
                RaiseAddPointEvent("Straight", CoursePoint.PointType.Straight);
        }

        private void InitTypesComboBox()
        {
            cbPointType.DataSource = Enum.GetNames(typeof(CoursePoint.PointType))
                .Except(new[] { nameof(CoursePoint.PointType.Undefined) }).ToList();
            cbPointType.SelectedItem = nameof(CoursePoint.PointType.Left);
        }

        private void MapControl1_MapClickEvent(object sender, MapClickEventArgs e)
        {
            if (mapControl1.CurrentRoute != null)
                GetNearestEvent?.Invoke(
                    this, 
                    new GetNearestEventArgs
                    {
                        Route = mapControl1.CurrentRoute,
                        ReferencePoint = new Position(e.Lattitude, e.Longitude)
                    });
        }

        private void btnOpenRoute_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _fileName = dialog.FileName;
                    OpenFileEvent?.Invoke(this, new OpenRouteEventArgs(dialog.FileName));
                }
            }
        }

        private void btnAddStartFinish_Click(object sender, EventArgs e)
        {
            AddStartFinishEvent?.Invoke(this, new AddStartFinishEventargs(mapControl1.CurrentRoute));
        }

        private void btnSaveRoute_Click(object sender, EventArgs e)
        {
            SaveRouteEvent?.Invoke(this, new SaveRouteEventargs(mapControl1.CurrentRoute, _fileName));
        }

        private void btnAddCoursePoint_Click(object sender, EventArgs e)
        {

            RaiseAddPointEvent(tbPointNotes.Text, GetSelectedPointType());
        }

        private CoursePoint.PointType GetSelectedPointType()
        {
            return (CoursePoint.PointType)Enum.Parse(
                typeof(CoursePoint.PointType),
                cbPointType.SelectedItem.ToString(),
                true);
        }

        private void RaiseAddPointEvent(string notes, CoursePoint.PointType pointType)
        {
            TrackPoint newPoint = mapControl1.PointToEdit;
            AddPointEvent?.Invoke(
                this,
                new AddPointEventArgs
                {
                    Route = mapControl1.CurrentRoute,
                    NewPoint =
                        new CoursePoint(newPoint.Lattitude, newPoint.Longitude)
                        {
                            TimeStamp = newPoint.TimeStamp,
                            Notes = notes,
                            Type = pointType
                        }
                });
        }

        public void ShowRoute(Route route)
        {
            mapControl1.SetRoute(route);
        }

        public void ShowPointToEdit(TrackPoint point)
        {
            mapControl1.ShowPointToEdit(point);
            tbPointNotes.Focus();
        }

        private void btnStepFwd_Click(object sender, EventArgs e)
        {
            mapControl1.StepForward();
        }

        private void btnStepBck_Click(object sender, EventArgs e)
        {
            mapControl1.StepBack();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeletePointEvent?.Invoke(
                this,
                new DeletePointEventArgs
                {
                    Route = mapControl1.CurrentRoute,
                });
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Fout");
        }

        public void Apply(GuiState state)
        {
            routeDirectionsGroup.Enabled = state.AddCoursePoint || state.DeleteCoursePoint;

            btnAddStartFinish.Enabled = state.AddCoursePoint;
            btnAddCoursePoint.Enabled = state.AddCoursePoint;
            btnDelete.Enabled = state.DeleteCoursePoint;

            btnSaveRoute.Enabled = state.SaveEnabled;

            grbRouteScrolling.Enabled = state.ScrollRoute;
        }

        public void ShowEditCoursePointMarker(TrackPoint position)
        {
            mapControl1.SetEditCoursePointMarker(position);
        }

        public void ShowEditTrackPointMarker(TrackPoint position)
        {
            mapControl1.ShowPointToEdit(position);
        }
    }

    public class GuiState
    {
        public bool SaveEnabled { get; set; }
        public bool AddCoursePoint { get; set; }
        public bool DeleteCoursePoint { get; set; }
        public bool ScrollRoute { get; internal set; }
    }
}
