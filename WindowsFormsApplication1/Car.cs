using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Timers;


namespace tracy
{
    public class Car
    {
        private int cruiseSpeed;
        private int speed;
        readonly int color;

        public Car(Point startV)
        {

            Random r = new Random();

            CruiseSpeed = r.Next(1, 5);
            Speed = CruiseSpeed;
            Coordinates = startV;
            color = r.Next(0, 4);

        }

        public Point Coordinates
        {
            get;set;
        }

        public int CruiseSpeed { get => cruiseSpeed; set => cruiseSpeed = value; }
        public int Speed { get => speed; set => speed = value; }

        public int Color => color;

        public void Draw(ref Graphics gr)
        {
            SolidBrush p;
            switch (this.Color)
            {
                case 0:
                    p = new SolidBrush(System.Drawing.Color.Red);
                    break;
                case 1:
                    p = new SolidBrush(System.Drawing.Color.Green);
                    break;

                case 2:
                    p = new SolidBrush(System.Drawing.Color.Blue);
                    break;
                case 3:
                    p = new SolidBrush(System.Drawing.Color.White);

                    break;
                default:
                    p = new SolidBrush(System.Drawing.Color.Yellow);
                    break;
            }

            gr.FillRectangle(p, new Rectangle(Coordinates.X, Coordinates.Y, 8, 8));

            p.Dispose();

        }

        public void accelerate()
        {
            if (CruiseSpeed < Speed)
            {
                Speed++;
            }
        }

        public void decellerate()
        {
            Speed--;
        }

    }
}
