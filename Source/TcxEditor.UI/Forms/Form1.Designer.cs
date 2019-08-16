namespace TcxEditor.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapControl1 = new TcxEditor.UI.MapControl();
            this.btnOpenRoute = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddStartFinish = new System.Windows.Forms.Button();
            this.btnSaveRoute = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Location = new System.Drawing.Point(146, 3);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(614, 525);
            this.mapControl1.TabIndex = 0;
            // 
            // btnOpenRoute
            // 
            this.btnOpenRoute.Location = new System.Drawing.Point(19, 31);
            this.btnOpenRoute.Name = "btnOpenRoute";
            this.btnOpenRoute.Size = new System.Drawing.Size(103, 23);
            this.btnOpenRoute.TabIndex = 1;
            this.btnOpenRoute.Text = "Open route file...";
            this.btnOpenRoute.UseVisualStyleBackColor = true;
            this.btnOpenRoute.Click += new System.EventHandler(this.btnOpenRoute_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.87287F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.12713F));
            this.tableLayoutPanel1.Controls.Add(this.mapControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(763, 531);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveRoute);
            this.panel1.Controls.Add(this.btnAddStartFinish);
            this.panel1.Controls.Add(this.btnOpenRoute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(137, 525);
            this.panel1.TabIndex = 1;
            // 
            // btnAddStartFinish
            // 
            this.btnAddStartFinish.Location = new System.Drawing.Point(19, 61);
            this.btnAddStartFinish.Name = "btnAddStartFinish";
            this.btnAddStartFinish.Size = new System.Drawing.Size(103, 23);
            this.btnAddStartFinish.TabIndex = 2;
            this.btnAddStartFinish.Text = "Add Start + Finish";
            this.btnAddStartFinish.UseVisualStyleBackColor = true;
            this.btnAddStartFinish.Click += new System.EventHandler(this.btnAddStartFinish_Click);
            // 
            // btnSaveRoute
            // 
            this.btnSaveRoute.Location = new System.Drawing.Point(19, 91);
            this.btnSaveRoute.Name = "btnSaveRoute";
            this.btnSaveRoute.Size = new System.Drawing.Size(103, 23);
            this.btnSaveRoute.TabIndex = 3;
            this.btnSaveRoute.Text = "Save Route";
            this.btnSaveRoute.UseVisualStyleBackColor = true;
            this.btnSaveRoute.Click += new System.EventHandler(this.btnSaveRoute_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Duurt\'s TCX editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MapControl mapControl1;
        private System.Windows.Forms.Button btnOpenRoute;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddStartFinish;
        private System.Windows.Forms.Button btnSaveRoute;
    }
}

