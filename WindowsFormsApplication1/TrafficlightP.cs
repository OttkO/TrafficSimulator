using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace tracy
{
    public class TrafficLightP : TrafficLight
    {       
        public TrafficLightP(int greenINT,Point location) : base(greenINT,location)
        {
            this.greenInterval = 7;
        }

        public override void Draw(ref Graphics g)
        {
        
            Point ped = place;
            ped.X = ped.X - 40;
            ped.Y = ped.Y + 40;


            if ((((lightColor == 3) || (lightColor == 2))))
            {
                if (place.X < Cplace.X + 75 && place.Y < Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedGreenWest, place.X, place.Y, 20, 10);
                }
                else if (place.X > Cplace.X + 75 && place.Y < Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedGreenEast, place.X, place.Y, 20, 10);
                }
                else if (place.X > Cplace.X + 75 && place.Y > Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedGreenEast, place.X, place.Y, 20, 10);
                }
                else if (place.X < Cplace.X + 75 && place.Y > Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedGreenWest, place.X, place.Y, 20, 10);
                }

            }
            else
            {
                if (place.X < Cplace.X + 75 && place.Y < Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedRedWest, place.X, place.Y, 20, 10);
                }
                else if (place.X > Cplace.X + 75 && place.Y < Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedRedEast, place.X, place.Y, 20, 10);
                }
                else if (place.X > Cplace.X + 75 && place.Y > Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedRedEast, place.X, place.Y, 20, 10);
                }
                else if (place.X < Cplace.X + 75 && place.Y > Cplace.Y + 75)
                {
                    g.DrawImage(Resource1.PedRedWest, place.X, place.Y, 20, 10);
                }
            }
            
        }


















    }
}
