using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TrafficSimulator
{
  public class Straight : Road
  {
    private readonly string direction;

    public Straight(Point Coord, string dir)
    {
      this.lanes = new List<Lane>();
      //assign direction, coordinates and images
      //creates list of lane and neighbours
      //Make all spawnable lane spawnable
      direction = dir;
      this.Coordinates = Coord;
      this.DisableTimer();


      switch (dir)
      {
        case "H":
          {
            image = new Bitmap(@"Resources\StraightH.jpg");
            lanes.Add(new Lane(time, false, new Point(Coord.X + 150, Coord.Y + 60), new Point(Coord.X, Coord.Y + 60), "H"));
            lanes.Add(new Lane(time, false, new Point(Coord.X, Coord.Y + 85), new Point(Coord.X + 140, Coord.Y + 85), "H"));

          }
          break;
        case "V":
          {
            image = new Bitmap(@"Resources\StraightV.jpg");
            lanes.Add(new Lane(time, false, new Point(Coord.X + 55, Coord.Y), new Point(Coord.X + 55, Coord.Y + 150), "V"));
            lanes.Add(new Lane(time, false, new Point(Coord.X + 80, Coord.Y + 150), new Point(Coord.X + 80, Coord.Y), "V"));

          }
          break;

      }

      NrOfNeighbours = 0;

      connectedRoads = new Road[2];
    }

    /// <summary>
    /// Returns the direction of the straight(Vertical or Horizontal)
    /// </summary>
    /// <returns>Direction, V for Vertical H for Horizontal</returns>
    public override string GetDirection()
    {
      return direction;
    }

    /// <summary>
    /// Connecting the calling road to its neighbours "other" 
    /// </summary>
    /// <param name="side">The position of the neighbour, using 4 cardinal points (NESW)</param>
    /// <param name="other">The road object to be connected to</param>
    /// <returns>True if allowed to be connected, false otherwise</returns>
    public override bool ConnectToRoad(string side, Road other)
    {
      //Code for side is 4 cardinal point NESW
      if (direction == "V")
      {
        switch (side)
        {
          case "N":
            {
              if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "SE")))
                  || ((other is Straight) && other.GetDirection() == "V")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
              {
                NrOfNeighbours++;
                connectedRoads[0] = other;
                return true;
              }
              else
              {
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
                NrOfNeighbours++;
                connectedRoads[1] = other;
                return true;
              }
              else
              {
                return false;
              }
            }

          default:
            {
              return false;
            }
        }
      }
      else
      {
        switch (side)
        {

          case "E":
            {
              if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "NW")))
                  || ((other is Straight) && other.GetDirection() == "H")
                  || (other is CCrossroad)
                  || (other is PCrossroad))
              {
                NrOfNeighbours++;
                connectedRoads[0] = other;

                return true;
              }
              else
              {
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
                NrOfNeighbours++;
                connectedRoads[1] = other;
                return true;
              }
              else
              {
                return false;
              }
            }
          default:
            {
              return false;
            }
        }
      }

    }

    /// <summary>
    /// Connects the lanes of the calling road to the corresponding lanes of its neighbours
    /// </summary>
    /// <param name="side">The position of neighbour</param>
    /// <param name="other">The road object it is connected to</param>
    public override void ConnectLanesTo(string side, Road other)
    {
      //First, it checks the direction of the straight road, horizontal or vertical
      //Then, if V consider only north and south, else east and west
      //Next check the type of road connected and connect their corresponding lanes
      if (direction == "V")
      {
        switch (side)
        {
          case "N":
            {
              if (other is CCrossroad)
              {
                //done
                //not tested
                this.lanes[1].ConnectToLane(other.getListOfLane()[6]);
                this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                other.getListOfLane()[8].ConnectToLane(this.lanes[0]);
              }
              else if (other is PCrossroad)
              {
                //done
                //not tested
                this.lanes[1].ConnectToLane(other.getListOfLane()[5]);
                other.getListOfLane()[6].ConnectToLane(this.lanes[0]);
              }
              else if (other is Curve)
              {
                if (other.GetDirection() == "SW")
                {
                  //done
                  this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                  other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                }

                else //SE
                {
                  //done
                  this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                  other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                }
              }
              else
              {
                //done
                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
              }
            }
            break;

          case "S":
            {
              if (other is CCrossroad)
              {
                //done
                //not tested
                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[2].ConnectToLane(this.lanes[1]);
              }
              else if (other is PCrossroad)
              {
                //done
                //not tested
                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);

              }
              else if (other is Curve)
              {
                if (other.GetDirection() == "NW")
                {
                  //done
                  this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                  other.getListOfLane()[1].ConnectToLane(this.lanes[1]);

                }
                //NE
                else
                {
                  //done
                  this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                  other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                }
              }
              else
              {
                //done
                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
              }
            }
            break;

          default:
            {
              return;
            }
        }
      }
      //Horizontal
      else
      {
        switch (side)
        {
          case "E":
            {
              if (other is CCrossroad)
              {
                //done
                //not tested
                this.lanes[1].ConnectToLane(other.getListOfLane()[9]);
                this.lanes[1].ConnectToLane(other.getListOfLane()[10]);
                other.getListOfLane()[11].ConnectToLane(this.lanes[0]);

              }
              else if (other is PCrossroad)
              {
                //done
                //not tested
                this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                this.lanes[1].ConnectToLane(other.getListOfLane()[8]);
                other.getListOfLane()[9].ConnectToLane(this.lanes[0]);
              }
              else if (other is Curve)
              {
                if (other.GetDirection() == "SW")
                {
                  //done
                  this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                  other.getListOfLane()[1].ConnectToLane(this.lanes[0]);

                }
                else //NW
                {
                  //done
                  this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                  other.getListOfLane()[0].ConnectToLane(this.lanes[0]);

                }
              }
              else
              {
                //done
                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
              }
            }
            break;
          case "W":
            {
              if (other is CCrossroad)
              {
                //done
                //not tested
                this.lanes[0].ConnectToLane(other.getListOfLane()[3]);
                this.lanes[0].ConnectToLane(other.getListOfLane()[4]);
                other.getListOfLane()[5].ConnectToLane(this.lanes[1]);

              }
              else if (other is PCrossroad)
              {
                //done
                //not tested
                this.lanes[0].ConnectToLane(other.getListOfLane()[2]);
                this.lanes[0].ConnectToLane(other.getListOfLane()[3]);
                other.getListOfLane()[4].ConnectToLane(this.lanes[1]);
              }
              else if (other is Curve)
              {
                if (other.GetDirection() == "SE")
                {
                  //done
                  this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                  other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                }
                else // NE
                {
                  //done
                  this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                  other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                }
              }
              else
              {
                //done
                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
              }
            }
            break;
          default:
            {
              return;
            }
        }
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
          connectedRoads[0] = null;
          break;
        case "S":
          connectedRoads[1] = null;
          break;
        case "W":
          connectedRoads[1] = null;
          break;
      }
      this.NrOfNeighbours--;
    }

    /// <summary>
    /// Change the spawn time of spawnable lane to specified time
    /// </summary>
    /// <param name="time">The interval for which the car is spawned</param>
    public override void AdjustSpawnTime(int time)
    {
      //Gets the list of possible spawnable lanes
      //then change all the spawntime
      foreach (Lane l in lanes)
      {
        l.SpawnTime = time;
      }
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
          if (direction == "H")
          {
            //Horizontal's east neighbour
            if (i == 0)
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
            else if (i == 1)
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
              {   //pcrossroad
                connectedRoads[i].getListOfLane()[4].DisconnectLanes();
              }
              connectedRoads[i].DisconnectNeighbour("E");
              connectedRoads[i].ChangeSpawnability("E", true);
            }

          }
          else
          {
            //vertical north neighbour
            if (i == 0)
            {
              if (connectedRoads[i] is Curve)
              {
                if (connectedRoads[i].GetDirection() == "SE")
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
              connectedRoads[i].ChangeSpawnability("S", true);
              connectedRoads[i].DisconnectNeighbour("S");
            }
            //vertical's south neighbour
            else if (i == 1)
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
        case "E":
          {
            this.lanes[0].Spawnable = spawnAble;
          }
          break;
        case "S":
        case "W":
          {
            this.lanes[1].Spawnable = spawnAble;
          }
          break;

      }
    }

    public override void Draw(ref Graphics g)
    {
      g.DrawImage(image, Coordinates.X, Coordinates.Y, 150, 150);
    }

    public override void RemoveLanes()
    {
      for (int i = 0; i < lanes.Count; i++)
      {
        this.lanes.RemoveAt(i);
      }

    }
  }
}
