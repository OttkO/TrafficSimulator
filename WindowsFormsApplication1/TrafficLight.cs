using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace tracy
{
    public class TrafficLight
    {


        protected Point place;
        protected int lightColor; // 1 = red , 2 = yellow ,  3 = green
        protected int GreenInterval;
        protected int RedInterval;
        protected int timer;
        protected bool colorChanged;
        protected Point Cplace;

        public int greenInterval
        {
            get { return GreenInterval; }
            set { GreenInterval = value; }
        }
        public int redInterval
        {
            get { return RedInterval; }
            set { RedInterval = value; }
        }
        public int Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public Point position
        {

            set { place = value; }
            get { return place; }
        }

        public Point CPost
        {
            get { return Cplace; }
            set { Cplace = value; }
        }

        public void AdjustGreenTime(int greentime)
        {
            if (Timer == 0)
            {
                timer = 0;
            }
            else if (Timer == GreenInterval / 2)
            {
                timer = greentime / 2;
            }
            else if (Timer == GreenInterval * 2)
            {
                timer = greentime * 2;
            }
            else if (Timer == GreenInterval)
            {
                timer = greentime;
            }

            if (RedInterval / GreenInterval == 3)
            {
                GreenInterval = greentime;
                RedInterval = 3 * GreenInterval;

            }
            else if (RedInterval / GreenInterval == 1)
            {
                GreenInterval = greentime;
                RedInterval = GreenInterval;
            }
        }

        public TrafficLight(int greenINT, Point location)
        {
            place = location;
            lightColor = 1;
            GreenInterval = greenINT;
            timer = 0;
            RedInterval = 0;
            colorChanged = false;
        }

        public TrafficLight()
        {
            // TODO: Complete member initialization
        }
        public int Colour
        {
            get { return this.lightColor; }
            set { lightColor = value; }
        }

        public bool ColorChanged
        {
            get { return this.colorChanged; }
            set { this.colorChanged = value; }
        }


        public virtual void Draw(ref Graphics g)
        {

            //Graphics g = pb.CreateGraphics();
            //Rectangle r = new Rectangle(10, 10, 60, 180);
            //g.FillRectangle(Brushes.LightGray, r);
            //Rectangle r1 = new Rectangle(10, 10, 60, 60);
            //Rectangle r2 = new Rectangle(10, 70, 60, 60);
            //Rectangle r3 = new Rectangle(10, 130, 60, 60);


            //switch (lightColor)
            //{
            //    case 1:
            //        g.FillEllipse(Brushes.Red, r1);
            //        g.FillEllipse(Brushes.Black, r2);
            //        g.FillEllipse(Brushes.Black, r3);
            //        break;
            //    case 2:
            //        g.FillEllipse(Brushes.Black, r1);
            //        g.FillEllipse(Brushes.Yellow, r2);
            //        g.FillEllipse(Brushes.Black, r3);
            //        break;
            //    case 3:
            //        g.FillEllipse(Brushes.Black, r1);
            //        g.FillEllipse(Brushes.Black, r2);
            //        g.FillEllipse(Brushes.Green, r3);
            //        break;
            //}
            //if (timer < 60 - GreenInterval - 3)
            //{
            //    lightColor = 1;
            //}
            //if (timer == 60 - GreenInterval - 3)
            //{
            //    lightColor = 2;

            //}
            //else if (timer == 60 - GreenInterval)
            //{
            //    lightColor = 3;


            //}
            //else if ((timer >= 60) && (timer < 64))
            //{
            //    lightColor = 2;
            //}
            //else if (timer >= 64)
            //{
            //    timer = 0;
            //}


        }

        public virtual void IncTimer()
        {
            this.timer++;
        }
    }

}
