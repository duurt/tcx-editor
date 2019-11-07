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
            this.btnOpenRoute = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mapControl1 = new TcxEditor.UI.MapControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbRouteScrolling = new System.Windows.Forms.GroupBox();
            this.btnStepBck = new System.Windows.Forms.Button();
            this.btnStepFwd = new System.Windows.Forms.Button();
            this.routeDirectionsGroup = new System.Windows.Forms.GroupBox();
            this.btnReverseRoute = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddStartFinish = new System.Windows.Forms.Button();
            this.btnAddCoursePoint = new System.Windows.Forms.Button();
            this.tbPointNotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPointType = new System.Windows.Forms.ComboBox();
            this.btnSaveRoute = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grbRouteScrolling.SuspendLayout();
            this.routeDirectionsGroup.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mapControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(763, 531);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // mapControl1
            // 
            this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl1.Location = new System.Drawing.Point(148, 3);
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.Size = new System.Drawing.Size(612, 525);
            this.mapControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grbRouteScrolling);
            this.panel1.Controls.Add(this.routeDirectionsGroup);
            this.panel1.Controls.Add(this.btnSaveRoute);
            this.panel1.Controls.Add(this.btnOpenRoute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 525);
            this.panel1.TabIndex = 1;
            // 
            // grbRouteScrolling
            // 
            this.grbRouteScrolling.Controls.Add(this.btnStepBck);
            this.grbRouteScrolling.Controls.Add(this.btnStepFwd);
            this.grbRouteScrolling.Location = new System.Drawing.Point(9, 105);
            this.grbRouteScrolling.Name = "grbRouteScrolling";
            this.grbRouteScrolling.Size = new System.Drawing.Size(125, 63);
            this.grbRouteScrolling.TabIndex = 5;
            this.grbRouteScrolling.TabStop = false;
            this.grbRouteScrolling.Text = "Route scrolling";
            // 
            // btnStepBck
            // 
            this.btnStepBck.Location = new System.Drawing.Point(6, 19);
            this.btnStepBck.Name = "btnStepBck";
            this.btnStepBck.Size = new System.Drawing.Size(56, 23);
            this.btnStepBck.TabIndex = 6;
            this.btnStepBck.Text = "<";
            this.btnStepBck.UseVisualStyleBackColor = true;
            this.btnStepBck.Click += new System.EventHandler(this.btnStepBck_Click);
            // 
            // btnStepFwd
            // 
            this.btnStepFwd.Location = new System.Drawing.Point(68, 19);
            this.btnStepFwd.Name = "btnStepFwd";
            this.btnStepFwd.Size = new System.Drawing.Size(56, 23);
            this.btnStepFwd.TabIndex = 5;
            this.btnStepFwd.Text = ">";
            this.btnStepFwd.UseVisualStyleBackColor = true;
            this.btnStepFwd.Click += new System.EventHandler(this.btnStepFwd_Click);
            // 
            // routeDirectionsGroup
            // 
            this.routeDirectionsGroup.Controls.Add(this.btnReverseRoute);
            this.routeDirectionsGroup.Controls.Add(this.btnDelete);
            this.routeDirectionsGroup.Controls.Add(this.btnAddStartFinish);
            this.routeDirectionsGroup.Controls.Add(this.btnAddCoursePoint);
            this.routeDirectionsGroup.Controls.Add(this.tbPointNotes);
            this.routeDirectionsGroup.Controls.Add(this.label2);
            this.routeDirectionsGroup.Controls.Add(this.label1);
            this.routeDirectionsGroup.Controls.Add(this.cbPointType);
            this.routeDirectionsGroup.Location = new System.Drawing.Point(4, 174);
            this.routeDirectionsGroup.Name = "routeDirectionsGroup";
            this.routeDirectionsGroup.Size = new System.Drawing.Size(130, 342);
            this.routeDirectionsGroup.TabIndex = 4;
            this.routeDirectionsGroup.TabStop = false;
            this.routeDirectionsGroup.Text = "Route directions";
            // 
            // btnReverseRoute
            // 
            this.btnReverseRoute.Location = new System.Drawing.Point(15, 276);
            this.btnReverseRoute.Name = "btnReverseRoute";
            this.btnReverseRoute.Size = new System.Drawing.Size(103, 23);
            this.btnReverseRoute.TabIndex = 8;
            this.btnReverseRoute.Text = "Reverse route";
            this.btnReverseRoute.UseVisualStyleBackColor = true;
            this.btnReverseRoute.Click += new System.EventHandler(this.btnReverseRoute_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(15, 179);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(103, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Remove point";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddStartFinish
            // 
            this.btnAddStartFinish.Location = new System.Drawing.Point(15, 237);
            this.btnAddStartFinish.Name = "btnAddStartFinish";
            this.btnAddStartFinish.Size = new System.Drawing.Size(103, 23);
            this.btnAddStartFinish.TabIndex = 2;
            this.btnAddStartFinish.Text = "Add Start + Finish";
            this.btnAddStartFinish.UseVisualStyleBackColor = true;
            this.btnAddStartFinish.Click += new System.EventHandler(this.btnAddStartFinish_Click);
            // 
            // btnAddCoursePoint
            // 
            this.btnAddCoursePoint.Location = new System.Drawing.Point(15, 149);
            this.btnAddCoursePoint.Name = "btnAddCoursePoint";
            this.btnAddCoursePoint.Size = new System.Drawing.Size(103, 23);
            this.btnAddCoursePoint.TabIndex = 4;
            this.btnAddCoursePoint.Text = "Add";
            this.btnAddCoursePoint.UseVisualStyleBackColor = true;
            this.btnAddCoursePoint.Click += new System.EventHandler(this.btnAddCoursePoint_Click);
            // 
            // tbPointNotes
            // 
            this.tbPointNotes.Location = new System.Drawing.Point(3, 113);
            this.tbPointNotes.Name = "tbPointNotes";
            this.tbPointNotes.Size = new System.Drawing.Size(121, 20);
            this.tbPointNotes.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "2. Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "1. Type";
            // 
            // cbPointType
            // 
            this.cbPointType.FormattingEnabled = true;
            this.cbPointType.Location = new System.Drawing.Point(3, 45);
            this.cbPointType.Name = "cbPointType";
            this.cbPointType.Size = new System.Drawing.Size(121, 21);
            this.cbPointType.TabIndex = 0;
            // 
            // btnSaveRoute
            // 
            this.btnSaveRoute.Location = new System.Drawing.Point(19, 60);
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
            this.Text = "TCX editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grbRouteScrolling.ResumeLayout(false);
            this.routeDirectionsGroup.ResumeLayout(false);
            this.routeDirectionsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MapControl mapControl1;
        private System.Windows.Forms.Button btnOpenRoute;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAddStartFinish;
        private System.Windows.Forms.Button btnSaveRoute;
        private System.Windows.Forms.GroupBox routeDirectionsGroup;
        private System.Windows.Forms.Button btnAddCoursePoint;
        private System.Windows.Forms.TextBox tbPointNotes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPointType;
        private System.Windows.Forms.Button btnStepFwd;
        private System.Windows.Forms.Button btnStepBck;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox grbRouteScrolling;
        private System.Windows.Forms.Button btnReverseRoute;
    }
}

