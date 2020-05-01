using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TcxEditor.Core.Entities;
using TcxEditor.UI.Interfaces;

namespace TcxEditor.UI
{
    public partial class MainForm : Form, IRouteView, IErrorView, IGuiStateSetter
    {
        private string _fileName;

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler AddStartFinishEvent;
        public event EventHandler<SaveRouteEventArgs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;
        public event EventHandler DeletePointEvent;
        public event EventHandler<SelectPointEventArgs> SelectCoursePointEvent;
        public event EventHandler<StepEventArgs> StepEvent;

        private readonly List<Control> _controlsThatSuppressShortCuts;

        public MainForm()
        {
            InitializeComponent();
            InitTypesComboBox();
            AddVersionToWindowTitle();

            _controlsThatSuppressShortCuts = new List<Control>
                {
                    tbPointNotes,
                    cbPointType
                };

            mapControl1.SetLocation(ConfigurationManager.AppSettings["Location"]);
            mapControl1.MapClickEvent += MapControl1_MapClickEvent;
            mapControl1.CoursePointSelectEvent += OnCoursePointClick;
            KeyPreview = true;
        }

        private void AddVersionToWindowTitle()
        {
            Version v = Assembly.GetEntryAssembly().GetName().Version;
            base.Text += $" - version {v.Major}.{v.Minor}.{v.Build}";
        }

        private void OnCoursePointClick(object sender, PointSelectEventArgs e)
        {
            SelectCoursePointEvent?.Invoke(this, new SelectPointEventArgs(e._timeStamp));
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (ShouldSuppressShortCut())
                return;

            // prevent keypress from being passed to underlying control
            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Left)
                RaiseAddPointEvent("Left", CoursePoint.PointType.Left);
            else if (e.KeyCode == Keys.Right)
                RaiseAddPointEvent("Right", CoursePoint.PointType.Right);
            else if (e.KeyCode == Keys.Up)
                RaiseAddPointEvent("Straight", CoursePoint.PointType.Straight);
            else if (e.KeyCode == Keys.M)
                RaiseAddPointEvent("MAP", CoursePoint.PointType.Generic);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (ShouldSuppressShortCut())
                return;

            // prevent keypress from being passed to underlying control
            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.A)
                RaiseStepEvent(1);
            else if (e.KeyCode == Keys.Z)
                RaiseStepEvent(-1);
            else if (e.KeyCode == Keys.S)
                RaiseStepEvent(10);
            else if (e.KeyCode == Keys.X)
                RaiseStepEvent(-10);
        }

        private bool ShouldSuppressShortCut()
        {
            return _controlsThatSuppressShortCuts.Any(c => c.Focused);
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
                        new SaveRouteEventArgs(saveFileDialog.FileName));
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
            RaiseStepEvent(1);
        }

        private void btnStepBck_Click(object sender, EventArgs e)
        {
            RaiseStepEvent(-1);
        }

        private void RaiseStepEvent(int step)
        {
            StepEvent?.Invoke(this, new StepEventArgs(step));
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
