using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        public event EventHandler AddStartFinishEvent;
        public event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        public event EventHandler<StepEventArgs> StepEvent;

        public MainForm()
        {
            InitializeComponent();
            InitTypesComboBox();
            mapControl1.SetPosition(ConfigurationManager.AppSettings["Location"]);
            mapControl1.MapClickEvent += MapControl1_MapClickEvent;
            mapControl1.CoursePointSelectEvent += OnCoursePointClick;
            KeyPreview = true;
        }

        private void OnCoursePointClick(object sender, PointSelectEventArgs e)
        {
            SelectCoursePointEvent?.Invoke(this, new SelectPointEventArgs(e._timeStamp));
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
            GetNearestEvent?.Invoke(
                this,
                new GetNearestEventArgs
                {
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
            AddStartFinishEvent?.Invoke(this, EventArgs.Empty);
        }

        private void btnSaveRoute_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "TCX file|*.tcx";
                saveFileDialog.Title = "Save the Route";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                    SaveRouteEvent?.Invoke(
                        this, 
                        new SaveRouteEventargs(saveFileDialog.FileName));
            }
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
            AddPointEvent?.Invoke(
                this,
                new AddPointEventArgs
                {
                    Name = pointType.ToString(),
                    Notes = notes,
                    PointType = pointType,
                });
        }

        public void ShowRoute(Route route)
        {
            mapControl1.SetRoute(route);
        }

        public void ShowPointToEdit(TrackPoint point)
        {
            mapControl1.ShowPointToEdit(point);
        }

        private void btnStepFwd_Click(object sender, EventArgs e)
        {
            StepEvent?.Invoke(this, new StepEventArgs(1));
        }

        private void btnStepBck_Click(object sender, EventArgs e)
        {
            StepEvent?.Invoke(this, new StepEventArgs(-1));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeletePointEvent?.Invoke(
                this,
                EventArgs.Empty);
        }

        public void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Something went wrong...");
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
