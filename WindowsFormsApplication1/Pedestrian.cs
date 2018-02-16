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

        public void Move(object s, EventArgs e)
        {
            
            Graphics g = Tracy.bmGCars;
            g.Clear(Color.Transparent);
            this.Fin = false;
            Brush b = new SolidBrush(Color.Blue);
            if (Oriposition.X < Cpost.X + 75)
            {
                position.X = position.X + 4;
                g.FillEllipse(b, position.X, position.Y, 5, 5);
                if (position.X == Cpost.X + 102)
                {
                    Oriposition = position;
                    this.Fin = true;
                }
            }
            else
            {
                position.X = position.X - 4;
                g.FillEllipse(b, position.X, position.Y, 5, 5);
                if (position.X == Cpost.X + 42)
                {
                    Oriposition = position;
                    this.Fin = true;
                }
            }

        }

        public Point Pos
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

        public Point OriPosition
        {
            get
            {
                return Oriposition;
            }
        }

        public Point Cpost
        {
            get;
            set;
     
        }

        public bool Fin
        {
            get;set;
        }

        public void Draw(ref Graphics g)
        {
            Brush b = new SolidBrush(Color.Blue);
            g.FillEllipse(b, position.X, position.Y, 5, 5);
        }


    }
}
