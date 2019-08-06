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
using TcxEditor.Core.Interfaces;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace TcxEditor.UI
{
    public partial class MainForm : Form
    {
        private readonly IOpenRouteCommand _opener;

        public MainForm(IOpenRouteCommand opener)
        {
            _opener = opener;
            InitializeComponent();
        }

        private void btnOpenRoute_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var result = _opener.Execute(new OpenRouteInput { Name = dialog.FileName });

                    mapControl1.SetRoute(result.Route);
                }
            }
        }
    }
}
