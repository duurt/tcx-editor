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
        public event EventHandler<OpenRouteEventArgs> OpenFileEvent;
        public event EventHandler<AddStartFinishEventargs> AddStartFinishEvent;

        public MainForm()
        {
            InitializeComponent();
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
                    OpenFileEvent?.Invoke(this, new OpenRouteEventArgs(dialog.FileName));
                }
            }
        }

        private void btnAddStartFinish_Click(object sender, EventArgs e)
        {
            AddStartFinishEvent?.Invoke(this, new AddStartFinishEventargs(mapControl1.CurrentRoute));
        }
    }
}
