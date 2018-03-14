using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TrafficSimulator
{
  public sealed class CCrossroad : Road
  {
    /// <summary>
    /// Constructor of CCrossroad
    /// </summary>
    /// <param name="coord">Coordinates of the road</param>
    public CCrossroad(Point coord)
    {
      //assign direction, coordinates and images
      //creates list of lane and neighbours
      //Make all spawnable lane spawnable
      image = new Bitmap(Properties.Resources.CrossC);
      lanes = new List<Lane>();

      DisableTimer();

      Coordinates = coord;

      //0 to 5
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 54, Coordinates.Y + 4), new Point(Coordinates.X + 54, Coordinates.Y + 30), "CrossC", 0));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 58, Coordinates.Y + 4), new Point(Coordinates.X + 64, Coordinates.Y + 30), "CrossC", 1));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 85, Coordinates.Y + 48), new Point(Coordinates.X + 85, Coordinates.Y + 4), "CrossC", 2));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 137, Coordinates.Y + 58), new Point(Coordinates.X + 100, Coordinates.Y + 58), "CrossC", 3));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 137, Coordinates.Y + 64), new Point(Coordinates.X + 100, Coordinates.Y + 63), "CrossC", 4));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 100, Coordinates.Y + 86), new Point(Coordinates.X + 147, Coordinates.Y + 86), "CrossC", 5));

      // 6 to 11
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 85, Coordinates.Y + 141), new Point(Coordinates.X + 85, Coordinates.Y + 100), "CrossC", 6));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 85, Coordinates.Y + 141), new Point(Coordinates.X + 75, Coordinates.Y + 100), "CrossC", 7));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 56, Coordinates.Y + 100), new Point(Coordinates.X + 56, Coordinates.Y + 143), "CrossC", 8));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 1, Coordinates.Y + 85), new Point(Coordinates.X + 50, Coordinates.Y + 85), "CrossC", 9));
      lanes.Add(new Lane(time, true, new Point(Coordinates.X + 1, Coordinates.Y + 83), new Point(Coordinates.X + 50, Coordinates.Y + 70), "CrossC", 10));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 50, Coordinates.Y + 54), new Point(Coordinates.X + 1, Coordinates.Y + 61), "CrossC", 11));

      // 12 to 17
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 56, Coordinates.Y + 40), new Point(Coordinates.X + 50, Coordinates.Y + 60), "CrossC", 12));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 64, Coordinates.Y + 40), new Point(Coordinates.X + 56, Coordinates.Y + 100), "CrossC", 13));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 74, Coordinates.Y + 47), new Point(Coordinates.X + 100, Coordinates.Y + 86), "CrossC", 14));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 100, Coordinates.Y + 58), new Point(Coordinates.X + 89, Coordinates.Y + 48), "CrossC", 15));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 100, Coordinates.Y + 65), new Point(Coordinates.X + 50, Coordinates.Y + 60), "CrossC", 16));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 100, Coordinates.Y + 63), new Point(Coordinates.X + 56, Coordinates.Y + 100), "CrossC", 17));

      // 18 to 23
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 88, Coordinates.Y + 95), new Point(Coordinates.X + 100, Coordinates.Y + 88), "CrossC", 18));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 72, Coordinates.Y + 100), new Point(Coordinates.X + 88, Coordinates.Y + 48), "CrossC", 19));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 72, Coordinates.Y + 100), new Point(Coordinates.X + 50, Coordinates.Y + 58), "CrossC", 20));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 50, Coordinates.Y + 88), new Point(Coordinates.X + 56, Coordinates.Y + 100), "CrossC", 21));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 40, Coordinates.Y + 70), new Point(Coordinates.X + 100, Coordinates.Y + 88), "CrossC", 22));
      lanes.Add(new Lane(time, false, new Point(Coordinates.X + 40, Coordinates.Y + 70), new Point(Coordinates.X + 88, Coordinates.Y + 48), "CrossC", 23));

      lanes[0].TrafficLight.Position = new Point(Coordinates.X + 25, Coordinates.Y + 27);
      lanes[0].TrafficLight.CPost = Coordinates;
      lanes[1].TrafficLight.Position = new Point(Coordinates.X + 35, Coordinates.Y + 27);
      lanes[1].TrafficLight.CPost = Coordinates;
      lanes[3].TrafficLight.Position = new Point(Coordinates.X + 100, Coordinates.Y + 30);
      lanes[3].TrafficLight.CPost = Coordinates;
      lanes[4].TrafficLight.Position = new Point(Coordinates.X + 100, Coordinates.Y + 40);
      lanes[4].TrafficLight.CPost = Coordinates;

      lanes[6].TrafficLight.Position = new Point(Coordinates.X + 110, Coordinates.Y + 100);
      lanes[6].TrafficLight.CPost = Coordinates;
      lanes[7].TrafficLight.Position = new Point(Coordinates.X + 100, Coordinates.Y + 100);
      lanes[7].TrafficLight.CPost = Coordinates;

      lanes[9].TrafficLight.Position = new Point(Coordinates.X + 26, Coordinates.Y + 110);
      lanes[9].TrafficLight.CPost = Coordinates;
      lanes[10].TrafficLight.Position = new Point(Coordinates.X + 26, Coordinates.Y + 100);
      lanes[10].TrafficLight.CPost = Coordinates;

      ChangeSpawnability("N", true);
      ChangeSpawnability("E", true);
      ChangeSpawnability("S", true);
      ChangeSpawnability("W", true);


      //Set the intial sequence of the 8 traffic light
      Random rand = new Random();
      int r = rand.Next(0, 4);
      if (r == 0)
      {
        lanes[0].TrafficLight.Colour = 3;
        lanes[1].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Timer = 10;
        lanes[4].TrafficLight.Timer = 20;
        lanes[6].TrafficLight.Timer = 10;
        lanes[7].TrafficLight.Timer = 10;

      }
      else if (r == 1)
      {
        lanes[3].TrafficLight.Colour = 3;
        lanes[4].TrafficLight.Colour = 3;
        lanes[6].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Timer = 10;
        lanes[7].TrafficLight.Timer = 20;
        lanes[9].TrafficLight.Timer = 10;
        lanes[10].TrafficLight.Timer = 10;
      }
      else if (r == 2)
      {
        lanes[6].TrafficLight.Colour = 3;
        lanes[7].TrafficLight.Colour = 3;
        lanes[9].TrafficLight.Colour = 3;
        lanes[6].TrafficLight.Timer = 10;
        lanes[10].TrafficLight.Timer = 20;
        lanes[0].TrafficLight.Timer = 10;
        lanes[1].TrafficLight.Timer = 10;
      }
      else
      {
        lanes[9].TrafficLight.Colour = 3;
        lanes[10].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Colour = 3;
        lanes[9].TrafficLight.Timer = 10;
        lanes[1].TrafficLight.Timer = 20;
        lanes[3].TrafficLight.Timer = 10;
        lanes[4].TrafficLight.Timer = 10;
      }


      lanes[0].TrafficLight.GreenInterval = 20;
      lanes[1].TrafficLight.GreenInterval = 10;
      lanes[3].TrafficLight.GreenInterval = 20;
      lanes[4].TrafficLight.GreenInterval = 10;
      lanes[6].TrafficLight.GreenInterval = 20;
      lanes[7].TrafficLight.GreenInterval = 10;
      lanes[9].TrafficLight.GreenInterval = 20;
      lanes[10].TrafficLight.GreenInterval = 10;

      lanes[0].TrafficLight.RedInterval = 20;
      lanes[1].TrafficLight.RedInterval = 30;
      lanes[3].TrafficLight.RedInterval = 20;
      lanes[4].TrafficLight.RedInterval = 30;
      lanes[6].TrafficLight.RedInterval = 20;
      lanes[7].TrafficLight.RedInterval = 30;
      lanes[9].TrafficLight.RedInterval = 20;
      lanes[10].TrafficLight.RedInterval = 30;

      //Connect all inside lane here  
      lanes[0].ConnectToLane(this.lanes[12]);
      lanes[12].ConnectToLane(this.lanes[11]);
      lanes[1].ConnectToLane(this.lanes[13]);
      lanes[13].ConnectToLane(this.lanes[8]);
      lanes[1].ConnectToLane(this.lanes[14]);
      lanes[14].ConnectToLane(this.lanes[5]);
      lanes[3].ConnectToLane(this.lanes[15]);
      lanes[15].ConnectToLane(this.lanes[2]);
      lanes[4].ConnectToLane(this.lanes[16]);
      lanes[16].ConnectToLane(this.lanes[11]);
      lanes[4].ConnectToLane(this.lanes[17]);
      lanes[17].ConnectToLane(this.lanes[8]);
      lanes[6].ConnectToLane(this.lanes[18]);
      lanes[18].ConnectToLane(this.lanes[5]);
      lanes[7].ConnectToLane(this.lanes[19]);
      lanes[19].ConnectToLane(this.lanes[2]);
      lanes[7].ConnectToLane(this.lanes[20]);
      lanes[20].ConnectToLane(this.lanes[11]);
      lanes[9].ConnectToLane(this.lanes[21]);
      lanes[21].ConnectToLane(this.lanes[8]);
      lanes[10].ConnectToLane(this.lanes[22]);
      lanes[22].ConnectToLane(this.lanes[5]);
      lanes[10].ConnectToLane(this.lanes[23]);
      lanes[23].ConnectToLane(this.lanes[2]);

      NrOfNeighbours = 0;
      connectedRoads = new Road[4];

      trafTime.Interval = 1000;
      trafTime.Elapsed += TrafTime_Elapsed;
    }


    /// <summary>
    /// Connecting the calling road to its neighbours "other" 
    /// </summary>
    /// <param name="side">The position of the neighbour, using 4 cardinal points (NESW)</param>
    /// <param name="other">The road object to be connected to</param>
    /// <returns>True if allowed to be connected, false otherwise</returns>
    public override bool ConnectToRoad(string side, Road other)
    {
      //First check the side to which the road "other" is connected
      //The code for side is the 4 cardinal NESW(for now)
      //Then, check whether it is possible to connect other to the calling road
      /*
       * For curve: 
       * SW = South West
       * SE = South East
       * NW = North West
       * NE = North East
       * 
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
              this.NrOfNeighbours++;
              return true;
            }
            else
            {
              this.NrOfNeighbours++;
              return false;
            }
          }

        case "E":
          {
            if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "NW")))
                || ((other is Straight) && other.GetDirection() == "H")
                || (other is CCrossroad)
                || (other is PCrossroad))
            {
              connectedRoads[1] = other;
              NrOfNeighbours++;
              return true;
            }
            else
            {
              NrOfNeighbours++;
              return false;
            }
          }

        case "S":
          {
            if (((other is Curve) && ((other.GetDirection() == "NE") || (other.GetDirection() == "NW")))
                || ((other is Straight) && other.GetDirection() == "V")
                || (other is CCrossroad)
                || (other is PCrossroad))
            {
              connectedRoads[2] = other;
              NrOfNeighbours++;
              return true;
            }
            else
            {
              NrOfNeighbours++;
              return false;
            }
          }

        case "W":
          {
            if (((other is Curve) && ((other.GetDirection() == "SE") || (other.GetDirection() == "NE")))
                || ((other is Straight) && other.GetDirection() == "H")
                || (other is CCrossroad)
                || (other is PCrossroad))
            {
              connectedRoads[3] = other;
              NrOfNeighbours++;
              return true;
            }
            else
            {
              NrOfNeighbours++;
              return false;
            }
          }

        default:
          {
            return false;
          }
      }
    }

    public override string GetDirection()
    {
      return "";
    }



    /// <summary>
    /// Connects the lanes of the calling road to the corresponding lanes of its neighbours
    /// </summary>
    /// <param name="side">The position of neighbour</param>
    /// <param name="other">The road object it is connected to</param>
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
              lanes[2].ConnectToLane(other.getListOfLane()[7]);
              lanes[2].ConnectToLane(other.getListOfLane()[6]);
              other.getListOfLane()[8].ConnectToLane(this.lanes[0]);
              other.getListOfLane()[8].ConnectToLane(this.lanes[1]);
            }
            else if (other is PCrossroad)
            {
              lanes[2].ConnectToLane(other.getListOfLane()[5]);
              other.getListOfLane()[6].ConnectToLane(this.lanes[0]);
              other.getListOfLane()[6].ConnectToLane(this.lanes[1]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "SW")
              {
                lanes[2].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
              }
              else // SE
              {
                lanes[2].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
              }
            }
            else
            {
              lanes[2].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
            }
          }
          break;
        case "E":
          {
            if (other is CCrossroad)
            {
              lanes[5].ConnectToLane(other.getListOfLane()[9]);
              lanes[5].ConnectToLane(other.getListOfLane()[10]);
              other.getListOfLane()[11].ConnectToLane(this.lanes[3]);
              other.getListOfLane()[11].ConnectToLane(this.lanes[4]);
            }
            else if (other is PCrossroad)
            {
              lanes[5].ConnectToLane(other.getListOfLane()[7]);
              lanes[5].ConnectToLane(other.getListOfLane()[8]);
              other.getListOfLane()[9].ConnectToLane(this.lanes[3]);
              other.getListOfLane()[9].ConnectToLane(this.lanes[4]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "SW")
              {
                lanes[5].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[3]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[4]);
              }
              else //NW
              {
                lanes[5].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[4]);
              }
            }
            else
            {
              lanes[5].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
              other.getListOfLane()[0].ConnectToLane(this.lanes[4]);
            }
          }
          break;
        case "S":
          {
            if (other is CCrossroad)
            {
              lanes[8].ConnectToLane(other.getListOfLane()[0]);
              lanes[8].ConnectToLane(other.getListOfLane()[1]);
              other.getListOfLane()[2].ConnectToLane(this.lanes[6]);
              other.getListOfLane()[2].ConnectToLane(this.lanes[7]);
            }
            else if (other is PCrossroad)
            {
              lanes[8].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "NW")
              {
                lanes[8].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
              }
              else //NE
              {
                lanes[8].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[6]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[7]);
              }
            }
            else
            {
              lanes[8].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
            }
          }
          break;
        case "W":
          {
            if (other is CCrossroad)
            {
              lanes[11].ConnectToLane(other.getListOfLane()[3]);
              lanes[11].ConnectToLane(other.getListOfLane()[4]);
              other.getListOfLane()[5].ConnectToLane(this.lanes[10]);
              other.getListOfLane()[5].ConnectToLane(this.lanes[9]);
            }
            else if (other is PCrossroad)
            {
              lanes[11].ConnectToLane(other.getListOfLane()[2]);
              lanes[11].ConnectToLane(other.getListOfLane()[3]);
              other.getListOfLane()[4].ConnectToLane(this.lanes[10]);
              other.getListOfLane()[4].ConnectToLane(this.lanes[9]);
            }
            else if (other is Curve)
            {
              if (other.GetDirection() == "NE")
              {
                lanes[11].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[9]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[10]);
              }
              else // SE
              {
                lanes[11].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[9]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[10]);
              }
            }
            else
            {
              lanes[11].ConnectToLane(other.getListOfLane()[0]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[9]);
              other.getListOfLane()[1].ConnectToLane(this.lanes[10]);
            }
          }
          break;
        default:
          {
            return;
          }
      }
    }

    /// <summary>
    /// Change the spawn time of spawnable lane to specified time
    /// </summary>
    /// <param name="time">The interval for which the car is spawned</param>
    public override void AdjustSpawnTime(int time)
    {
      //Gets the list of possible spawnable lanes
      //then change all the spawntime
      List<Lane> temp = new List<Lane>
      {
        lanes[0], lanes[1],
        lanes[3], lanes[4],
        lanes[6], lanes[7],
        lanes[9], lanes[10]
      };

      foreach (Lane t in temp)
      {
        t.SpawnTime = time;
      }
    }

    /// <summary>
    /// Disconnect the neighbour road r from the list of neighbours
    /// </summary>
    /// <param name="r">The neighbour road object to be deleted</param>
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
      NrOfNeighbours--;
    }

    /// <summary>
    /// Disconnect this road from the grid, disconnect itself form neighbour along with all of its lanes
    /// </summary>
    public override void DisconnectRoad()
    {
      for (int i = 0; i < connectedRoads.Count(); i++)
      {
        if (connectedRoads[i] != null)
        {
          // north neighbour
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
          // east neighbour
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
                connectedRoads[i].getListOfLane()[0].DisconnectLanes();
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
          // south neighbour
          else if (i == 2)
          {
            if (connectedRoads[i] is Curve)
            {
              if (connectedRoads[i].GetDirection() == "NW")
              {
                connectedRoads[i].getListOfLane()[1].DisconnectLanes();
              }
              else
              {
                connectedRoads[i].getListOfLane()[0].DisconnectLanes();
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
          // west neighbour
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
            lanes[0].Spawnable = spawnAble;
            lanes[1].Spawnable = spawnAble;
          }
          break;
        case "E":
          {
            lanes[3].Spawnable = spawnAble;
            lanes[4].Spawnable = spawnAble;
          }
          break;
        case "S":
          {
            lanes[6].Spawnable = spawnAble;
            lanes[7].Spawnable = spawnAble;
          }
          break;
        case "W":
          {
            lanes[9].Spawnable = spawnAble;
            lanes[10].Spawnable = spawnAble;
          }
          break;

      }
    }

    public override void Draw(ref Graphics gr)
    {
      gr.DrawImage(image, Coordinates.X, Coordinates.Y, 150, 150);
      int[] id = { 0, 1, 3, 4, 6, 7, 9, 10 };
      for (int i = 0; i < id.Length; i++)
      {
        this.lanes[id[i]].TrafficLight.Draw(ref gr);
      }
    }

    public override void DrawTrafficLight(ref Graphics gr)
    {
      int[] id = { 0, 1, 3, 4, 6, 7, 9, 10 };
      for (int i = 0; i < id.Length; i++)
      {
        if (this.lanes[id[i]].TrafficLight.ColorChanged)
        {
          this.lanes[id[i]].TrafficLight.Draw(ref gr);
          this.lanes[id[i]].TrafficLight.ColorChanged = false;
        }
      }
    }

    private void TrafTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      lanes[0].TrafficLight.IncTimer();
      lanes[1].TrafficLight.IncTimer();
      lanes[3].TrafficLight.IncTimer();
      lanes[4].TrafficLight.IncTimer();
      lanes[6].TrafficLight.IncTimer();
      lanes[7].TrafficLight.IncTimer();
      lanes[9].TrafficLight.IncTimer();
      lanes[10].TrafficLight.IncTimer();
    }

    public void AdjustGreenTime(int green)
    {
      lanes[0].TrafficLight.AdjustGreenTime(green * 2);
      lanes[1].TrafficLight.AdjustGreenTime(green);
      lanes[3].TrafficLight.AdjustGreenTime(green * 2);
      lanes[4].TrafficLight.AdjustGreenTime(green);
      lanes[6].TrafficLight.AdjustGreenTime(green * 2);
      lanes[7].TrafficLight.AdjustGreenTime(green);
      lanes[9].TrafficLight.AdjustGreenTime(green * 2);
      lanes[10].TrafficLight.AdjustGreenTime(green);
    }

    public override void RemoveLanes()
    {
      for (int i = 0; i < lanes.Count; i++)
      {
        lanes.RemoveAt(i);
      }

    }

    public void resetTrafficLight()
    {
      Random rand = new Random();
      int r = rand.Next(0, 4);
      if (r == 0)
      {
        lanes[0].TrafficLight.Colour = 3;
        lanes[1].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.GreenInterval / 2;
        lanes[4].TrafficLight.Timer = this.lanes[4].TrafficLight.GreenInterval * 2;
        lanes[6].TrafficLight.Timer = this.lanes[6].TrafficLight.GreenInterval / 2;
        lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.GreenInterval;

        lanes[4].TrafficLight.Colour = 1;
        lanes[6].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[9].TrafficLight.Colour = 1;
        lanes[10].TrafficLight.Colour = 1;
        lanes[1].TrafficLight.Timer = 0;
        lanes[3].TrafficLight.Timer = 0;
        lanes[9].TrafficLight.Timer = 0;
        lanes[10].TrafficLight.Timer = 0;
      }
      else if (r == 1)
      {
        lanes[3].TrafficLight.Colour = 3;
        lanes[4].TrafficLight.Colour = 3;
        lanes[6].TrafficLight.Colour = 3;
        lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.GreenInterval / 2;
        lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.GreenInterval * 2;
        lanes[9].TrafficLight.Timer = this.lanes[9].TrafficLight.GreenInterval / 2;
        lanes[10].TrafficLight.Timer = this.lanes[10].TrafficLight.GreenInterval;

        lanes[0].TrafficLight.Colour = 1;
        lanes[1].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[9].TrafficLight.Colour = 1;
        lanes[10].TrafficLight.Colour = 1;
        lanes[0].TrafficLight.Timer = 0;
        lanes[1].TrafficLight.Timer = 0;
        lanes[4].TrafficLight.Timer = 0;
        lanes[6].TrafficLight.Timer = 0;
      }
      else if (r == 2)
      {
        lanes[6].TrafficLight.Colour = 3;
        lanes[7].TrafficLight.Colour = 3;
        lanes[9].TrafficLight.Colour = 3;
        lanes[6].TrafficLight.Timer = this.lanes[6].TrafficLight.GreenInterval / 2;
        lanes[10].TrafficLight.Timer = this.lanes[10].TrafficLight.GreenInterval * 2;
        lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.GreenInterval / 2;
        lanes[1].TrafficLight.Timer = this.lanes[1].TrafficLight.GreenInterval;

        lanes[0].TrafficLight.Colour = 1;
        lanes[1].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Colour = 1;
        lanes[4].TrafficLight.Colour = 1;
        lanes[10].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Timer = 0;
        lanes[4].TrafficLight.Timer = 0;
        lanes[7].TrafficLight.Timer = 0;
        lanes[9].TrafficLight.Timer = 0;
      }
      else
      {
        lanes[9].TrafficLight.Colour = 3;
        lanes[10].TrafficLight.Colour = 3;
        lanes[0].TrafficLight.Colour = 3;
        lanes[9].TrafficLight.Timer = this.lanes[9].TrafficLight.GreenInterval / 2;
        lanes[1].TrafficLight.Timer = this.lanes[1].TrafficLight.GreenInterval * 2;
        lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.GreenInterval / 2;
        lanes[4].TrafficLight.Timer = this.lanes[4].TrafficLight.GreenInterval;

        lanes[1].TrafficLight.Colour = 1;
        lanes[3].TrafficLight.Colour = 1;
        lanes[4].TrafficLight.Colour = 1;
        lanes[6].TrafficLight.Colour = 1;
        lanes[7].TrafficLight.Colour = 1;
        lanes[0].TrafficLight.Timer = 0;
        lanes[6].TrafficLight.Timer = 0;
        lanes[7].TrafficLight.Timer = 0;
        lanes[10].TrafficLight.Timer = 0;
      }

    }


  }
}
