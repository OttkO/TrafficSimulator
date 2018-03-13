using System;
using System.Drawing;
using tracy;

namespace TrafficSimulator
{
  public class Pedestrian
  {
    private Point _position;
    private Point _oriPosition;

    public void Move(object s, EventArgs e)
    {
      // TODO: This is UI shit, and should not belong in here..
      // Brainstorm on how we can divide UI and data but still be able to test and work with our shit

      Graphics g = null; //= Tracy.bmGCars;
      g.Clear(Color.Transparent);

      Fin = false;

      Brush b = new SolidBrush(Color.Blue);
      if (_oriPosition.X < Cpost.X + 75)
      {
        _position.X = _position.X + 4;
        g.FillEllipse(b, _position.X, _position.Y, 5, 5);

        if (_position.X == Cpost.X + 102)
        {
          _oriPosition = _position;
          Fin = true;
        }
      }
      else
      {
        _position.X = _position.X - 4;
        g.FillEllipse(b, _position.X, _position.Y, 5, 5);

        if (_position.X == Cpost.X + 42)
        {
          _oriPosition = _position;
          Fin = true;
        }
      }
    }

    public Point Pos
    {
      get => _position;
      set
      {
        _position = value;
        _oriPosition = value;
      }
    }

    public Point OriPosition => _oriPosition;

    public Point Cpost { get; set; }

    public bool Fin { get; set; }

    public void Draw(ref Graphics g)
    {
      Brush b = new SolidBrush(Color.Blue);
      g.FillEllipse(b, _position.X, _position.Y, 5, 5);
    }
  }
}
