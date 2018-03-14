using System.Drawing;

namespace TrafficSimulator
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
            g.DrawImage(new Bitmap(Properties.Resources.RedNorth), Position.X, Position.Y, 10, 20);

          }
          else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.RedEast), Position.X, Position.Y, 20, 10);
          }
          else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.RedSouth), Position.X, Position.Y, 10, 20);
          }
          else
          {
            g.DrawImage(new Bitmap(Properties.Resources.RedWest), Position.X, Position.Y, 20, 10);
          }
          break;
        case 2:
          if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.YellowNorth), Position.X, Position.Y, 10, 20);
          }
          else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.YellowEast), Position.X, Position.Y, 20, 10);
          }
          else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.YellowSouth), Position.X, Position.Y, 10, 20);
          }
          else
          {
            g.DrawImage(new Bitmap(Properties.Resources.YellowWest), Position.X, Position.Y, 20, 10);
          }
          break;
        case 3:
          if (Position.X < Cplace.X + 75 && Position.Y < Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.GreenNorth), Position.X, Position.Y, 10, 20);
          }
          else if (Position.X > Cplace.X + 75 && Position.Y < Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.GreenEast), Position.X, Position.Y, 20, 10);
          }
          else if (Position.X > Cplace.X + 75 && Position.Y > Cplace.Y + 75)
          {
            g.DrawImage(new Bitmap(Properties.Resources.GreenSouth), Position.X, Position.Y, 10, 20);
          }
          else
          {
            g.DrawImage(new Bitmap(Properties.Resources.GreenWest), Position.X, Position.Y, 20, 10);
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
