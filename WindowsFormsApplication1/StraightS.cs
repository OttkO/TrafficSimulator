using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tracy
{
    public class Straight : Road
    {
        private readonly string direction;
        //Horizontal or vertical

        /* Denny Version
        /// <summary>
        /// Constructor of the Straight road
        /// </summary>
        /// <param name="Coord">Coordinates of the road in the grid</param>
        /// <param name="dir">direction of the straight road, vertical or horizontal</param>
        public Straight(Point Coord, string dir)
        {
            //assign direction, coordinates and images
            //creates list of lane and neighbours
            //Make all spawnable lane spawnable
            direction = dir;
            switch (dir)
            {
                case "H":
                    {
                        Form1 x = (Form1)Form1.ActiveForm;
                        image = x.imageList2.Images[6];
                    }
                    break;
                case "V":
                    {
                        Form1 x = (Form1)Form1.ActiveForm;
                        image = x.imageList2.Images[7];
                    }
                    break;
                
            }
            this.lanes = new List<Lane>();
            for (int i = 0; i < 2; i++)
            {
                lanes.Add(new Lane(time, false));
            }

            this.changeSpawnability("N", true);
            this.changeSpawnability("S", true);

            nrOfNeighbours = 0;
            this.coordinates = Coord;
            connectedRoads = new Road[2];
            this.time.Enabled = true;
        }
        */

        //JP version
        public Straight(Point Coord, string dir)
        {
            this.lanes = new List<Lane>();
            //assign direction, coordinates and images
            //creates list of lane and neighbours
            //Make all spawnable lane spawnable
            direction = dir;
            this.coordinates = Coord;
            this.disableTimer();
            

            switch (dir)
            {
                case "H":
                    {
                        Tracy x = (Tracy)Tracy.ActiveForm;
                        image = Resource1.StraightH;
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 150, Coord.Y + 60), new Point(Coord.X, Coord.Y + 60), "H"));
                        lanes.Add(new Lane(time, false, new Point(Coord.X, Coord.Y + 85), new Point(Coord.X + 140, Coord.Y + 85), "H"));

                    }
                    break;
                case "V":
                    {
                        Tracy x = (Tracy)Tracy.ActiveForm;
                        image = Resource1.StraightV;
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 55, Coord.Y), new Point(Coord.X + 55, Coord.Y + 150), "V"));
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 80, Coord.Y + 150), new Point(Coord.X + 80, Coord.Y), "V"));

                    }
                    break;

            }

            nrOfNeighbours = 0;

            connectedRoads = new Road[2];
        }

        /// <summary>
        /// Returns the direction of the straight(Vertical or Horizontal)
        /// </summary>
        /// <returns>Direction, V for Vertical H for Horizontal</returns>
        public override string getDirection()
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
                            if (((other is Curve) && ((other.getDirection() == "SW") || (other.getDirection() == "SE")))
                                || ((other is Straight) && other.getDirection() == "V")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                nrOfNeighbours++;
                                connectedRoads[0] = other;
                                //this.ConnectLanesTo("N", other);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                    case "S":
                        {
                            if (((other is Curve) && ((other.getDirection() == "NE") || (other.getDirection() == "NW")))
                                || ((other is Straight) && other.getDirection() == "V")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                nrOfNeighbours++;
                                connectedRoads[1] = other;
                                //this.ConnectLanesTo("S", other);
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
                            // throw new Exception("Trying to connect to an unrecognized side of the road");                       
                        }
                }
            }
            else
            {
                switch (side)
                {

                    case "E":
                        {
                            if (((other is Curve) && ((other.getDirection() == "SW") || (other.getDirection() == "NW")))
                                || ((other is Straight) && other.getDirection() == "H")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                nrOfNeighbours++;
                                connectedRoads[0] = other;
                               // this.ConnectLanesTo("E", other);

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    case "W":
                        {
                            if (((other is Curve) && ((other.getDirection() == "SE") || (other.getDirection() == "NE")))
                                || ((other is Straight) && other.getDirection() == "H")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                nrOfNeighbours++;
                                connectedRoads[1] = other;
                                //this.ConnectLanesTo("W", other);
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
                            // throw new Exception("Trying to connect to an unrecognized side of the road");                       
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
                                if (other.getDirection() == "SW")
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
                                if (other.getDirection() == "NW")
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
                                if (other.getDirection() == "SW")
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
                                if (other.getDirection() == "SE")
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
            this.nrOfNeighbours--;
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
                                if (connectedRoads[i].getDirection() == "SW")
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
                            connectedRoads[i].changeSpawnability("W", true);
                        }
                        else if (i == 1)
                        {
                            if (connectedRoads[i] is Curve)
                            {
                                if (connectedRoads[i].getDirection() == "SE")
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
                            connectedRoads[i].changeSpawnability("E", true);
                        }

                    }
                    else
                    {
                        //vertical north neighbour
                        if (i == 0)
                        {
                            if (connectedRoads[i] is Curve)
                            {
                                if (connectedRoads[i].getDirection() == "SE")
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
                            connectedRoads[i].changeSpawnability("S", true);
                            connectedRoads[i].DisconnectNeighbour("S");
                        }
                        //vertical's south neighbour
                        else if (i == 1)
                        {
                            if (connectedRoads[i] is Curve)
                            {
                                if (connectedRoads[i].getDirection() == "NW")
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
                            connectedRoads[i].changeSpawnability("N", true);
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
        public override void changeSpawnability(string side, bool spawnAble)
        {
            switch (side)
            {
                case "N":
                case "E":
                    {
                        this.lanes[0].SpawnAble = spawnAble;
                    }
                    break;
                case "S":
                case "W":
                    {
                        this.lanes[1].SpawnAble = spawnAble;
                    }
                    break;

            }
        }

        public override void Draw(ref Graphics gr)
        {
            gr.DrawImage(image, coordinates.X, coordinates.Y, 150, 150);
            /*foreach (Lane mylane in this.lanes)
            {
                mylane.drawCars(ref gr);
            }*/
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
