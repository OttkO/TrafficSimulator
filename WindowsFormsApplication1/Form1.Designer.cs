namespace tracy
{
    partial class Tracy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tracy));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelRoads = new System.Windows.Forms.Panel();
            this.pbStraightV = new System.Windows.Forms.PictureBox();
            this.pbStraightH = new System.Windows.Forms.PictureBox();
            this.pbCurveSW = new System.Windows.Forms.PictureBox();
            this.pbCurveSE = new System.Windows.Forms.PictureBox();
            this.pbCurveNW = new System.Windows.Forms.PictureBox();
            this.pbCurveNE = new System.Windows.Forms.PictureBox();
            this.pbPCrossroad = new System.Windows.Forms.PictureBox();
            this.pbCCrossroad = new System.Windows.Forms.PictureBox();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.pbGrid = new System.Windows.Forms.PictureBox();
            this.Tools = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRoads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStraightV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStraightH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveSW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveSE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveNW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveNE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPCrossroad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCCrossroad)).BeginInit();
            this.panelSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelRoads
            // 
            this.panelRoads.AutoScroll = true;
            this.panelRoads.BackColor = System.Drawing.SystemColors.Window;
            this.panelRoads.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRoads.Controls.Add(this.pbStraightV);
            this.panelRoads.Controls.Add(this.pbStraightH);
            this.panelRoads.Controls.Add(this.pbCurveSW);
            this.panelRoads.Controls.Add(this.pbCurveSE);
            this.panelRoads.Controls.Add(this.pbCurveNW);
            this.panelRoads.Controls.Add(this.pbCurveNE);
            this.panelRoads.Controls.Add(this.pbPCrossroad);
            this.panelRoads.Controls.Add(this.pbCCrossroad);
            this.panelRoads.Location = new System.Drawing.Point(30, 28);
            this.panelRoads.Name = "panelRoads";
            this.panelRoads.Size = new System.Drawing.Size(231, 207);
            this.panelRoads.TabIndex = 0;
            // 
            // pbStraightV
            // 
            this.pbStraightV.Image = global::tracy.Resource1.StraightVIcon;
            this.pbStraightV.Location = new System.Drawing.Point(116, 279);
            this.pbStraightV.Name = "pbStraightV";
            this.pbStraightV.Size = new System.Drawing.Size(86, 86);
            this.pbStraightV.TabIndex = 7;
            this.pbStraightV.TabStop = false;
            this.pbStraightV.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // pbStraightH
            // 
            this.pbStraightH.Image = global::tracy.Resource1.StraightHIco;
            this.pbStraightH.Location = new System.Drawing.Point(14, 279);
            this.pbStraightH.Name = "pbStraightH";
            this.pbStraightH.Size = new System.Drawing.Size(86, 86);
            this.pbStraightH.TabIndex = 6;
            this.pbStraightH.TabStop = false;
            this.pbStraightH.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // pbCurveSW
            // 
            this.pbCurveSW.Image = global::tracy.Resource1.curveSWIcon;
            this.pbCurveSW.Location = new System.Drawing.Point(116, 187);
            this.pbCurveSW.Name = "pbCurveSW";
            this.pbCurveSW.Size = new System.Drawing.Size(86, 86);
            this.pbCurveSW.TabIndex = 5;
            this.pbCurveSW.TabStop = false;
            this.pbCurveSW.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // pbCurveSE
            // 
            this.pbCurveSE.Image = global::tracy.Resource1.curveSEIcon;
            this.pbCurveSE.Location = new System.Drawing.Point(14, 187);
            this.pbCurveSE.Name = "pbCurveSE";
            this.pbCurveSE.Size = new System.Drawing.Size(86, 86);
            this.pbCurveSE.TabIndex = 4;
            this.pbCurveSE.TabStop = false;
            this.pbCurveSE.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // pbCurveNW
            // 
            this.pbCurveNW.Image = global::tracy.Resource1.curveNWIcon;
            this.pbCurveNW.Location = new System.Drawing.Point(116, 95);
            this.pbCurveNW.Name = "pbCurveNW";
            this.pbCurveNW.Size = new System.Drawing.Size(86, 86);
            this.pbCurveNW.TabIndex = 3;
            this.pbCurveNW.TabStop = false;
            this.pbCurveNW.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pbCurveNE
            // 
            this.pbCurveNE.Image = global::tracy.Resource1.curveNEIcon;
            this.pbCurveNE.Location = new System.Drawing.Point(14, 95);
            this.pbCurveNE.Name = "pbCurveNE";
            this.pbCurveNE.Size = new System.Drawing.Size(86, 86);
            this.pbCurveNE.TabIndex = 2;
            this.pbCurveNE.TabStop = false;
            this.pbCurveNE.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pbPCrossroad
            // 
            this.pbPCrossroad.Image = global::tracy.Resource1.CrossPIcon;
            this.pbPCrossroad.Location = new System.Drawing.Point(116, 3);
            this.pbPCrossroad.Name = "pbPCrossroad";
            this.pbPCrossroad.Size = new System.Drawing.Size(86, 86);
            this.pbPCrossroad.TabIndex = 1;
            this.pbPCrossroad.TabStop = false;
            this.pbPCrossroad.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pbCCrossroad
            // 
            this.pbCCrossroad.BackgroundImage = global::tracy.Resource1.CrossC;
            this.pbCCrossroad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbCCrossroad.Image = global::tracy.Resource1.CrossCIcon;
            this.pbCCrossroad.InitialImage = null;
            this.pbCCrossroad.Location = new System.Drawing.Point(14, 3);
            this.pbCCrossroad.Name = "pbCCrossroad";
            this.pbCCrossroad.Size = new System.Drawing.Size(86, 86);
            this.pbCCrossroad.TabIndex = 0;
            this.pbCCrossroad.TabStop = false;
            this.pbCCrossroad.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelSettings
            // 
            this.panelSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSettings.Controls.Add(this.btnRemove);
            this.panelSettings.Controls.Add(this.label3);
            this.panelSettings.Controls.Add(this.label2);
            this.panelSettings.Controls.Add(this.btnStart);
            this.panelSettings.Controls.Add(this.trackBar2);
            this.panelSettings.Controls.Add(this.trackBar1);
            this.panelSettings.Location = new System.Drawing.Point(30, 262);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(231, 243);
            this.panelSettings.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::tracy.Resource1.Delete;
            this.btnRemove.Location = new System.Drawing.Point(116, 152);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(71, 71);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Adjust Traffic Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Adjust Traffic Flow";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::tracy.Resource1.Play;
            this.btnStart.Location = new System.Drawing.Point(14, 153);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(71, 71);
            this.btnStart.TabIndex = 2;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(36, 108);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.Value = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(36, 42);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.AutoScroll = true;
            this.panelGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGrid.Controls.Add(this.pbGrid);
            this.panelGrid.Location = new System.Drawing.Point(267, 28);
            this.panelGrid.MaximumSize = new System.Drawing.Size(1065, 915);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(746, 470);
            this.panelGrid.TabIndex = 2;
            // 
            // pbGrid
            // 
            this.pbGrid.Location = new System.Drawing.Point(0, 0);
            this.pbGrid.MaximumSize = new System.Drawing.Size(1060, 910);
            this.pbGrid.Name = "pbGrid";
            this.pbGrid.Size = new System.Drawing.Size(1050, 900);
            this.pbGrid.TabIndex = 0;
            this.pbGrid.TabStop = false;
            this.pbGrid.Click += new System.EventHandler(this.pictureBox9_Click);
            this.pbGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pbGrid_Paint);
            this.pbGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox9_MouseDown);
            // 
            // Tools
            // 
            this.Tools.AutoSize = true;
            this.Tools.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tools.Location = new System.Drawing.Point(111, 5);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(47, 20);
            this.Tools.TabIndex = 3;
            this.Tools.Text = "Tools";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Setting";
            // 
            // Tracy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.panelRoads);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Tracy";
            this.Text = "Tracy Traffic Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panelRoads.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbStraightV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStraightH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveSW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveSE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveNW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurveNE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPCrossroad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCCrossroad)).EndInit();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelRoads;
        private System.Windows.Forms.PictureBox pbStraightV;
        private System.Windows.Forms.PictureBox pbStraightH;
        private System.Windows.Forms.PictureBox pbCurveSW;
        private System.Windows.Forms.PictureBox pbCurveSE;
        private System.Windows.Forms.PictureBox pbCurveNW;
        private System.Windows.Forms.PictureBox pbCurveNE;
        private System.Windows.Forms.PictureBox pbPCrossroad;
        private System.Windows.Forms.PictureBox pbCCrossroad;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Tools;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.PictureBox pbGrid;
        public System.Windows.Forms.Timer timer1;
    }
}

