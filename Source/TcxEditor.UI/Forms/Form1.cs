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
    public partial class MainForm : Form, IRouteView
    {
        private string _fileName;

        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler<AddStartFinishEventargs> AddStartFinishEvent;
        public event EventHandler<SaveRouteEventargs> SaveRouteEvent;
        public event EventHandler<GetNearestEventArgs> GetNearestEvent;
        public event EventHandler<AddPointEventArgs> AddPointEvent;

        public MainForm()
        {
            InitializeComponent();
            InitTypesComboBox();
            mapControl1.MapClickEvent += MapControl1_MapClickEvent;
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
                            Notes = tbPointNotes.Text,
                            Type = (CoursePoint.PointType)Enum.Parse(
                                typeof(CoursePoint.PointType),
                                cbPointType.SelectedItem.ToString(),
                                true)
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
    }
}
