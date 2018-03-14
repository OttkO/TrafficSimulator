using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Timers;

namespace TrafficSimulator
{
  public class PCrossroad : Road
  {
    private readonly TrafficLightP[] _trafPed = new TrafficLightP[4];
    private readonly ElapsedEventHandler _onTimerTick;

    // for pedestrian stuff
    private ElapsedEventHandler _onSensorPress;
    private ElapsedEventHandler _onPedestrianStart;

    private readonly ElapsedEventHandler[] _onPedestrianMove = new ElapsedEventHandler[4];
    private readonly List<TrafficLightC> _tempList = new List<TrafficLightC>();
    private readonly Timer _pedTimer;
    private bool _pressedSensor;
    private readonly Pedestrian[] _ped;
    private int[] _laneId;


    /// <summary>
    /// Constructor of PCrossroad
    /// </summary>
    /// <param name="Coord">Coordinates of the road</param>
    public PCrossroad(Point Coord)
    {
      //assign coordinates and images
      //creates list of lane and neighbours
      //Make all spawnable lane spawnable
      image = new Bitmap(Properties.Resources.CrossP);
      lanes = new List<Lane>();
      Coordinates = Coord;

      //0 to 4
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 58, Coordinates.Y), new Point(Coordinates.X + 58, Coordinates.Y + 20), "CrossP", 0));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 82, Coordinates.Y + 38), new Point(Coordinates.X + 82, Coordinates.Y), "CrossP", 1));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 140, Coordinates.Y + 52), new Point(Coordinates.X + 98, Coordinates.Y + 52), "CrossP", 2));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 140, Coordinates.Y + 52), new Point(Coordinates.X + 98, Coordinates.Y + 69), "CrossP", 3));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 98, Coordinates.Y + 85), new Point(Coordinates.X + 141, Coordinates.Y + 85), "CrossP", 4));

      //5 to 9
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 80, Coordinates.Y + 140), new Point(Coordinates.X + 80, Coordinates.Y + 120), "CrossP", 5));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 58, Coordinates.Y + 100), new Point(Coordinates.X + 58, Coordinates.Y + 140), "CrossP", 6));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X, Coordinates.Y + 85), new Point(Coordinates.X + 42, Coordinates.Y + 85), "CrossP", 7));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X, Coordinates.Y + 85), new Point(Coordinates.X + 42, Coordinates.Y + 70), "CrossP", 8));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 42, Coordinates.Y + 52), new Point(Coordinates.X, Coordinates.Y + 52), "CrossP", 9));

      //10 to 15
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 58, Coordinates.Y + 20), new Point(Coordinates.X + 42, Coordinates.Y + 52), "CrossP", 10));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 58, Coordinates.Y + 20), new Point(Coordinates.X + 58, Coordinates.Y + 100), "CrossP", 11));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 58, Coordinates.Y + 20), new Point(Coordinates.X + 98, Coordinates.Y + 85), "CrossP", 12));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 98, Coordinates.Y + 52), new Point(Coordinates.X + 82, Coordinates.Y + 48), "CrossP", 13));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 98, Coordinates.Y + 52), new Point(Coordinates.X + 42, Coordinates.Y + 52), "CrossP", 14));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 98, Coordinates.Y + 71), new Point(Coordinates.X + 58, Coordinates.Y + 100), "CrossP", 15));

      //16 ro 21
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 80, Coordinates.Y + 120), new Point(Coordinates.X + 98, Coordinates.Y + 85), "CrossP", 16));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 80, Coordinates.Y + 120), new Point(Coordinates.X + 82, Coordinates.Y + 38), "CrossP", 17));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 80, Coordinates.Y + 120), new Point(Coordinates.X + 42, Coordinates.Y + 52), "CrossP", 18));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 42, Coordinates.Y + 85), new Point(Coordinates.X + 58, Coordinates.Y + 100), "CrossP", 19));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 42, Coordinates.Y + 85), new Point(Coordinates.X + 98, Coordinates.Y + 85), "CrossP", 20));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 42, Coordinates.Y + 70), new Point(Coordinates.X + 58, Coordinates.Y + 48), "CrossP", 21));

      ChangeSpawnability("N", true);
      ChangeSpawnability("E", true);
      ChangeSpawnability("S", true);
      ChangeSpawnability("W", true);


      Random rand = new Random();
      int r = rand.Next(0, 4);
      if (r == 0)
      {
        lanes[0].TrafficLight.Colour = 3;
        lanes[2].TrafficLight.Timer = 20;
        lanes[3].TrafficLight.Timer = 20;
        lanes[5].TrafficLight.Timer = 10;
      }
      else if (r == 1)
      {
        lanes[2].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Colour = 3;
        lanes[5].TrafficLight.Timer = 20;
        lanes[7].TrafficLight.Timer = 10;
        lanes[8].TrafficLight.Timer = 10;
      }
      else if (r == 2)
      {
        lanes[5].TrafficLight.Colour = 3;
        lanes[6].TrafficLight.Timer = 20;
        lanes[7].TrafficLight.Timer = 20;
        lanes[0].TrafficLight.Timer = 10;
      }
      else
      {
        lanes[7].TrafficLight.Colour = 3;
        lanes[8].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Timer = 20;
        lanes[2].TrafficLight.Timer = 10;
        lanes[3].TrafficLight.Timer = 10;

      }

      lanes[0].TrafficLight.GreenInterval = 10;
      lanes[2].TrafficLight.GreenInterval = 10;
      lanes[3].TrafficLight.GreenInterval = 10;
      lanes[5].TrafficLight.GreenInterval = 10;
      lanes[7].TrafficLight.GreenInterval = 10;
      lanes[8].TrafficLight.GreenInterval = 10;

      lanes[0].TrafficLight.RedInterval = 30;
      lanes[2].TrafficLight.RedInterval = 30;
      lanes[3].TrafficLight.RedInterval = 30;
      lanes[5].TrafficLight.RedInterval = 30;
      lanes[7].TrafficLight.RedInterval = 30;
      lanes[8].TrafficLight.RedInterval = 30;

      lanes[0].ConnectToLane(this.lanes[10]);
      lanes[10].ConnectToLane(this.lanes[9]);
      lanes[0].ConnectToLane(this.lanes[11]);
      lanes[11].ConnectToLane(this.lanes[6]);
      lanes[0].ConnectToLane(this.lanes[12]);
      lanes[12].ConnectToLane(this.lanes[4]);
      lanes[2].ConnectToLane(this.lanes[13]);
      lanes[13].ConnectToLane(this.lanes[1]);
      lanes[2].ConnectToLane(this.lanes[14]);
      lanes[14].ConnectToLane(this.lanes[9]);
      lanes[3].ConnectToLane(this.lanes[15]);
      lanes[15].ConnectToLane(this.lanes[6]);
      lanes[5].ConnectToLane(this.lanes[16]);
      lanes[16].ConnectToLane(this.lanes[4]);
      lanes[5].ConnectToLane(this.lanes[17]);
      lanes[17].ConnectToLane(this.lanes[1]);
      lanes[5].ConnectToLane(this.lanes[18]);
      lanes[18].ConnectToLane(this.lanes[9]);
      lanes[7].ConnectToLane(this.lanes[19]);
      lanes[19].ConnectToLane(this.lanes[6]);
      lanes[7].ConnectToLane(this.lanes[20]);
      lanes[20].ConnectToLane(this.lanes[4]);
      lanes[8].ConnectToLane(this.lanes[21]);
      lanes[21].ConnectToLane(this.lanes[1]);

      NrOfNeighbours = 0;

      connectedRoads = new Road[4];

      lanes[0].TrafficLight.Position = new Point(Coordinates.X + 27, Coordinates.Y + 4);
      lanes[0].TrafficLight.CPost = Coordinates;

      lanes[2].TrafficLight.Position = new Point(Coordinates.X + 110, Coordinates.Y + 28);
      lanes[2].TrafficLight.CPost = Coordinates;
      lanes[3].TrafficLight.Position = new Point(Coordinates.X + 110, Coordinates.Y + 38);
      lanes[3].TrafficLight.CPost = Coordinates;

      lanes[5].TrafficLight.Position = new Point(Coordinates.X + 110, Coordinates.Y + 120);
      lanes[5].TrafficLight.CPost = Coordinates;

      lanes[7].TrafficLight.Position = new Point(Coordinates.X + 20, Coordinates.Y + 113);
      lanes[7].TrafficLight.CPost = Coordinates;
      lanes[8].TrafficLight.Position = new Point(Coordinates.X + 20, Coordinates.Y + 103);
      lanes[8].TrafficLight.CPost = Coordinates;

      for (int i = 0; i < 4; i++)
      {
        _trafPed[i] = new TrafficLightP(10, new Point(1, 1));
      }

      _trafPed[0].Position = new Point(Coordinates.X + 20, Coordinates.Y + 33);
      _trafPed[0].CPost = Coordinates;
      _trafPed[1].Position = new Point(Coordinates.X + 110, Coordinates.Y + 14);
      _trafPed[1].CPost = Coordinates;
      _trafPed[2].Position = new Point(Coordinates.X + 110, Coordinates.Y + 105);
      _trafPed[2].CPost = Coordinates;
      _trafPed[3].Position = new Point(Coordinates.X + 20, Coordinates.Y + 123);
      _trafPed[3].CPost = Coordinates;

      _pedTimer = new Timer { Enabled = false };

      trafTime.Interval = 1000;
      _pedTimer.Interval = 1000;

      _onTimerTick = new ElapsedEventHandler(IncTimer);
      trafTime.Elapsed += _onTimerTick;

      _ped = new Pedestrian[4];
      for (int i = 0; i < 4; i++)
      {
        _ped[i] = new Pedestrian { Cpost = Coordinates };
      }

      _ped[0].Pos = new Point(Coord.X + 42, Coord.Y + 35);
      _ped[1].Pos = new Point(Coord.X + 102, Coord.Y + 35);
      _ped[2].Pos = new Point(Coord.X + 102, Coord.Y + 110);
      _ped[3].Pos = new Point(Coord.X + 42, Coord.Y + 110);

      DisableTimer();
    }

    private void IncTimer(object sender, ElapsedEventArgs e)
    {
      lanes[0].TrafficLight.IncTimer();
      lanes[2].TrafficLight.IncTimer();
      lanes[3].TrafficLight.IncTimer();
      lanes[5].TrafficLight.IncTimer();
      lanes[7].TrafficLight.IncTimer();
      lanes[8].TrafficLight.IncTimer();
    }


    public void PressSensor()
    {
      _pressedSensor = true;

      // Check the green light, timer 0 or not, pressed or not  
      _onSensorPress = new ElapsedEventHandler(StartPedestrian);
      trafTime.Elapsed += _onSensorPress;
    }

    private void StartPedestrian(object sender, ElapsedEventArgs e)
    {
      List<Lane> TEMP = new List<Lane> { lanes[0], lanes[2], lanes[3], lanes[5], lanes[7], lanes[8] };
      if (_pressedSensor)
      {
        for (int i = 0; i < 6; i++)
        {
          if ((TEMP[i].TrafficLight.Colour == 2 && TEMP[i].TrafficLight.Timer == 9))
          {
            trafTime.Elapsed -= _onTimerTick;
            trafTime.Enabled = false;
            trafTime.Stop();

            if ((i == 0) || (i == 3))
            {
              _tempList.Add(new TrafficLightC(10, new Point(1, 1)));
              _tempList.Add(new TrafficLightC(10, new Point(1, 1)));
              _laneId = new int[2];
              _laneId[0] = i + 1;
              _laneId[1] = i + 2;
              for (int k = 0; k < 2; k++)
              {
                TrafficLightC tp = new TrafficLightC(10, new Point(1, 1));
                tp.Colour = 3;
                tp.Position = TEMP[i + k + 1].TrafficLight.Position;
                tp.CPost = TEMP[i + k + 1].TrafficLight.CPost;
                tp.GreenInterval = TEMP[i + k + 1].TrafficLight.GreenInterval;
                tp.RedInterval = TEMP[i + k + 1].TrafficLight.RedInterval;
                tp.Timer = 0;
                tp.ColorChanged = true;
                _tempList[k] = tp;
                TEMP[i + k + 1].TrafficLight.Colour = 1;
                TEMP[i + k + 1].TrafficLight.Timer = 0;
                TEMP[i + k + 1].TrafficLight.ColorChanged = true;
              }
              TEMP[i].TrafficLight.Colour = 1;
              TEMP[i].TrafficLight.Timer = 0;
              TEMP[i].TrafficLight.ColorChanged = true;
              TEMP[(i + 3) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 4) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 5) % 6].TrafficLight.Timer += 1;
            }
            else if ((i == 1) || (i == 4))
            {
              _laneId = new int[1];
              _laneId[0] = (i + 2) % 6;

              TrafficLightC tp = new TrafficLightC(10, new Point(1, 1))
              {
                Colour = 3,
                Position = TEMP[_laneId[0]].TrafficLight.Position,
                CPost = TEMP[_laneId[0]].TrafficLight.CPost,
                GreenInterval = TEMP[_laneId[0]].TrafficLight.GreenInterval,
                RedInterval = TEMP[_laneId[0]].TrafficLight.RedInterval,
                Timer = 0,
                ColorChanged = true
              };

              _tempList.Add(tp);

              TEMP[_laneId[0]].TrafficLight.Colour = 1;
              TEMP[_laneId[0]].TrafficLight.Timer = 0;
              TEMP[_laneId[0]].TrafficLight.ColorChanged = true;
              TEMP[i].TrafficLight.ColorChanged = true;
              TEMP[i].TrafficLight.Colour = 1;
              TEMP[i + 1].TrafficLight.Colour = 1;
              TEMP[i + 1].TrafficLight.ColorChanged = true;
              TEMP[i].TrafficLight.Timer = 0;
              TEMP[i + 1].TrafficLight.Timer = 0;
              TEMP[(i + 3) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 4) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 5) % 6].TrafficLight.Timer += 1;
            }
            else if ((i == 2) || (i == 5))
            {
              _laneId = new int[1];
              _laneId[0] = (i + 1) % 6;

              TrafficLightC tp = new TrafficLightC(10, new Point(1, 1))
              {
                Colour = 3,
                Position = TEMP[_laneId[0]].TrafficLight.Position,
                CPost = TEMP[_laneId[0]].TrafficLight.CPost,
                GreenInterval = TEMP[_laneId[0]].TrafficLight.GreenInterval,
                RedInterval = TEMP[_laneId[0]].TrafficLight.RedInterval,
                Timer = 0,
                ColorChanged = true
              };

              _tempList.Add(tp);

              TEMP[_laneId[0]].TrafficLight.Colour = 1;
              TEMP[_laneId[0]].TrafficLight.Timer = 0;
              TEMP[_laneId[0]].TrafficLight.ColorChanged = true;

              TEMP[i].TrafficLight.ColorChanged = true;
              TEMP[i - 1].TrafficLight.ColorChanged = true;
              TEMP[i].TrafficLight.Colour = 1;
              TEMP[i - 1].TrafficLight.Colour = 1;
              TEMP[i].TrafficLight.Timer = 0;
              TEMP[i - 1].TrafficLight.Timer = 0;
              TEMP[(i + 3) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 4) % 6].TrafficLight.Timer += 1;
              TEMP[(i + 5) % 6].TrafficLight.Timer += 1;
            }

            for (int j = 0; j < 4; j++)
            {
              _onPedestrianMove[j] = new ElapsedEventHandler(_ped[j].Move);
              _pedTimer.Elapsed += _onPedestrianMove[j];
            }

            for (int c = 0; c < _trafPed.Count(); c++)
            {
              _trafPed[c].Colour = 3;
              _trafPed[c].ColorChanged = true;
            }

            _onPedestrianStart = new ElapsedEventHandler(StopPedestrian);
            _pedTimer.Enabled = true;
            _pedTimer.Elapsed += _onPedestrianStart;
            
            _pedTimer.Start();
            _pressedSensor = false;
            break;
          }
        }
      }
    }

    private void StopPedestrian(object sender, ElapsedEventArgs e)
    {
      if ((_ped[0].Fin || (_ped[1].Fin) || (_ped[2].Fin) || (_ped[3].Fin)))
      {
        _pedTimer.Enabled = false;
        _pedTimer.Stop();

        trafTime.Elapsed += _onTimerTick;
        trafTime.Elapsed -= _onSensorPress;

        _pressedSensor = false;
        _pedTimer.Elapsed -= _onPedestrianStart;

        _ped[0].Fin = false;
        _ped[1].Fin = false;
        _ped[2].Fin = false;
        _ped[3].Fin = false;


        //Tracy f1 = (Tracy)Tracy.ActiveForm;
        //f1.pedestrianDone();

        if (_laneId[0] == 0)
        {
          lanes[0].TrafficLight = _tempList[0];
        }
        else if (_laneId[0] == 1)
        {
          lanes[2].TrafficLight = _tempList[0];
          lanes[3].TrafficLight = _tempList[1];
        }
        else if (_laneId[0] == 3)
        {
          lanes[5].TrafficLight = _tempList[0];
        }
        else if (_laneId[0] == 4)
        {
          lanes[7].TrafficLight = _tempList[0];
          lanes[8].TrafficLight = _tempList[1];
        }

        _tempList.Clear();

        for (int j = 0; j < 4; j++)
        {
          _pedTimer.Elapsed -= _onPedestrianMove[j];
        }

        for (int i = 0; i < _trafPed.Count(); i++)
        {
          _trafPed[i].Colour = 1;
          _trafPed[i].ColorChanged = true;
        }
      }

      trafTime.Start();
    }
    
    public override bool ConnectToRoad(string side, Road other)
    {
      // First check the side to which the road "other" is connected
      // The code for side is the 4 cardinal NESW(for now)
      // Then, check whether it is possile to connect other to the calling road

      /*
       * For curve: 
       * SW = South West
       * SE = South East
       * NW = North West
       * NE = North East
       
       * For straight:
       * V= Vertical
       * H=Horizontal
       */

      switch (side)
      {
        case "N":
          {
            if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "SE")))
                  || ((other is Straight) && other.GetDirection() == "V")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
            {
              connectedRoads[0] = other;
              return true;
            }

            return false;
          }

        case "E":
          {
            if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "NW")))
                  || ((other is Straight) && other.GetDirection() == "H")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
            {
              connectedRoads[1] = other;
              return true;
            }

            return false;
          }

        case "S":
          {
            if (((other is Curve) && ((other.GetDirection() == "NE") || (other.GetDirection() == "NW")))
                  || ((other is Straight) && other.GetDirection() == "V")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
            {
              connectedRoads[2] = other;
              return true;
            }

            return false;
          }

        case "W":
          {
            if (((other is Curve) && ((other.GetDirection() == "SE") || (other.GetDirection() == "NE")))
                  || ((other is Straight) && other.GetDirection() == "H")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
            {
              connectedRoads[3] = other;
              return true;
            }
            return false;
          }

        default:
          return false;
      }
    }

    public override void ConnectLanesTo(string side, Road other)
    {
      //Check which side is the Road "other" is connected to the calling road
      //Then check the type of the road
      //Note for the lane: The calling lane connectToLane("gets the car from/the cars flow to", corresponding lane in "other")
      switch (side)
      {
        case "N":
          {
            if (other is CCrossroad)
            {
              this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
              this.lanes[1].ConnectToLane(other.getListOfLane()[6]);
              other.getListOfLane()[8].ConnectToLane(this.lanes[0]);

            }
            else if (other is PCrossroad)
            {
              this.lanes[1].ConnectToLane(other.getListOfLane()[5]);
              other.getListOfLane()[6].ConnectToLane(this.lanes[0]);
            }

            else if (other is Curve)
            {
              if (other.GetDirection() == "SW")
              {
                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
              }
              else // SE
              {
                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
              }
            }
            else
            {
              this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
            }
          }
          break;
        case "E":
          {
            if (other is CCrossroad)
            {
              this.lanes[4].ConnectToLane(other.getListOfLane()[9]);
              this.lanes[4].ConnectToLane(other.getListOfLane()[10]);
              other.getListOfLane()[11].ConnectToLane(this.lanes[2]);
              other.getListOfLane()[11].ConnectToLane(this.lanes[3]);
            }
            else if (other is PCrossroad)
            {
              this.lanes[4].ConnectToLane(other.getListOfLane()[7]);
              this.lanes[4].ConnectToLane(other.getListOfLane()[8]);
              other.getListOfLane()[9].ConnectToLane(this.lanes[2]);
              other.getListOfLane()[9].ConnectToLane(this.lanes[3]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "SW")
              {
                this.lanes[4].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[2]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[3]);
              }
              else // NW
              {
                this.lanes[4].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[2]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
              }
            }
            else
            {
              this.lanes[4].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[2]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
            }
          }
          break;
        case "S":
          {
            if (other is CCrossroad)
            {
              this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
              this.lanes[6].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[2].ConnectToLane(this.lanes[5]);
            }
            else if (other is PCrossroad)
            {
              this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "NW")
              {
                this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
              }
              else // NE
              {
                this.lanes[6].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[5]);
              }
            }
            else
            {
              this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
            }
          }
          break;
        case "W":
          {
            if (other is CCrossroad)
            {
              this.lanes[9].ConnectToLane(other.getListOfLane()[3]);
              this.lanes[9].ConnectToLane(other.getListOfLane()[4]);
              other.getListOfLane()[5].ConnectToLane(this.lanes[7]);
              other.getListOfLane()[5].ConnectToLane(this.lanes[8]);
            }
            else if (other is PCrossroad)
            {
              this.lanes[9].ConnectToLane(other.getListOfLane()[2]);
              this.lanes[9].ConnectToLane(other.getListOfLane()[3]);
              other.getListOfLane()[4].ConnectToLane(this.lanes[7]);
              other.getListOfLane()[4].ConnectToLane(this.lanes[8]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "SE")
              {
                this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
              }
              else // NE
              {
                this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
              }
            }
            else
            {
              this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
            }
          }
          break;
        default:
          return;
      }
    }

    public override void AdjustSpawnTime(int time)
    {
      List<Lane> temp = new List<Lane>();
      temp.Add(this.lanes[0]);
      temp.Add(this.lanes[2]);
      temp.Add(this.lanes[3]);
      temp.Add(this.lanes[5]);
      temp.Add(this.lanes[7]);
      temp.Add(this.lanes[8]);

      for (int i = 0; i < temp.Count; i++)
      {
        temp[i].SpawnTime = time;
      }
    }


    public override void DisconnectRoad()
    {
      time.Elapsed -= _onTimerTick;
      for (int i = 0; i < connectedRoads.Count(); i++)
      {
        if (connectedRoads[i] != null)
        {
          // north
          if (i == 0)
          {
            if (connectedRoads[i] is Curve)
            {
              if (connectedRoads[i].GetDirection() == "SW")
              {
                connectedRoads[i].getListOfLane()[0].DisconnectLanes();
              }
              else
              {
                connectedRoads[i].getListOfLane()[0].DisconnectLanes();
              }
            }
            else if (connectedRoads[i] is Straight)
            {
              connectedRoads[i].getListOfLane()[0].DisconnectLanes();

            }
            else if (connectedRoads[i] is CCrossroad)
            {
              connectedRoads[i].getListOfLane()[8].DisconnectLanes();
            }
            else
            {
              connectedRoads[i].getListOfLane()[6].DisconnectLanes();
            }
            connectedRoads[i].DisconnectNeighbour("S");
            connectedRoads[i].ChangeSpawnability("S", true);

          }
          // east
          else if (i == 1)
          {
            if (connectedRoads[i] is Curve)
            {
              if (connectedRoads[i].GetDirection() == "SW")
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
              else
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
            }
            else if (connectedRoads[i] is Straight)
            {
              connectedRoads[i].getListOfLane()[0].DisconnectLanes();

            }
            else if (connectedRoads[i] is CCrossroad)
            {
              connectedRoads[i].getListOfLane()[11].DisconnectLanes();
            }
            else
            {
              connectedRoads[i].getListOfLane()[9].DisconnectLanes();
            }
            connectedRoads[i].DisconnectNeighbour("W");
            connectedRoads[i].ChangeSpawnability("W", true);
          }
          else if (i == 2)
          {
            if (connectedRoads[i] is Curve)
            {
              if (connectedRoads[i].GetDirection() == "SW")
              {
                connectedRoads[i].getListOfLane()[0].DisconnectLanes();
              }
              else
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
            }
            else if (connectedRoads[i] is Straight)
            {
              connectedRoads[i].getListOfLane()[1].DisconnectLanes();

            }
            else if (connectedRoads[i] is CCrossroad)
            {
              connectedRoads[i].getListOfLane()[2].DisconnectLanes();
            }
            else
            {
              connectedRoads[i].getListOfLane()[1].DisconnectLanes();
            }
            connectedRoads[i].DisconnectNeighbour("N");
            connectedRoads[i].ChangeSpawnability("N", true);
          }
          else
          {
            if (connectedRoads[i] is Curve)
            {
              if (connectedRoads[i].GetDirection() == "SE")
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
              else
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
            }
            else if (connectedRoads[i] is Straight)
            {
              connectedRoads[i].getListOfLane()[1].DisconnectLanes();

            }
            else if (connectedRoads[i] is CCrossroad)
            {
              connectedRoads[i].getListOfLane()[5].DisconnectLanes();
            }
            else
            {
              connectedRoads[i].getListOfLane()[4].DisconnectLanes();
            }
            connectedRoads[i].DisconnectNeighbour("E");
            connectedRoads[i].ChangeSpawnability("E", true);
          }

        }
      }
    }


    public override void DisconnectNeighbour(string side)
    {
      switch (side)
      {
        case "N":
          connectedRoads[0] = null;
          break;
        case "E":
          connectedRoads[1] = null;
          break;
        case "S":
          connectedRoads[2] = null;
          break;
        case "W":
          connectedRoads[3] = null;
          break;
      }
    }

    public override string GetDirection()
    {
      return "";
    }

    /// <summary>
    /// Change the spawnability of all lanes in this road located in the side 'side'
    /// </summary>
    /// <param name="side">The side where the spawnability of the lanes are going to be changed</param>
    /// <param name="spawnAble">The new spawnable status of the lanes on that side</param>
    public override void ChangeSpawnability(string side, bool spawnAble)
    {
      switch (side)
      {
        case "N":
          {
            this.lanes[0].Spawnable = spawnAble;
          }
          break;
        case "E":
          {
            this.lanes[3].Spawnable = spawnAble;
            this.lanes[2].Spawnable = spawnAble;
          }
          break;
        case "S":
          {
            this.lanes[5].Spawnable = spawnAble;
          }
          break;
        case "W":
          {
            this.lanes[7].Spawnable = spawnAble;
            this.lanes[8].Spawnable = spawnAble;
          }
          break;
      }
    }


    public override void Draw(ref Graphics g)
    {
      g.DrawImage(image, Coordinates.X, Coordinates.Y, 150, 150);
      int[] id = { 0, 2, 3, 5, 7, 8 };
      for (int i = 0; i < id.Length; i++)
      {
        this.lanes[id[i]].TrafficLight.Draw(ref g);
      }
      for (int i = 0; i < _trafPed.Count(); i++)
      {
        if (_trafPed[i] != null)
          _trafPed[i].Draw(ref g);
      }
    }

    public override void DrawTrafficLight(ref Graphics g)
    {
      int[] id = { 0, 2, 3, 5, 7, 8 };
      for (int i = 0; i < id.Length; i++)
      {
        if (this.lanes[id[i]].TrafficLight.ColorChanged)
        {
          this.lanes[id[i]].TrafficLight.Draw(ref g);
          this.lanes[id[i]].TrafficLight.ColorChanged = false;
        }
      }
      for (int i = 0; i < _trafPed.Count(); i++)
      {
        if (_trafPed[i] != null && _trafPed[i].ColorChanged)
        {

          _trafPed[i].Draw(ref g);
          _trafPed[i].ColorChanged = false;
        }

      }
    }

    public override void DrawCars(ref Graphics g)
    {
      base.DrawCars(ref g);
      for (int i = 0; i < _ped.Count(); i++)
      {
        _ped[i].Draw(ref g);
      }
    }

    public override void RemoveLanes()
    {
      for (int i = 0; i < lanes.Count; i++)
      {
        this.lanes.RemoveAt(i);
      }

    }

    public void adjustGreenTime(int Green)
    {
      this.lanes[0].TrafficLight.AdjustGreenTime(Green);
      this.lanes[2].TrafficLight.AdjustGreenTime(Green);
      this.lanes[3].TrafficLight.AdjustGreenTime(Green);
      this.lanes[5].TrafficLight.AdjustGreenTime(Green);
      this.lanes[7].TrafficLight.AdjustGreenTime(Green);
      this.lanes[8].TrafficLight.AdjustGreenTime(Green);
    }

    public override void StartTimer()
    {
      base.StartTimer();
      try
      {
        trafTime.Elapsed -= _onTimerTick;
        trafTime.Elapsed += _onTimerTick;
      }
      catch
      {
        trafTime.Elapsed += _onTimerTick;
      }

    }
    public override void DisableTimer()
    {
      base.DisableTimer();
      _pedTimer.Enabled = false;
      _pedTimer.Stop();
      _pedTimer.Elapsed -= _onPedestrianStart;

      for (int j = 0; j < 4; j++)
      {
        _pedTimer.Elapsed -= _onPedestrianMove[j];
        _ped[j].Pos = _ped[j].OriPosition;
      }

    }

    public void ResetTrafficLight()
    {
      Random rand = new Random();
      int r = rand.Next(0, 4);
      if (r == 0)
      {
        lanes[0].TrafficLight.Colour = 3;
        lanes[2].TrafficLight.Timer = lanes[2].TrafficLight.GreenInterval * 2;
        lanes[3].TrafficLight.Timer = lanes[3].TrafficLight.GreenInterval * 2;
        lanes[5].TrafficLight.Timer = lanes[5].TrafficLight.GreenInterval;

        lanes[2].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Colour = 1;
        lanes[5].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[8].TrafficLight.Colour = 1;
        lanes[0].TrafficLight.Timer = 0;
        lanes[7].TrafficLight.Timer = 0;
        lanes[8].TrafficLight.Timer = 0;
      }
      else if (r == 1)
      {
        lanes[2].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Colour = 3;
        lanes[5].TrafficLight.Timer = lanes[5].TrafficLight.GreenInterval * 2;
        lanes[7].TrafficLight.Timer = lanes[7].TrafficLight.GreenInterval;
        lanes[8].TrafficLight.Timer = lanes[8].TrafficLight.GreenInterval;

        lanes[0].TrafficLight.Colour = 1;
        lanes[5].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[8].TrafficLight.Colour = 1;
        lanes[0].TrafficLight.Timer = 0;
        lanes[2].TrafficLight.Timer = 0;
        lanes[3].TrafficLight.Timer = 0;
      }
      else if (r == 2)
      {
        lanes[5].TrafficLight.Colour = 3;
        lanes[8].TrafficLight.Timer = lanes[8].TrafficLight.GreenInterval * 2;
        lanes[7].TrafficLight.Timer = lanes[7].TrafficLight.GreenInterval * 2;
        lanes[0].TrafficLight.Timer = lanes[0].TrafficLight.GreenInterval;

        lanes[0].TrafficLight.Colour = 1;
        lanes[2].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[8].TrafficLight.Colour = 1;
        lanes[5].TrafficLight.Timer = 0;
        lanes[2].TrafficLight.Timer = 0;
        lanes[3].TrafficLight.Timer = 0;
      }
      else
      {
        lanes[7].TrafficLight.Colour = 3;
        lanes[8].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Timer = lanes[0].TrafficLight.GreenInterval * 2;
        lanes[2].TrafficLight.Timer = lanes[2].TrafficLight.GreenInterval;
        lanes[3].TrafficLight.Timer = lanes[3].TrafficLight.GreenInterval;

        lanes[2].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Colour = 1;
        lanes[0].TrafficLight.Colour = 1;
        lanes[5].TrafficLight.Colour = 1;
        lanes[5].TrafficLight.Timer = 0;
        lanes[7].TrafficLight.Timer = 0;
        lanes[8].TrafficLight.Timer = 0;
      }

      foreach (TrafficLightP t in _trafPed)
      {
        t.Colour = 1;
      }
    }
  }
}