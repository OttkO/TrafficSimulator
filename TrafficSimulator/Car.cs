using System;
using System.Drawing;


namespace TrafficSimulator
{
  public class Car
  {
    private int _cruiseSpeed;
    private int _speed;
    readonly int _color;

    public Car(Point startV)
    {
      Random r = new Random();

      CruiseSpeed = r.Next(1, 5);
      Speed = CruiseSpeed;
      Coordinates = startV;
      _color = r.Next(0, 4);
    }

    public Point Coordinates { get; set; }

    public int CruiseSpeed { get => _cruiseSpeed; set => _cruiseSpeed = value; }

    public int Speed { get => _speed; set => _speed = value; }

    public int Color => _color;

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

    public void Accelerate()
    {
      if (CruiseSpeed < Speed)
      {
        Speed++;
      }
    }

    public void Decellerate()
    {
      Speed--;
    }

  }
}
