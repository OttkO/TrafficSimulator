﻿using System.Drawing;

namespace TrafficSimulator
{
  public class TrafficLightP : TrafficLight
  {
    public TrafficLightP(int greenINT, Point location) : base(greenINT, location)
    {
      this.GreenInterval = 7;
    }

    public override void Draw(ref Graphics g)
    {
      Point ped = Position;
      ped.X = ped.X - 40;
      ped.Y = ped.Y + 40;

      if ((Colour == 3) || (Colour == 2))
      {
        if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedGreenWest), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedGreenEast), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedGreenEast), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X < Cplace.X + 75 && Position.Y > Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedGreenWest), Position.X, Position.Y, 20, 10);
        }
      }
      else
      {
        if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedRedWest), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedRedEast), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedRedEast), Position.X, Position.Y, 20, 10);
        }
        else if (Position.X < Cplace.X + 75 && Position.Y > Cplace.Y + 75)
        {
          g.DrawImage(new Bitmap(Properties.Resources.PedRedWest), Position.X, Position.Y, 20, 10);
        }
      }
    }
  }
}