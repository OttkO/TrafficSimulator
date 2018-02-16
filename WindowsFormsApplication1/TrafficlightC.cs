using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace tracy
{
    public class TrafficLightC : TrafficLight
    {
        public TrafficLightC(int greenINT, Point location)
            : base(greenINT, location)
        {
            
        }

        public override void Draw(ref Graphics g)
        {
            switch (Colour)
            {
                case 1:
                    if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedNorth, Position.X, Position.Y, 10, 20);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedEast, Position.X, Position.Y, 20, 10);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedSouth, Position.X, Position.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.RedWest, Position.X, Position.Y, 20, 10);
                    }
                    break;
                case 2:
                    if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowNorth, Position.X, Position.Y, 10, 20);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowEast, Position.X, Position.Y, 20, 10);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowSouth, Position.X, Position.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.YellowWest, Position.X, Position.Y, 20, 10);
                    }
                    break;
                case 3:
                    if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenNorth, Position.X, Position.Y, 10, 20);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenEast, Position.X, Position.Y, 20, 10);
                    }
                    else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenSouth, Position.X, Position.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.GreenWest, Position.X, Position.Y, 20, 10);
                    }
                    break;
            }
        }

        public override void IncTimer()
        {
            base.IncTimer();


            if ((Timer == RedInterval) && (Colour == 1))
            // finish red light duration, switch color to green,reset timer
            {
                Colour = 3;
                ColorChanged = true;
                Timer = 0;
            }
            else if ((Timer == GreenInterval - 3) && (Colour == 3))
            // finish green light duration, switch color to yellow
            // assume the yellow light last for 3 seconds
            {
                Colour = 2;
                ColorChanged = true;
            }
            else if ((Timer == GreenInterval) && (Colour == 2))
            // finish yellow light duration, switch color to red,reset timer
            {
                Colour = 1;
                ColorChanged = true;
                Timer = 0;
            }
        }

    }
}
