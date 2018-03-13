using System.Drawing;

namespace TrafficSimulator
{
  public class TrafficLight
  {
    protected Point Cplace;

    public int GreenInterval
    {
      get; set;
    }
    public int RedInterval
    {
      get; set;
    }
    public int Timer
    {
      get; set;
    }

    public Point Position
    {
      get; set;
    }

    public Point CPost
    {
      get; set;
    }

    public void AdjustGreenTime(int greentime)
    {

      if (Timer == GreenInterval / 2)
      {
        Timer = greentime / 2;
      }
      else if (Timer == GreenInterval * 2)
      {
        Timer = greentime * 2;
      }
      else if (Timer == GreenInterval)
      {
        Timer = greentime;
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
      Position = location;
      Colour = 1;
      GreenInterval = greenINT;
      Timer = 0;
      RedInterval = 0;
      ColorChanged = false;
    }
    // 1 = red , 2 = yellow ,  3 = green
    public int Colour
    {
      get; set;
    }

    public bool ColorChanged
    {
      get; set;
    }


    public virtual void Draw(ref Graphics g)
    {
    }

    public virtual void IncTimer()
    {
      this.Timer++;
    }
  }

}
