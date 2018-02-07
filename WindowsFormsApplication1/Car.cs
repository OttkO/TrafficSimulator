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
        public int cruiseSpeed;
        public int speed;
        private Point coordinates;
        int color;
        //int angle;
        //string direction;

        public Car(Point startV)
        {

            Random r = new Random();

            cruiseSpeed = r.Next(1, 5);
            speed = cruiseSpeed;
            coordinates = startV;
            color = r.Next(0, 4);

        }

        public Point Coordinates
        {
            get { return this.coordinates; }
            set { this.coordinates = value; }
        }


        public void Draw(ref Graphics gr)
        {
            SolidBrush p;
            switch (this.color)
            {
                case 0:
                    p = new SolidBrush(Color.Red);
                    break;
                case 1:
                    p = new SolidBrush(Color.Green);
                    break;

                case 2:
                    p = new SolidBrush(Color.Blue);
                    break;
                case 3:
                    p = new SolidBrush(Color.White);

                    break;
                default:
                    p = new SolidBrush(Color.Yellow);
                    break;
            }

            gr.FillRectangle(p, new Rectangle(coordinates.X, coordinates.Y, 8, 8));

            p.Dispose();

        }

        public void accelerate()
        {
            if (cruiseSpeed < speed)
            {
                speed++;
            }
        }

        public void decellerate()
        {
            speed--;
        }

    }
}
