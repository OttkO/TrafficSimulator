using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace TrafficSimulator
{
  public class Lane
  {
    //Spawning stuff
    private readonly Timer _spawnTimer;
    private readonly Timer _moveTimer;

    //Positioning stuff
    private readonly int _numberOfPositions;
    private readonly string _direction;
    private readonly int _increments;
    private readonly Point _start;
    private readonly Point _end;
    private readonly Point[] _positions;


    //Traffic stuff
    private TrafficLightC _trafficLight;
    private readonly List<Lane> _connectedLanes;
    private readonly List<Car> _cars;
    private readonly List<Car> _carsInQueue;
    private readonly int _carLimit;


    /// <summary>
    /// Constructor of lane.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="hasLight">Indicates if lane has a traffic light.</param>
    /// <param name="pStart"></param>
    /// <param name="pEnd"></param>
    /// <param name="direction"></param>
    public Lane(Timer time, bool hasLight, Point pStart, Point pEnd, string direction)
    {
      //initialize fields
      _numberOfPositions = 150;
      _cars = new List<Car>();
      _connectedLanes = new List<Lane>();
      _carsInQueue = new List<Car>();
      _positions = new Point[_numberOfPositions];
      _start = pStart;
      _end = pEnd;
      _direction = direction;

      //spawning
      Spawnable = true;

      _spawnTimer = time;
      SpawnTime = (int)_spawnTimer.Interval;

      _moveTimer = new Timer { Interval = 25 };
      _moveTimer.Elapsed += MoveCarsOnTimerElapsed;

      _carLimit = 14;

      _spawnTimer.Elapsed += SpawnCarsOnTimerElapsed;
      _moveTimer.Start();

      //increments of positions
      //Define positions depending on direction
      switch (direction)
      {
        case "V":
          //150 was: Math.Abs(pStart.Y - pEnd.Y)
          _increments = 150 / _numberOfPositions;
          //If lane is going from top to bottom then Y is ascending.
          if (pStart.Y < pEnd.Y)
          {
            for (int i = 0; i < _numberOfPositions; i++)
            {
              _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);

            }

          }
          else
          {
            for (int i = 0; i < _numberOfPositions; i++)
            {
              _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);

            }

          }

          break;
        case "H":
          _increments = 150 / _numberOfPositions;
          //If lane is going from top to bottom then Y is ascending.
          if (pStart.X < pEnd.X)
          {
            for (int i = 0; i < _numberOfPositions; i++)
            {
              _positions[i] = new Point(pStart.X + i * _increments, pStart.Y);

            }

          }
          else
          {
            _increments = 150 / _numberOfPositions;
            for (int i = 0; i < _numberOfPositions; i++)
            {
              _positions[i] = new Point(pStart.X - i * _increments, pStart.Y);

            }

          }
          break;
        case "NE":
          //150 was: Math.Abs(pStart.X - pEnd.X)
          _increments = 150 / _numberOfPositions;
          //If lane is going from top to bottom then Y is ascending.
          if (pStart.X < pEnd.X)
          {
            _positions = new Point[_numberOfPositions + 10];
            for (int i = 0; i < (_numberOfPositions / 2) + 5; i++)
            {
              _positions[i] = new Point(pStart.X - 5, pStart.Y + (i * _increments) - 5);

            }

            for (int i = (_numberOfPositions / 2) + 5; i < _numberOfPositions + 10; i++)
            {
              _positions[i] = new Point(pStart.X - 5 + ((i - ((_numberOfPositions / 2) + 5)) * _increments), pStart.Y + (80 * _increments));

            }

            _numberOfPositions += 10;
          }
          else
          {
            _carLimit = 12;
            _positions = new Point[_numberOfPositions - 20];
            for (int i = 0; i < (_numberOfPositions / 2) - 10; i++)
            {
              _positions[i] = new Point(pStart.X - i * _increments, pStart.Y - 5);

            }

            for (int i = (_numberOfPositions / 2) - 10; i < _numberOfPositions - 20; i++)
            {
              _positions[i] = new Point(pStart.X - (65 * _increments), pStart.Y - 5 - (i - ((_numberOfPositions / 2) - 10) * _increments));

            }

            _numberOfPositions -= 20;

          }

          break;

        case "NW":
          //150 was: Math.Abs(pStart.X - pEnd.X)
          _increments = 150 / _numberOfPositions;

          //Lane 0 postitions
          if (pStart.X > pEnd.X)
          {
            _carLimit = 12;
            _positions = new Point[_numberOfPositions - 20];
            for (int i = 0; i < (_numberOfPositions / 2) - 15; i++)
            {
              _positions[i] = new Point(pStart.X - 5, pStart.Y + (i * _increments));

            }

            for (int i = (_numberOfPositions / 2) - 15; i < _numberOfPositions - 20; i++)
            {
              _positions[i] = new Point(pStart.X - 5 - ((i - 60) * _increments), pStart.Y + (60 * _increments));

            }
            _numberOfPositions -= 20;

          }
          //Lane 1 positions
          else
          {
            _positions = new Point[_numberOfPositions - 10];
            for (int i = 0; i < (_numberOfPositions / 2) - 10; i++)
            {
              _positions[i] = new Point(pStart.X + i * _increments, pStart.Y);
            }

            for (int i = (_numberOfPositions / 2) - 10; i < (_numberOfPositions / 2) + 10; i++)
            {
              _positions[i] = new Point(pStart.X + i * _increments, pStart.Y - ((i - 60) * _increments));
            }


            for (int i = (_numberOfPositions / 2) + 10; i < _numberOfPositions - 10; i++)
            {
              _positions[i] = new Point(pStart.X + 7 + (75 * _increments), pStart.Y - ((i - (_numberOfPositions / 2) + 20) * _increments));
            }

            _numberOfPositions -= 10;
          }

          break;
        case "SE":
          _increments = 150 / _numberOfPositions;
          //If lane is going from top to bottom then Y is ascending.
          if (pStart.X > pEnd.X)
          {

            _positions = new Point[_numberOfPositions + 10];
            for (int i = 0; i < (_numberOfPositions / 2) + 10; i++)
            {
              _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y - 5);

            }

            for (int i = (_numberOfPositions / 2) + 10; i < _numberOfPositions + 10; i++)
            {
              _positions[i] = new Point(pStart.X - (90 * _increments), pStart.Y + ((i - 90) * _increments));

            }

            _numberOfPositions += 10;
          }
          else
          {
            _carLimit = 12;
            _positions = new Point[_numberOfPositions - 20];
            for (int i = 0; i < (_numberOfPositions / 2) - 10; i++)
            {
              _positions[i] = new Point(pStart.X, pStart.Y - (i * _increments) - 5);

            }

            for (int i = (_numberOfPositions / 2) - 10; i < _numberOfPositions - 20; i++)
            {
              _positions[i] = new Point(pStart.X + (i - 65 * _increments), pStart.Y - (65 * _increments) - 5);

            }
            _numberOfPositions -= 20;

          }
          break;
        case "SW":

          _increments = 150 / _numberOfPositions;
          //If lane is going from top to bottom then Y is ascending.
          if (pStart.X < pEnd.X)
          {
            _positions = new Point[_numberOfPositions - 20];
            for (int i = 0; i < (_numberOfPositions / 2) - 10; i++)
            {
              _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - 5);
            }

            for (int i = (_numberOfPositions / 2) - 10; i < _numberOfPositions - 20; i++)
            {
              _positions[i] = new Point(pStart.X + (65 * _increments), pStart.Y + ((i - 65) * _increments));
            }
            _numberOfPositions -= 20;
          }
          else
          {
            _carLimit = 12;
            _positions = new Point[_numberOfPositions + 20];
            _numberOfPositions += 20;
            for (int i = 0; i < ((_numberOfPositions - 10) / 2) + 10; i++)
            {
              _positions[i] = new Point(pStart.X, pStart.Y - (i * _increments));
            }

            for (int i = ((_numberOfPositions - 10) / 2) + 10; i < _numberOfPositions; i++)
            {
              _positions[i] = new Point(pStart.X - (i - 90 * _increments), pStart.Y - (90 * _increments));
            }
          }
          break;
      }

      if (hasLight)
      {
        TrafficLight = new TrafficLightC(5, new Point(1, 1));
      }
    }

    public void ClearCarList()
    {
      _cars.Clear();
      _carsInQueue.Clear();
    }

    public Lane(Timer time, bool hasLight, Point pStart, Point pEnd, string direction, int laneNumber)
    {
      //initialize fields
      this._numberOfPositions = 150;
      _cars = new List<Car>();
      _connectedLanes = new List<Lane>();
      _carsInQueue = new List<Car>();
      _positions = new Point[_numberOfPositions];
      _start = pStart;
      _end = pEnd;
      _direction = direction;

      //spawning
      Spawnable = true;
      SpawnTime = 3000;
      _spawnTimer = time;
      _spawnTimer.Interval = SpawnTime;

      _moveTimer = new Timer();
      _moveTimer.Interval = 25;
      _moveTimer.Elapsed += MoveCarsOnTimerElapsed;

      _spawnTimer.Elapsed += SpawnCarsOnTimerElapsed;
      _moveTimer.Start();

      //increments of positions
      //Define positions depending on direction
      switch (direction)
      {
        case "CrossC":
          {
            _increments = 1;
            if ((laneNumber == 2) || (laneNumber == 6))
            {
              // Added by JP:
              _carLimit = 4;
              _numberOfPositions = 40;
              _positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
            }
            else
                if (laneNumber == 7)
            {
              // Added by JP:
              _carLimit = 3;
              _numberOfPositions = 40;
              _positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 4); i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y - (i * _increments));
              }

              for (int i = (_numberOfPositions / 4); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (15 * _increments), pStart.Y - (i * _increments));
              }
            }
            else
                    if ((laneNumber == 8) || (laneNumber == 0))
            {
              // Added by JP:
              _carLimit = 4;
              _numberOfPositions = 40;
              _positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + (i * _increments));
              }
            }
            else if (laneNumber == 1)
            {
              // Added by JP:
              _carLimit = 3;
              _numberOfPositions = 40;
              _positions = new Point[_numberOfPositions];
              for (int i = 0; i < 9; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y + (i * _increments));
              }

              for (int i = 9; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (9 * _increments), pStart.Y + (i * _increments));
              }
            }
            else
                            if ((laneNumber == 11))
            {
              //Added by JP:
              this._carLimit = 3;
              this._numberOfPositions = 45;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y);
              }
            }
            else
                                if ((laneNumber == 3))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y);
              }
            }
            else if (laneNumber == 4)
            {
              //Added by JP:
              this._carLimit = 3;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 4) * 0.7; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y + (i * _increments));
              }

              for (int i = (int)((_numberOfPositions / 4) * 0.7); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y + (7 * _increments));
              }
            }
            else
                if ((laneNumber == 5) || (laneNumber == 9))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y);
              }
            }
            else if (laneNumber == 10)
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions / 4) * 1.3); i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - (i * _increments));
              }

              for (int i = (int)((_numberOfPositions / 4) * 1.3); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - (13 * _increments));
              }
            }
            else if (laneNumber == 12)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y + (i * _increments));
              }
            }
            else if (laneNumber == 13)
            {
              _carLimit = 6;
              this._numberOfPositions = 50;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions) / 4; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + (i * _increments));
              }

              for (int i = (int)Math.Floor((_numberOfPositions) / 4.0); i < (_numberOfPositions / 3) * 1.5; i++)
              {
                _positions[i] = new Point(pStart.X - ((i - 12) * _increments), pStart.Y + (i * _increments));
              }

              for (int i = (int)((_numberOfPositions / 3) * 1.5); i < (_numberOfPositions); i++)
              {
                _positions[i] = new Point(pStart.X - (12 * _increments), pStart.Y + (i * _increments));
              }
            }
            else if (laneNumber == 14)
            {
              _carLimit = 6;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions - 10; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + (i * _increments));
              }

              for (int i = _numberOfPositions - 10; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + ((i - 30) * _increments), pStart.Y + (i * _increments));
              }
            }
            else if (laneNumber == 15)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y - (i * _increments));
              }
            }
            else if (laneNumber == 16)
            {
              _carLimit = 6;
              this._numberOfPositions = 50;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 4); i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y);
              }

              for (int i = (int)Math.Floor((_numberOfPositions) / 4.0); i < ((_numberOfPositions) / 3) * 1.5; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y - ((i - 12) * _increments));
              }

              for (int i = (int)(((_numberOfPositions) / 3) * 1.5); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y - (12 * _increments));
              }
            }
            else if (laneNumber == 17)
            {
              this._numberOfPositions = 50;
              _carLimit = 6;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 2); i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y);
              }

              for (int i = ((_numberOfPositions) / 2); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y + ((i - 25) * _increments));
              }
            }
            else if (laneNumber == 18)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions / 2; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - (i * _increments));
              }
              for (int i = _numberOfPositions / 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - (5 * _increments));
              }
            }
            else if (laneNumber == 19)
            {
              _carLimit = 6;
              this._numberOfPositions = 50;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 2) / 2; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - (i * _increments));
              }

              for (int i = ((_numberOfPositions) / 2) / 2; i < ((_numberOfPositions) / 3) * 1.5; i++)
              {
                _positions[i] = new Point(pStart.X + ((i - 12) * _increments), pStart.Y - (i * _increments));
              }

              for (int i = (int)(((_numberOfPositions) / 3) * 1.5); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (12 * _increments), pStart.Y - (i * _increments));
              }
            }
            else if (laneNumber == 20)
            {
              _carLimit = 6;
              this._numberOfPositions = 45;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 2); i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - (i * _increments));
              }

              for (int i = (int)Math.Floor((_numberOfPositions) / 2.0); i < (_numberOfPositions); i++)
              {
                _positions[i] = new Point(pStart.X - ((i - 25) * _increments), pStart.Y - (i * _increments));
              }
            }
            else if (laneNumber == 21)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y + (i * _increments));
              }
            }
            else if (laneNumber == 22)
            {
              this._numberOfPositions = 50; _carLimit = 6;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < ((_numberOfPositions) / 2) / 2; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y);
              }

              for (int i = (int)Math.Floor((_numberOfPositions) / 4.0); i < ((_numberOfPositions) / 3) * 2; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y + ((i - 12) * _increments));
              }

              for (int i = (int)Math.Floor(((_numberOfPositions) / 3) * 2.0); i < (_numberOfPositions); i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y + (15 * _increments));
              }
            }
            else if (laneNumber == 23)
            {
              _carLimit = 6;
              this._numberOfPositions = 45;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 9) * 4; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y);
              }

              for (int i = (_numberOfPositions / 9) * 4; i < (_numberOfPositions); i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - ((i - 20) * _increments));
              }
            }
            break;
          }
        case "CrossP":
          {
            _increments = 1;
            if ((laneNumber == 6))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);
              }
            }
            else if (laneNumber == 0)
            {
              this._carLimit = 2;
              this._numberOfPositions = Math.Abs(pStart.Y - pEnd.Y);
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);
              }
            }
            else if ((laneNumber == 1))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
            }
            else if (laneNumber == 5)
            {
              this._carLimit = 2;
              this._numberOfPositions = Math.Abs(pStart.Y - pEnd.Y);
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
            }
            else if ((laneNumber == 2) || (laneNumber == 9))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 43;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y);
              }
            }
            else if (laneNumber == 3)
            {
              //Added by JP:
              this._carLimit = 3;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 4) * 1.8; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y + i * _increments);
              }
              for (int i = (int)((_numberOfPositions / 4) * 1.8); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y + (18 * _increments));
              }
            }
            else if ((laneNumber == 4) || (laneNumber == 7))
            {
              //Added by JP:
              this._carLimit = 4;
              this._numberOfPositions = 43;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y);
              }
            }
            else if (laneNumber == 8)
            {
              //Added by JP:
              this._carLimit = 3;
              this._numberOfPositions = 40;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 4) * 1.5; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y - i * _increments);
              }
              for (int i = (int)((_numberOfPositions / 4) * 1.5); i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y - (15 * _increments));
              }
            }
            else if (laneNumber == 10)
            {
              _carLimit = 2;
              this._numberOfPositions = 30;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 3) * 2; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);
              }
              for (int i = (_numberOfPositions / 3) * 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - ((i - 20) * _increments), pStart.Y + i * _increments);
              }

            }
            else if (laneNumber == 11)
            {
              _carLimit = 6;
              this._numberOfPositions = 80;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);
              }

            }
            else if (laneNumber == 12)
            {
              _carLimit = 6;
              this._numberOfPositions = 60;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions / 2; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y + i * _increments);
              }
              for (int i = _numberOfPositions / 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + ((i - 30) * _increments), pStart.Y + i * _increments);
              }

            }
            else if (laneNumber == 13)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y - i * _increments);
              }
            }
            else if (laneNumber == 14)
            {
              _carLimit = 6;
              this._numberOfPositions = 60;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y);
              }
            }
            else if (laneNumber == 15)
            {
              _carLimit = 6;
              this._numberOfPositions = 45;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions / 2 - 5; i++)
              {
                _positions[i] = new Point(pStart.X - i * _increments, pStart.Y);
              }
              for (int i = _numberOfPositions / 2 - 5; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - (i * _increments), pStart.Y + ((i - 18) * _increments));
              }
            }
            else if (laneNumber == 16)
            {
              _carLimit = 2;
              this._numberOfPositions = 30;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < (_numberOfPositions / 3) * 2; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
              for (int i = (_numberOfPositions / 3) * 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + ((i - 20) * _increments), pStart.Y - i * _increments);
              }
            }
            else if (laneNumber == 17)
            {
              _carLimit = 6;
              this._numberOfPositions = 80;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
            }
            else if (laneNumber == 18)
            {
              _carLimit = 6;
              this._numberOfPositions = 60;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions / 2; i++)
              {
                _positions[i] = new Point(pStart.X, pStart.Y - i * _increments);
              }
              for (int i = _numberOfPositions / 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X - ((i - 30) * _increments), pStart.Y - (i * _increments));
              }
            }
            else if (laneNumber == 19)
            {
              _carLimit = 2;
              this._numberOfPositions = 10;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y + i * _increments);
              }
            }
            else if (laneNumber == 20)
            {
              _carLimit = 6;
              this._numberOfPositions = 60;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y);
              }
            }
            else if (laneNumber == 21)
            {
              _carLimit = 6;
              this._numberOfPositions = 45;
              this._positions = new Point[_numberOfPositions];
              for (int i = 0; i < _numberOfPositions / 2; i++)
              {
                _positions[i] = new Point(pStart.X + i * _increments, pStart.Y);
              }

              for (int i = _numberOfPositions / 2; i < _numberOfPositions; i++)
              {
                _positions[i] = new Point(pStart.X + (i * _increments), pStart.Y - ((i - 22) * _increments));
              }
            }

            break;
          }

      }
      if (hasLight)
      {
        TrafficLight = new TrafficLightC(5, new Point(1, 1));
      }
    }


    public void reachEndOfLaneHandler(object obj, EventArgs args)
    {
      //car has reached end of lane.
      //if light is green changeto next lane.
    }

    public bool Spawnable
    {
      get; set;
    }

    public int SpawnTime
    {
      get; set;
    }


    public void AddCar(Car newCar)
    {
      newCar.Coordinates = this._positions[0];
      //add new car.
      this._cars.Add(newCar);

    }

    /// <summary>
    /// Once the car has reached the end of the lane, assuming the light is green. 
    /// It will move the car to the next connected lane.
    /// </summary>
    /// <param name="Car">Car to be moved to next lane</param>
    public void ChangeCarToNextLane(Car pCar)
    {
      //if there are no other lanes, then destroy
      if (this._connectedLanes.Count == 0)
      {
        _cars.Remove(pCar);
      }
      //if there is one connected lane, then add to next lane and remove from current
      else if (this._connectedLanes.Count == 1)
      {
        if (this._connectedLanes[0]._carLimit > this._connectedLanes[0]._cars.Count)
        {
          this._connectedLanes[0].AddCar(pCar);
          _cars.Remove(pCar);
        }

      }
      //if there a more lanes, then choose random lane
      else if (this._connectedLanes.Count == 2)
      {
        Random rand = new Random();
        int number = rand.Next(0, 2);
        if (this._connectedLanes[number]._carLimit > this._connectedLanes[number]._cars.Count)
        {
          this._connectedLanes[number].AddCar(pCar);
          _cars.Remove(pCar);
        }

      }
      else
      {
        Random rand = new Random();
        int number = rand.Next(0, 3);
        if (this._connectedLanes[number]._carLimit > this._connectedLanes[number]._cars.Count)
        {
          this._connectedLanes[number].AddCar(pCar);
          _cars.Remove(pCar);
        }
      }

    }


    public void ConnectToLane(Lane lane)
    {
      this._connectedLanes.Add(lane);
      lane.Spawnable = false;
    }

    public void DisconnectLanes()
    {
      for (int i = 0; i < _connectedLanes.Count; i++)
      {
        _connectedLanes[i].Spawnable = true;
      }

      this._connectedLanes.Clear();
    }

    // JP Version
    private void MoveCarsOnTimerElapsed(object sender, ElapsedEventArgs e)
    {
      // for each car, check if can move, if so, move car
      // if first car and there is room, then move
      for (int i = 0; i < this._cars.Count; i++)
      {
        int positionIndex = -1;
        positionIndex = this.getPositionIndexFromCar(_cars[i]);

        //if first car is at last postion
        if (_cars[i].Coordinates == this._positions[_numberOfPositions - 1])
        {
          if (TrafficLight == null)
          {
            //there's no light so, just move to next
            ChangeCarToNextLane(_cars[i]);
          }
          //if trafficLight is present and is green then change To next lane
          else if (TrafficLight.Colour == 3)
          {
            ChangeCarToNextLane(_cars[i]);
          }
        }
        else if (i == 0 || positionIndex == 0)
        {
          //there is room, so move
          MoveToNextPosition(i);
        }
        try
        {
          if ((positionIndex + 7 + _cars[i].Speed) >= _positions.Length)
          {
            continue;
          }
        }
        catch
        {

          continue;
        }
        try
        {
          if (i != 0 && _cars[i - 1] != null &&
                            (
                             _cars[i - 1].Coordinates == _positions[positionIndex + 1] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 2] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 3] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 4] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 5] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 6] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 8] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 9] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 10] ||
                             _cars[i - 1].Coordinates == _positions[positionIndex + 11] ||
                            _cars[i - 1].Coordinates == _positions[positionIndex + 1 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 2 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 3 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 4 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 5 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 6 + _cars[i].Speed]
                            || _cars[i - 1].Coordinates == _positions[positionIndex + 7 + _cars[i].Speed]
                            ))
          {
            // method check for collision or something
            continue;
          }
          else
          {
            //there is room, so move
            MoveToNextPosition(i);
            continue;
          }
        }
        catch
        {
          MoveToNextPosition(i);
        }
      }
    }

    private void MoveToNextPosition(int i)
    {
      int positionIndex = 0;
      int cycles = 0;
      for (int j = 0; j < this._numberOfPositions - 1; j++)
      {


        if (_cars[i].Coordinates == _positions[j])
        {
          positionIndex = this.getPositionIndexFromCar(_cars[i]);
          if (i != 0 && _cars[i - 1] != null &&
              (_cars[i - 1].Coordinates == _positions[positionIndex + 1] ||
               _cars[i - 1].Coordinates == _positions[positionIndex + 2] ||
               _cars[i - 1].Coordinates == _positions[positionIndex + 3] ||
               _cars[i - 1].Coordinates == _positions[positionIndex + 4] ||
               _cars[i - 1].Coordinates == _positions[positionIndex + 5] ||
               _cars[i - 1].Coordinates == _positions[positionIndex + 6]))
          {

            while (_cars[i].Speed == 1)
            {
              _cars[i].Decellerate();
            }
          }


          else
          {
            if (_cars[i].Speed <= _cars[i].CruiseSpeed)
            {
              _cars[i].Accelerate();
            }
          }

          if (((j + _cars[i].Speed) < _positions.Length) && (cycles == 2))
          {
            _cars[i].Coordinates = _positions[j + _cars[i].Speed];
            cycles = 0;
          }
          else
          {
            _cars[i].Coordinates = _positions[j + 1];
          }

          return;
        }
        cycles++;
      }
    }

    public Timer MoveTimer
    {
      get { return this._moveTimer; }

    }

    public TrafficLightC TrafficLight { get => _trafficLight; set => _trafficLight = value; }

    /// <summary>
    /// Return number of cars in the cars list.
    /// </summary>
    /// <returns></returns>
    public int GetNrOfCars()
    {
      return this._cars.Count;
    }

    /// <summary>
    /// Return number of cars in the cars in queque list.
    /// </summary>
    /// <returns></returns>
    public int GetNrOfCarsInQueue()
    {
      return this._carsInQueue.Count;
    }


    private void SpawnCarsOnTimerElapsed(object sender, ElapsedEventArgs e)
    {
      // if lane is not spawnable, or  is full, do nothing.
      if (!Spawnable || _carLimit == _cars.Count) return;

      foreach (Car t in _cars)
      {
        if (t.Coordinates == _positions[0]) return;
      }

      _cars.Add(new Car(_positions[0]));
    }

    /// <summary>
    /// Draw all cars to the passed graphics
    /// </summary>
    /// <param name="gr"></param>
    public void DrawCars(ref Graphics gr)
    {
      foreach (Car t in _cars)
      {
        t.Draw(ref gr);
      }
    }

    /// <summary>
    /// returns index of the position in the position array of the car.
    /// </summary>
    /// <param name="aCar">The car of which you want to find out the position index</param>
    /// <returns></returns>

    public int getPositionIndexFromCar(Car aCar)
    {


      for (int j = 0; j < this._numberOfPositions; j++)
      {
        if (aCar.Coordinates == this._positions[j])
          return j;
      }


      //to satisfy compiler, should never be reached.
      return -1;

    }

     public void IncreaseSpawnTime()
    {
      this.SpawnTime += 500;
    }

    public void DecreaseSpawnTime()
    {
      this.SpawnTime -= 500;
    }
  }
}
