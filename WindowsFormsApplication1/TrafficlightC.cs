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
            switch (lightColor)
            {
                case 1:
                    if (place.X < Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedNorth, place.X, place.Y, 10, 20);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedEast, place.X, place.Y, 20, 10);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.RedSouth, place.X, place.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.RedWest, place.X, place.Y, 20, 10);
                    }
                    break;
                case 2:
                    if (place.X < Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowNorth, place.X, place.Y, 10, 20);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowEast, place.X, place.Y, 20, 10);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.YellowSouth, place.X, place.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.YellowWest, place.X, place.Y, 20, 10);
                    }
                    break;
                case 3:
                    if (place.X < Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenNorth, place.X, place.Y, 10, 20);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y < Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenEast, place.X, place.Y, 20, 10);
                    }
                    else if (place.X > Cplace.X + 75 && place.Y > Cplace.Y + 75)
                    {
                        g.DrawImage(Resource1.GreenSouth, place.X, place.Y, 10, 20);
                    }
                    else
                    {
                        g.DrawImage(Resource1.GreenWest, place.X, place.Y, 20, 10);
                    }
                    break;
            }
        }

        public override void IncTimer()
        {
            base.IncTimer();


            if ((timer == redInterval) && (lightColor == 1))
            // finish red light duration, switch color to green,reset timer
            {
                lightColor = 3;
                colorChanged = true;
                timer = 0;
            }
            else if ((timer == greenInterval - 3) && (lightColor == 3))
            // finish green light duration, switch color to yellow
            // assume the yellow light last for 3 seconds
            {
                lightColor = 2;
                colorChanged = true;
            }
            else if ((timer == greenInterval) && (lightColor == 2))
            // finish yellow light duration, switch color to red,reset timer
            {
                lightColor = 1;
                colorChanged = true;
                timer = 0;
            }
        }

    }
}
