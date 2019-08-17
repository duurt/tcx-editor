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

        public MainForm()
        {
            InitializeComponent();
            mapControl1.MapClickEvent += MapControl1_MapClickEvent;
        }

        private void MapControl1_MapClickEvent(object sender, MapClickEventArgs e)
        {
            // wiring up with event in IRouteView comes later
        }

        public void ShowRoute(Route route)
        {
            mapControl1.SetRoute(route);
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
    }
}
