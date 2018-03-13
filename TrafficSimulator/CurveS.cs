using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TrafficSimulator
{
    public class Curve : Road
    {
        private readonly string direction;

        /// <summary>
        /// Constructor of Curve
        /// </summary>
        /// <param name="Coord">Coordinates of the road</param>
        /// <param name="dir">direction of the curve, SW, SE, NW, or NE</param>
        public Curve(Point Coord, string dir)
        {
            //assign direction, coordinates and images
            //creates list of lane and neighbours
            //Make all spawnable lane spawnable
            direction = dir;
            this.lanes = new List<Lane>();
            this.DisableTimer();
         
            switch (dir)
            {
                case "NE":
                    {
                        image = Resource1.curveNE;
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 150, Coord.Y + 65), new Point(Coord.X + 85, Coord.Y), direction));
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 65, Coord.Y), new Point(Coord.X + 150, Coord.Y + 85), direction));
                    }
                    break;
                case "SE":
                    {
                        image = Resource1.curveSE;
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 150, Coord.Y + 65), new Point(Coord.X + 65, Coord.Y + 150), direction));
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 85, Coord.Y + 150), new Point(Coord.X + 150, Coord.Y + 85), direction));
                    }
                    break;
                case "SW":
                    {
                        image = Resource1.curveSW;
                        lanes.Add(new Lane(time, false, new Point(Coord.X, Coord.Y + 85), new Point(Coord.X + 65, Coord.Y + 150), direction));
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 85, Coord.Y + 150), new Point(Coord.X, Coord.Y + 35), direction));
                    }
                    break;
                case "NW":
                    {
                        image = Resource1.curveNW;
                        lanes.Add(new Lane(time, false, new Point(Coord.X + 65, Coord.Y), new Point(Coord.X, Coord.Y + 65), direction));
                        lanes.Add(new Lane(time, false, new Point(Coord.X, Coord.Y + 85), new Point(Coord.X + 85, Coord.Y), direction));
                    }
                    break;

            }

            NrOfNeighbours = 0;
            this.Coordinates = Coord;
            connectedRoads = new Road[2];
        }

        public override string GetDirection()
        {
            return direction;
        }

        private bool directionChecker(string side)
        {
            bool value = false;
            if (direction == "NE" && (side == "N" || side == "E"))
            {
                    value = true;
            }
            else if (direction == "NW" && (side == "N" || side == "W"))
            {
                    value = true;

            }
            else if (direction == "SE" && (side == "S" || side == "E"))
            {              
                    value = true;               
            }
            else if (direction == "SW" && (side == "S" || side == "W"))
            {            
                    value = true;               
            }
            return value;
        }

        public override bool ConnectToRoad(string side, Road other)
        {
            //First check the side to which the road "other" is connected
            //The code for side is the 4 cardinal NESW(for now)
            //Then, check whether it is possile to connect other to the calling road
            /*
             * For curve: 
             * SW = South West
             * SE = South East
             * NW = North West
             * NE = North East
             * 
             * For straight:
             * V= Vertical
             * H=Horizaontal
             */

            bool result = false;

            if (directionChecker(side))
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
                                connectedRoads[0] = other;
                                result = true;
                            }
                        }
                        break;
                    case "E":
                        {
                            if (((other is Curve) && ((other.GetDirection() == "SW") || (other.GetDirection() == "NW")))
                                || ((other is Straight) && other.GetDirection() == "H")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                connectedRoads[1] = other;
                                result = true;
                            }
                        }
                        break;
                    case "S":
                        {
                            if (((other is Curve) && ((other.GetDirection() == "NE") || (other.GetDirection() == "NW")))
                                || ((other is Straight) && other.GetDirection() == "V")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                connectedRoads[0] = other;
                                result = true;
                            }
                        }
                        break;
                    case "W":
                        {
                            if (((other is Curve) && ((other.GetDirection() == "SE") || (other.GetDirection() == "NE")))
                                || ((other is Straight) && other.GetDirection() == "H")
                                || (other is CCrossroad)
                                || (other is PCrossroad))
                            {
                                connectedRoads[1] = other;
                                result = true;
                            }
                        }
                        break;
                    default:
                        {
                            return false;    
                        }
                }
            }
            return result;
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
                            if (direction == "NW")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[6]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                                other.getListOfLane()[8].ConnectToLane(this.lanes[0]);
                            }
                            else if (direction == "NE")
                            {
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[6]);
                                this.lanes[0].ConnectToLane(other.getListOfLane()[7]);
                                other.getListOfLane()[8].ConnectToLane(this.lanes[1]);
                            }
                        }
                        else if (other is PCrossroad)
                        {
                            if (direction == "NW")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[5]);
                                other.getListOfLane()[6].ConnectToLane(this.lanes[0]);
                            }
                            else if (direction == "NE")
                            {
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[5]);
                                other.getListOfLane()[6].ConnectToLane(this.lanes[1]);
                            }
                        }
                        else if (other is Curve)
                        {
                            if (direction == "NW")
                            {
                                if (other.GetDirection() == "SW")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(lanes[0]);
                                }
                                else if (other.GetDirection() == "SE")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(lanes[0]);
                                }
                            }
                            else if (direction == "NE")
                            {
                                if (other.GetDirection() == "SW")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(lanes[1]);
                                }
                                else if (other.GetDirection() == "SE")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(lanes[1]);
                                }
                            }
                        }
                        //Straight (V)
                        else
                        {
                            if (direction == "NW")
                            {
                                //done
                                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                            }
                            else if (direction == "NE")
                            {
                                //done
                                this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                            }
                        }
                    }
                    break;
                case "E":
                    {
                        if (other is CCrossroad)
                        {
                            if (direction == "SE")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[9]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[10]);
                                other.getListOfLane()[11].ConnectToLane(this.lanes[0]);
                               
                            }
                            else if (direction == "NE")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[9]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[10]);
                                other.getListOfLane()[11].ConnectToLane(this.lanes[0]);
                            }
                        }
                        else if (other is PCrossroad)
                        {
                            if (direction == "SE")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[8]);
                                other.getListOfLane()[9].ConnectToLane(this.lanes[0]);
                               
                            }
                            else if (direction == "NE")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[8]);
                                other.getListOfLane()[9].ConnectToLane(this.lanes[0]);
                               
                            }
                        }
                        else if (other is Curve)
                        {
                           
                            
                            if (direction == "SE")
                            {
                                if (other.GetDirection() == "SW")
                                {
                                    //done?
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[0]);
                                }
                                else if (other.GetDirection() == "NW")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                                }
                            }
                            else if (direction == "NE")
                            {
                                if (other.GetDirection() == "SW")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[0]);
                                }
                                else if (other.GetDirection() == "NW")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                                }
                            }
                        }
                        //straight
                        //counts for SE and NE
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
                            if (direction == "SW")
                            {

                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[2].ConnectToLane(this.lanes[1]);
                               
                            }
                            else if (direction == "SE")
                            { 
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[2].ConnectToLane(this.lanes[1]);
                            }
                        }
                        else if (other is PCrossroad)
                        {
                            if (direction == "SW")
                            {
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                            }
                            else if (direction == "SE")
                            {
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                            }
                        }
                        else if (other is Curve)
                        {

                            if (direction == "SW")
                            {
                                if (other.GetDirection() == "NW")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                                }
                                else if (other.GetDirection() == "NE")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                                }
                            }
                            else if (direction == "SE")
                            {
                                if (other.GetDirection() == "NW")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                                }
                                else if (other.GetDirection() == "NE")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[1]);
                                    other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                                }
                            }
                        }
                        else
                        {
                            if (direction == "SW")
                            {
                                //done
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                            }
                            else if (direction == "SE")
                            {
                                //done 
                                this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                            }
                        }
                    }
                    break;
                case "W":
                    {
                        if (other is CCrossroad)
                        {
                            if (direction == "NW")
                            {

                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[3]);
                                this.lanes[0].ConnectToLane(other.getListOfLane()[4]);
                                other.getListOfLane()[5].ConnectToLane(this.lanes[1]);

                                
                            }
                            else if (direction == "SW")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[3]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[4]);
                                other.getListOfLane()[5].ConnectToLane(this.lanes[0]);
                            }
                        }
                        else if (other is PCrossroad)
                        {
                            if (direction == "NW")
                            {
                                //done
                                //not tested
                                this.lanes[0].ConnectToLane(other.getListOfLane()[2]);
                                this.lanes[0].ConnectToLane(other.getListOfLane()[3]);
                                other.getListOfLane()[4].ConnectToLane(this.lanes[1]);

                            }
                            else if (direction == "SW")
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[2]);
                                this.lanes[1].ConnectToLane(other.getListOfLane()[3]);
                                other.getListOfLane()[4].ConnectToLane(this.lanes[0]);
                            }
                        }
                        else if (other is Curve)
                        {

                            if (direction == "NW")
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                                }
                                else if (other.GetDirection() == "SE")
                                {
                                    //done
                                    this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                                }
                            }
                            else if (direction == "SW")
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[0]);
                                }
                                else if (other.GetDirection() == "SE")
                                {
                                    //done
                                    this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                                    other.getListOfLane()[1].ConnectToLane(this.lanes[0]);
                                }
                            }
                        }
                        else
                        {
                            if (direction == "NW")
                            {
                                 //done
                                 this.lanes[0].ConnectToLane(other.getListOfLane()[0]);
                                 other.getListOfLane()[1].ConnectToLane(this.lanes[1]);
                            }
                            else if (direction == "SW")
                            {
                                 //done
                                 this.lanes[1].ConnectToLane(other.getListOfLane()[0]);
                                 other.getListOfLane()[1].ConnectToLane(this.lanes[0]);
                            }
                        }
                    }
                    break;
                default:
                    {
                        throw new Exception("Trying to connect to an unrecognized side of the road");
                    }
            }
        }

        public override void AdjustSpawnTime(int time)
        {
            //Gets the list of possible spawnable lanes
            //then change all the spawntime
            for (int i = 0; i < lanes.Count; i++)
            {
                lanes[i].SpawnTime = time;
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
                case "S":
                    connectedRoads[0] = null;
                    break;
                case "E":
                case "W":
                    connectedRoads[1] = null;
                    break;
            }
            this.NrOfNeighbours--;
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
                    Road other = connectedRoads[i];
                    if (direction == "SW")
                    {
                        //SW's south neighbour
                        if (i == 0)
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[1].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[2].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[1].DisconnectLanes();
                            }
                            other.ChangeSpawnability("N", true);
                            other.DisconnectNeighbour("N");
                        }
                        //SW's west neighbour
                        else
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[1].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[5].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[4].DisconnectLanes();
                            }
                            other.ChangeSpawnability("E", true);
                            other.DisconnectNeighbour("E");
                        }


                    }
                    else if (direction == "SE")
                    {
                        //SE's south neighbour
                        if (i == 0)
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[1].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[2].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[1].DisconnectLanes();
                            }
                            other.ChangeSpawnability("N", true);
                            other.DisconnectNeighbour("N");
                        }
                        //SE's east neighbour
                        else
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NW")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[0].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[11].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[9].DisconnectLanes();
                            }
                            other.ChangeSpawnability("W", true);
                            other.DisconnectNeighbour("W");
                        }
                    }
                    else if (direction == "NE")
                    {
                        //NE's north neighbour
                        if (i == 0)
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "SE")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[0].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[8].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[6].DisconnectLanes();
                            }
                            other.ChangeSpawnability("S", true);
                            other.DisconnectNeighbour("S");
                        }
                        //NE's east neighbour
                        else
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NW")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[0].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[11].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[9].DisconnectLanes();
                            }
                            other.ChangeSpawnability("W", true);
                            other.DisconnectNeighbour("W");
                        }
                    }
                    else if (direction == "NW")
                    {
                        //NW's north neighbour
                        if (i == 0)
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "SE")
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[0].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[0].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[8].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[6].DisconnectLanes();
                            }
                            other.ChangeSpawnability("S", true);
                            other.DisconnectNeighbour("S");
                        }
                        //NW's west neighbour
                        else
                        {
                            if (other is Curve)
                            {
                                if (other.GetDirection() == "NE")
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                                else
                                {
                                    other.getListOfLane()[1].DisconnectLanes();
                                }
                            }
                            else if (other is Straight)
                            {
                                other.getListOfLane()[1].DisconnectLanes();

                            }
                            else if (other is CCrossroad)
                            {
                                other.getListOfLane()[5].DisconnectLanes();
                            }
                            else
                            {
                                other.getListOfLane()[4].DisconnectLanes();
                            }
                            other.ChangeSpawnability("E", true);
                            other.DisconnectNeighbour("E");
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
                    {
                        if (direction == "NW")
                        {
                            this.lanes[0].Spawnable = spawnAble;
                        }
                        else if (direction == "NE")
                            this.lanes[1].Spawnable = spawnAble;

                    }
                    break;
                case "E":
                    {
                        if (direction == "NE")
                        {
                            this.lanes[0].Spawnable = spawnAble;
                        }
                        else if (direction == "SE")
                            this.lanes[0].Spawnable = spawnAble;
                    }
                    break;
                case "S":
                    {
                        if (direction == "SE")
                        {
                            this.lanes[1].Spawnable = spawnAble;
                        }
                        else if (direction == "SW")
                            this.lanes[1].Spawnable = spawnAble;
                    }
                    break;
                case "W":
                    {
                        if (direction == "SW")
                        {
                            this.lanes[0].Spawnable = spawnAble;
                        }
                        else if (direction == "NW")
                            this.lanes[1].Spawnable = spawnAble;
                    }
                    break;

            }
        }

        public override void Draw(ref Graphics gr)
        {
            gr.DrawImage(image, Coordinates.X, Coordinates.Y, 150, 150);
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

