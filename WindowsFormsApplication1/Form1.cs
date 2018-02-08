using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace tracy
{
    
    public partial class Tracy : Form
    {

        private bool start = false;
        private int roadId = 0;
        Simulator simu;
        Bitmap bmGridRoad = new Bitmap(1060, 910);
        Bitmap bmLights = new Bitmap(1060, 910);
        Bitmap bmCars = new Bitmap(1060, 910);
        internal Graphics bmGGridRoad, bmGLights;
        static internal Graphics bmGCars;

        private TextBox txtGrTime;
        private int[] justPlaced;
        private Point selected;
        private PictureBox pb;

        public bool Start { get => start; set => start = value; }

        public Tracy()
        {

            InitializeComponent();
            simu = new Simulator(150);
            timer1.Enabled = false;
            timer1.Interval = 20;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            simu.PaintLights(ref bmGLights);
            simu.PaintCars(ref bmGCars);
            this.pbGrid.Invalidate();
            GC.Collect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmGGridRoad = Graphics.FromImage(bmGridRoad);
            bmGGridRoad.Clear(Color.White);
            bmGLights = Graphics.FromImage(bmLights);
            bmGLights.Clear(Color.Transparent);
            bmGCars = Graphics.FromImage(bmCars);
            bmGCars.Clear(Color.Transparent);
            simu.DrawGrid(ref bmGGridRoad);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!Start)
            {
                clearPictureBox();
                btnStart.Image = Resource1.Stop;
                Start = true;
                timer1.Enabled = true;
                timer1.Start();
                simu.StartSim();
                bmGLights.Clear(Color.Transparent);
                bmGCars.Clear(Color.Transparent);
                simu.PaintLights(ref bmGLights);
                simu.PaintCars(ref bmGCars);
                pbGrid.Invalidate();
                if (pbGrid.Controls.Contains(txtGrTime))
                {
                    pbGrid.Controls.Remove(txtGrTime);
                    txtGrTime.Dispose();
                }
            }
            else
            {

                btnStart.Image = Resource1.Play;
                Start = false;
                simu.StopSim();
                bmGGridRoad.Clear(Color.White);
                bmGLights.Clear(Color.Transparent);
                bmGCars.Clear(Color.Transparent);
                simu.PaintAll(ref bmGGridRoad);
                simu.PaintLights(ref bmGLights);
                simu.PaintCars(ref bmGCars);
                timer1.Stop();
                pbGrid.Invalidate();
                pedestrianDone();

                this.trackBar1.Value = 5;
                this.trackBar2.Value = 5;

            }
            
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                roadId = 1;
                clearPictureBox();
                pbCCrossroad.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
        }

        private void clearPictureBox()
        {
            pbCCrossroad.BorderStyle = BorderStyle.None;
            pbPCrossroad.BorderStyle = BorderStyle.None;
            pbCurveNE.BorderStyle = BorderStyle.None;
            pbCurveSE.BorderStyle = BorderStyle.None;
            pbCurveNW.BorderStyle = BorderStyle.None;
            pbCurveSW.BorderStyle = BorderStyle.None;
            pbStraightH.BorderStyle = BorderStyle.None;
            pbStraightV.BorderStyle = BorderStyle.None;

        }

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            if (pbGrid.Controls.Contains(txtGrTime))
            {
                pbGrid.Controls.Remove(txtGrTime);
                txtGrTime.Dispose();
            }
            if (!Start)
            {
                int[] cell = simu.getSpotNumber(e.Location);
                if (roadId == 0)
                {
                    
                    Point roadCell=simu.existCrossRoad(cell);
                    if(roadCell != new Point(-10,-10))
                    {
                        txtGrTime = new TextBox();
                        txtGrTime.KeyPress += new KeyPressEventHandler(OnKeyPress);
                        pbGrid.Controls.Add(txtGrTime);
                        txtGrTime.BorderStyle = BorderStyle.Fixed3D;
                        txtGrTime.Location = new Point(roadCell.X, roadCell.Y);
                        txtGrTime.Size = new Size(100, 20);
                        txtGrTime.Visible = true;
                        txtGrTime.Size = new System.Drawing.Size(25, 20);
                        txtGrTime.Focus();
                        txtGrTime.BackColor = Color.Aqua;
                        txtGrTime.Show();
                        selected = e.Location;
                    }
                }

                switch (roadId)
                {
                    case -1:
                        {
                            simu.RemoveRoad(e.Location);
                            bmGGridRoad.Clear(Color.White);
                            bmGCars.Clear(Color.Transparent);
                            bmGLights.Clear(Color.Transparent);
                            simu.DrawGrid(ref bmGGridRoad);
                            simu.PaintAll(ref bmGGridRoad);
                            simu.PaintLights(ref bmGLights);
                            roadId = 0;
                            pbGrid.Invalidate();
                            clearPictureBox();
                            break;
                           
                        }
                    case 1:
                        {
                            simu.AddRoad(e.Location, "CCrossroad", "", ref bmGLights);
                            roadId = 0;
                            pbCCrossroad.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 2:
                        {
                            simu.AddRoad(e.Location, "PCrossroad", "", ref bmGLights);
                            roadId = 0;
                            pbPCrossroad.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 3:
                        {
                            simu.AddRoad(e.Location, "Curve", "NE", ref bmGLights);
                            roadId = 0;
                            pbCurveNE.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 4:
                        {
                            simu.AddRoad(e.Location, "Curve", "NW", ref bmGLights);
                            roadId = 0;
                            pbCurveNW.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 5:
                        {
                            simu.AddRoad(e.Location, "Curve", "SE", ref bmGLights);
                            roadId = 0;
                            pbCurveSE.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 6:
                        {
                            simu.AddRoad(e.Location, "Curve", "SW", ref bmGLights);
                            roadId = 0;
                            pbCurveSW.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 7:
                        {
                            simu.AddRoad(e.Location, "Straight", "H", ref bmGLights);
                            roadId = 0;
                            pbStraightH.BorderStyle = BorderStyle.None;
                            break;
                        }
                    case 8:
                        {
                            simu.AddRoad(e.Location, "Straight", "V", ref bmGLights);
                            roadId = 0;
                            pbStraightV.BorderStyle = BorderStyle.None;
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }

                if (roadId > 0)
                {
                    justPlaced = simu.getSpotNumber(e.Location);
                }

                simu.PaintAll(ref bmGGridRoad);
                simu.PaintLights(ref bmGLights);
                pbGrid.Invalidate();
            }
            else
            {
                    if(simu.pressSensor(e.Location))
                    {
                        Point x = simu.Pcross(e.Location);
                        pb = new PictureBox();
                        pedestrianDone();
                        pbGrid.Controls.Add(pb);
                        pb.BackColor = Color.Yellow;
                        pb.Visible = true;
                        pb.Location = x;
                        pb.Size = new Size(10, 10);
                    }
            }

        }

        internal void pedestrianDone()
        {
            if (pbGrid.Controls.Contains(pb))
            {
                pbGrid.Controls.Remove(pb);
                pb.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
                roadId = 2;
                clearPictureBox();
                pbPCrossroad.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            roadId = 3;
            clearPictureBox();
            pbCurveNE.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            roadId = 4;
            clearPictureBox();
            pbCurveNW.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            roadId = 5;
            clearPictureBox();
            pbCurveSE.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            roadId = 6;
            clearPictureBox();
            pbCurveSW.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            roadId = 7; 
            clearPictureBox();
            pbStraightH.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            roadId = 8;
            clearPictureBox();
            pbStraightV.BorderStyle = BorderStyle.Fixed3D;
        }

        private void panel3_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.roadId = -1;
        }

        private void panelGrid_Resize(object sender, EventArgs e)
        {

        }

        private void pbGrid_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmGridRoad, 0, 0);
            e.Graphics.DrawImage(bmLights, 0, 0);
            e.Graphics.DrawImage(bmCars, 0, 0);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string x = txtGrTime.Text;
                if (Convert.ToInt32(x) >= 3)
                {
                    simu.adjustGreenTime(selected, Convert.ToInt32(x));
                    pbGrid.Controls.Remove(txtGrTime);
                    txtGrTime.Visible = false;
                }
                else
                {
                    MessageBox.Show("The value entered is too small, this will make a traffic jam...","Timer value invalid");
                }
            }
            else if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != (char)Keys.Back))
            {
                e.KeyChar = (char)0;
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.panelSettings.Location = new Point(30, 450); 
                this.label1.Location = new Point(111, 427);
                this.panelRoads.Size += new Size(0,188); 
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                this.panelSettings.Location = new Point(30, 262);
                this.label1.Location = new Point(111, 239);
                this.panelRoads.Size -= new Size(0, 188);
            }
        }

        private void trackBar1_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            simu.AdjustSpawnTime(-(trackBar1.Value - 10));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            simu.AdjustCarSpeed(-(trackBar2.Value - 10));
        }

    }
}
