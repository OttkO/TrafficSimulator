using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tracy
{
    public class Pedestrian
    {
        private Point position;
        private Point Oriposition;
        private Point Cpost;
        private bool finished = false;

        public void Move(object s, EventArgs e)
        {
            
            Graphics g = Tracy.bmGCars;
            g.Clear(Color.Transparent);
            Brush b = new SolidBrush(Color.Blue);
            if (Oriposition.X < Cpost.X + 75)
            {
                position.X = position.X + 4;
                g.FillEllipse(b, position.X, position.Y, 5, 5);
                if (position.X == Cpost.X + 102)
                {
                    Oriposition = position;
                    finished = true;
                }
            }
            else
            {
                position.X = position.X - 4;
                g.FillEllipse(b, position.X, position.Y, 5, 5);
                if (position.X == Cpost.X + 42)
                {
                    Oriposition = position;
                    finished = true;
                }
            }

        }

        public Point pos
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                Oriposition = value;
            }
        }

        public Point oriPosition
        {
            get
            {
                return Oriposition;
            }
        }

        public Point Cpos
        {
            get
            {
                return Cpost;
            }
            set
            {
                Cpost = value;
            }
        }

        public bool fin
        {
            get
            {
                return finished;
            }
            set
            {
                finished = value;
            }
        }

        public void draw(ref Graphics g)
        {
            Brush b = new SolidBrush(Color.Blue);
            g.FillEllipse(b, position.X, position.Y, 5, 5);
        }


    }
}
