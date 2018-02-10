using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace tracy
{
    public class CCrossroad : Road
    {
        /// <summary>
        /// Constructor of CCrossroad
        /// </summary>
        /// <param name="Coord">Coordinates of the road</param>
        public CCrossroad(Point Coord)
        {
            //assign direction, coordinates and images
            //creates list of lane and neighbours
            //Make all spawnable lane spawnable
            image = Resource1.CrossC;
            this.lanes = new List<Lane>();
            this.disableTimer();

            this.coordinates = Coord;
            //0 to 5
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 54, coordinates.Y + 4), new Point(coordinates.X + 54, coordinates.Y + 30), "CrossC", 0));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 58, coordinates.Y + 4), new Point(coordinates.X + 64, coordinates.Y + 30), "CrossC", 1));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 85, coordinates.Y + 48), new Point(coordinates.X + 85, coordinates.Y + 4), "CrossC", 2));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 137, coordinates.Y + 58), new Point(coordinates.X + 100, coordinates.Y + 58), "CrossC", 3));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 137, coordinates.Y + 64), new Point(coordinates.X + 100, coordinates.Y + 63), "CrossC", 4));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 100, coordinates.Y + 86), new Point(coordinates.X + 147, coordinates.Y + 86), "CrossC", 5));

            // 6 to 11
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 85, coordinates.Y + 141), new Point(coordinates.X + 85, coordinates.Y + 100), "CrossC", 6));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 85, coordinates.Y + 141), new Point(coordinates.X + 75, coordinates.Y + 100), "CrossC", 7));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 56, coordinates.Y + 100), new Point(coordinates.X + 56, coordinates.Y + 143), "CrossC", 8));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 1, coordinates.Y + 85), new Point(coordinates.X + 50, coordinates.Y + 85), "CrossC", 9));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 1, coordinates.Y + 83), new Point(coordinates.X + 50, coordinates.Y + 70), "CrossC", 10));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 50, coordinates.Y + 54), new Point(coordinates.X + 1, coordinates.Y + 61), "CrossC", 11));

            // 12 to 17
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 56, coordinates.Y + 40), new Point(coordinates.X + 50, coordinates.Y + 60), "CrossC", 12));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 64, coordinates.Y + 40), new Point(coordinates.X + 56, coordinates.Y + 100), "CrossC", 13));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 74, coordinates.Y + 47), new Point(coordinates.X + 100, coordinates.Y + 86), "CrossC", 14));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 100, coordinates.Y + 58), new Point(coordinates.X + 89, coordinates.Y + 48), "CrossC", 15));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 100, coordinates.Y + 65), new Point(coordinates.X + 50, coordinates.Y + 60), "CrossC", 16));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 100, coordinates.Y + 63), new Point(coordinates.X + 56, coordinates.Y + 100), "CrossC", 17));

            // 18 to 23
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 88, coordinates.Y + 95), new Point(coordinates.X + 100, coordinates.Y + 88), "CrossC", 18));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 72, coordinates.Y + 100), new Point(coordinates.X + 88, coordinates.Y + 48), "CrossC", 19));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 72, coordinates.Y + 100), new Point(coordinates.X + 50, coordinates.Y + 58), "CrossC", 20));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 50, coordinates.Y + 88), new Point(coordinates.X + 56, coordinates.Y + 100), "CrossC", 21));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 40, coordinates.Y + 70), new Point(coordinates.X + 100, coordinates.Y + 88), "CrossC", 22));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 40, coordinates.Y + 70), new Point(coordinates.X + 88, coordinates.Y + 48), "CrossC", 23));



            this.lanes[0].TrafficLight.position = new Point(coordinates.X + 25, coordinates.Y + 27);
            this.lanes[0].TrafficLight.CPost = coordinates;
            this.lanes[1].TrafficLight.position = new Point(coordinates.X + 35, coordinates.Y + 27);
            this.lanes[1].TrafficLight.CPost = coordinates;
            this.lanes[3].TrafficLight.position = new Point(coordinates.X + 100, coordinates.Y + 30);
            this.lanes[3].TrafficLight.CPost = coordinates;
            this.lanes[4].TrafficLight.position = new Point(coordinates.X + 100, coordinates.Y + 40);
            this.lanes[4].TrafficLight.CPost = coordinates;

            this.lanes[6].TrafficLight.position = new Point(coordinates.X + 110, coordinates.Y + 100);
            this.lanes[6].TrafficLight.CPost = coordinates;
            this.lanes[7].TrafficLight.position = new Point(coordinates.X + 100, coordinates.Y + 100);
            this.lanes[7].TrafficLight.CPost = coordinates;

            this.lanes[9].TrafficLight.position = new Point(coordinates.X + 26, coordinates.Y + 110);
            this.lanes[9].TrafficLight.CPost = coordinates;
            this.lanes[10].TrafficLight.position = new Point(coordinates.X + 26, coordinates.Y + 100);
            this.lanes[10].TrafficLight.CPost = coordinates;

            this.changeSpawnability("N", true);
            this.changeSpawnability("E", true);
            this.changeSpawnability("S", true);
            this.changeSpawnability("W", true);


            //Set the intial sequence of the 8 traffic light
            Random rand = new Random();
            int r = rand.Next(0, 4);
            if (r == 0)
            {
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[1].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Timer = 10;
                this.lanes[4].TrafficLight.Timer = 20;
                this.lanes[6].TrafficLight.Timer = 10;
                this.lanes[7].TrafficLight.Timer = 10;

            }
            else if (r == 1)
            {
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[4].TrafficLight.Colour = 3;
                this.lanes[6].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Timer = 10;
                this.lanes[7].TrafficLight.Timer = 20;
                this.lanes[9].TrafficLight.Timer = 10;
                this.lanes[10].TrafficLight.Timer = 10;
            }
            else if (r == 2)
            {
                this.lanes[6].TrafficLight.Colour = 3;
                this.lanes[7].TrafficLight.Colour = 3;
                this.lanes[9].TrafficLight.Colour = 3;
                this.lanes[6].TrafficLight.Timer = 10;
                this.lanes[10].TrafficLight.Timer = 20;
                this.lanes[0].TrafficLight.Timer = 10;
                this.lanes[1].TrafficLight.Timer = 10;
            }
            else
            {
                this.lanes[9].TrafficLight.Colour = 3;
                this.lanes[10].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[9].TrafficLight.Timer = 10;
                this.lanes[1].TrafficLight.Timer = 20;
                this.lanes[3].TrafficLight.Timer = 10;
                this.lanes[4].TrafficLight.Timer = 10;
            }


            this.lanes[0].TrafficLight.greenInterval = 20;
            this.lanes[1].TrafficLight.greenInterval = 10;
            this.lanes[3].TrafficLight.greenInterval = 20;
            this.lanes[4].TrafficLight.greenInterval = 10;
            this.lanes[6].TrafficLight.greenInterval = 20;
            this.lanes[7].TrafficLight.greenInterval = 10;
            this.lanes[9].TrafficLight.greenInterval = 20;
            this.lanes[10].TrafficLight.greenInterval = 10;

            this.lanes[0].TrafficLight.redInterval = 20;
            this.lanes[1].TrafficLight.redInterval = 30;
            this.lanes[3].TrafficLight.redInterval = 20;
            this.lanes[4].TrafficLight.redInterval = 30;
            this.lanes[6].TrafficLight.redInterval = 20;
            this.lanes[7].TrafficLight.redInterval = 30;
            this.lanes[9].TrafficLight.redInterval = 20;
            this.lanes[10].TrafficLight.redInterval = 30;


            //Connect all inside lane here  
            this.lanes[0].ConnectToLane(this.lanes[12]);
            this.lanes[12].ConnectToLane(this.lanes[11]);
                this.lanes[1].ConnectToLane(this.lanes[13]);
                this.lanes[13].ConnectToLane(this.lanes[8]);
            this.lanes[1].ConnectToLane(this.lanes[14]);
            this.lanes[14].ConnectToLane(this.lanes[5]);
                this.lanes[3].ConnectToLane(this.lanes[15]);
                this.lanes[15].ConnectToLane(this.lanes[2]);
            this.lanes[4].ConnectToLane(this.lanes[16]);
            this.lanes[16].ConnectToLane(this.lanes[11]);
              this.lanes[4].ConnectToLane(this.lanes[17]);
            this.lanes[17].ConnectToLane(this.lanes[8]);
            this.lanes[6].ConnectToLane(this.lanes[18]);
            this.lanes[18].ConnectToLane(this.lanes[5]);
            this.lanes[7].ConnectToLane(this.lanes[19]);
            this.lanes[19].ConnectToLane(this.lanes[2]);
            this.lanes[7].ConnectToLane(this.lanes[20]);
            this.lanes[20].ConnectToLane(this.lanes[11]);
            this.lanes[9].ConnectToLane(this.lanes[21]);
            this.lanes[21].ConnectToLane(this.lanes[8]);
            this.lanes[10].ConnectToLane(this.lanes[22]);
            this.lanes[22].ConnectToLane(this.lanes[5]);
            this.lanes[10].ConnectToLane(this.lanes[23]);
            this.lanes[23].ConnectToLane(this.lanes[2]);

            nrOfNeighbours = 0;
            connectedRoads = new Road[4];
            
            this.TrafTime.Interval = 1000;
            this.TrafTime.Tick += new EventHandler(incTimer);
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
                        if (((other is Curve) && ((other.getDirection() == "SW") || (other.getDirection() == "SE")))
                            || ((other is Straight) && other.getDirection() == "V")
                            || (other is CCrossroad)
                            || (other is PCrossroad))
                        {
                            connectedRoads[0] = other;
                            this.nrOfNeighbours++;
                            return true;
                        }
                        else
                        {
                            this.nrOfNeighbours++;
                            return false;
                        }
                    }

                case "E":
                    {
                        if (((other is Curve) && ((other.getDirection() == "SW") || (other.getDirection() == "NW")))
                            || ((other is Straight) && other.getDirection() == "H")
                            || (other is CCrossroad)
                            || (other is PCrossroad))
                        {
                            connectedRoads[1] = other;
                            this.nrOfNeighbours++;
                            return true;
                        }
                        else
                        {
                            this.nrOfNeighbours++;
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
                            connectedRoads[2] = other;
                            this.nrOfNeighbours++;
                            return true;
                        }
                        else
                        {
                            this.nrOfNeighbours++;
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
                            connectedRoads[3] = other;
                            this.nrOfNeighbours++;
                            return true;
                        }
                        else
                        {
                            this.nrOfNeighbours++;
                            return false;
                        }
                    }

                default:
                    {
                        return false;              
                    }
            }
        }


        public override string getDirection()
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
                            //done
                            //not tested
                            this.lanes[2].ConnectToLane(other.getListOfLane()[7]);
                            this.lanes[2].ConnectToLane(other.getListOfLane()[6]);
                            other.getListOfLane()[8].ConnectToLane(this.lanes[0]);
                            other.getListOfLane()[8].ConnectToLane(this.lanes[1]);
                        }
                        else if (other is PCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[2].ConnectToLane(other.getListOfLane()[5]);        
                            other.getListOfLane()[6].ConnectToLane(this.lanes[0]);
                            other.getListOfLane()[6].ConnectToLane(this.lanes[1]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "SW")
                            {
                                this.lanes[2].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                            }
                            else //SE
                            {
                                this.lanes[2].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[2].ConnectToLane(other.getListOfLane()[1]);
                            other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                            other.getListOfLane()[0].ConnectToLane(this.lanes[1]);
                        }
                    }
                    break;
                case "E":
                    {
                        if (other is CCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[5].ConnectToLane(other.getListOfLane()[9]);
                            this.lanes[5].ConnectToLane(other.getListOfLane()[10]);
                            other.getListOfLane()[11].ConnectToLane(this.lanes[3]);
                            other.getListOfLane()[11].ConnectToLane(this.lanes[4]);
                        }
                        else if (other is PCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[5].ConnectToLane(other.getListOfLane()[7]);
                            this.lanes[5].ConnectToLane(other.getListOfLane()[8]);
                            other.getListOfLane()[9].ConnectToLane(this.lanes[3]);
                            other.getListOfLane()[9].ConnectToLane(this.lanes[4]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "SW")
                            {
                                //done
                                //not tested
                                this.lanes[5].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[3]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[4]);
                            }
                            else //NW
                            {
                                //done
                                //not tested
                                this.lanes[5].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[4]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[5].ConnectToLane(other.getListOfLane()[1]);
                            other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
                            other.getListOfLane()[0].ConnectToLane(this.lanes[4]);
                        }
                    }
                    break;
                case "S":
                    {
                        if (other is CCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[8].ConnectToLane(other.getListOfLane()[0]);
                            this.lanes[8].ConnectToLane(other.getListOfLane()[1]);
                            other.getListOfLane()[2].ConnectToLane(this.lanes[6]);
                            other.getListOfLane()[2].ConnectToLane(this.lanes[7]);
                        }
                        else if (other is PCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[8].ConnectToLane(other.getListOfLane()[0]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "NW")
                            {
                                //done
                                //not tested
                                this.lanes[8].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                            }
                            else //NE
                            {

                                //done
                                //not tested
                                this.lanes[8].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[6]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[7]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[8].ConnectToLane(other.getListOfLane()[0]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[6]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                        }
                    }
                    break;
                case "W":
                    {
                        if (other is CCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[11].ConnectToLane(other.getListOfLane()[3]);
                            this.lanes[11].ConnectToLane(other.getListOfLane()[4]);
                            other.getListOfLane()[5].ConnectToLane(this.lanes[10]);
                            other.getListOfLane()[5].ConnectToLane(this.lanes[9]);
                        }
                        else if (other is PCrossroad)
                        { 
                            //done
                            //not tested
                            this.lanes[11].ConnectToLane(other.getListOfLane()[2]);
                            this.lanes[11].ConnectToLane(other.getListOfLane()[3]);
                            other.getListOfLane()[4].ConnectToLane(this.lanes[10]);
                            other.getListOfLane()[4].ConnectToLane(this.lanes[9]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "NE")
                            {
                                //done
                                //not tested
                                this.lanes[11].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[9]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[10]);
                            }
                            else //SE
                            {
                                //done
                                //not tested
                                this.lanes[11].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[9]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[10]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[11].ConnectToLane(other.getListOfLane()[0]);
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
            List<Lane> temp = new List<Lane>();
            temp.Add(this.lanes[0]);
            temp.Add(this.lanes[1]);
            temp.Add(this.lanes[3]);
            temp.Add(this.lanes[4]);
            temp.Add(this.lanes[6]);
            temp.Add(this.lanes[7]);
            temp.Add(this.lanes[9]);
            temp.Add(this.lanes[10]);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].SpawnTime = time;
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
            this.nrOfNeighbours--;
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
                    //north neighbour
                    if (i == 0)
                    {
                        if (connectedRoads[i] is Curve)
                        {
                            if (connectedRoads[i].getDirection() == "SW")
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
                        connectedRoads[i].changeSpawnability("S", true);
                    }
                    //east neighbour
                    else if (i == 1)
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
                    //south neighbour
                    else if (i == 2)
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
                    //west neighbour
                    else
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
                        {
                            connectedRoads[i].getListOfLane()[4].DisconnectLanes();
                        }
                        connectedRoads[i].DisconnectNeighbour("E");
                        connectedRoads[i].changeSpawnability("E", true);
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
                    {
                        this.lanes[0].SpawnAble = spawnAble;
                        this.lanes[1].SpawnAble = spawnAble;
                    }
                    break;
                case "E":
                    {
                        this.lanes[3].SpawnAble = spawnAble;
                        this.lanes[4].SpawnAble = spawnAble;
                    }
                    break;
                case "S":
                    {
                        this.lanes[6].SpawnAble = spawnAble;
                        this.lanes[7].SpawnAble = spawnAble;
                    }
                    break;
                case "W":
                    {
                        this.lanes[9].SpawnAble = spawnAble;
                        this.lanes[10].SpawnAble = spawnAble;
                    }
                    break;

            }
        }

        public override void Draw(ref Graphics g)
        {
            g.DrawImage(image, coordinates.X, coordinates.Y, 150, 150);
            int[] id = { 0, 1, 3, 4, 6, 7, 9, 10 };
            for (int i = 0; i < id.Length; i++)
            {
                this.lanes[id[i]].TrafficLight.Draw(ref g);
            }
        }

        public override void DrawTrafficLight(ref Graphics g)
        {
            int[] id = { 0, 1, 3, 4, 6, 7, 9, 10 };
            for (int i = 0; i < id.Length; i++)
            {
                if (this.lanes[id[i]].TrafficLight.ColorChanged)
                {
                    this.lanes[id[i]].TrafficLight.Draw(ref g);
                    this.lanes[id[i]].TrafficLight.ColorChanged = false;
                }
            }
        }

        public void incTimer(object sender, EventArgs e)
        {
            this.lanes[0].TrafficLight.IncTimer();
            this.lanes[1].TrafficLight.IncTimer();
            this.lanes[3].TrafficLight.IncTimer();
            this.lanes[4].TrafficLight.IncTimer();
            this.lanes[6].TrafficLight.IncTimer();
            this.lanes[7].TrafficLight.IncTimer();
            this.lanes[9].TrafficLight.IncTimer();
            this.lanes[10].TrafficLight.IncTimer();
        }

        public void adjustGreenTime(int Green)
        {
            this.lanes[0].TrafficLight.AdjustGreenTime(Green * 2);
            this.lanes[1].TrafficLight.AdjustGreenTime(Green);
            this.lanes[3].TrafficLight.AdjustGreenTime(Green * 2);
            this.lanes[4].TrafficLight.AdjustGreenTime(Green);
            this.lanes[6].TrafficLight.AdjustGreenTime(Green * 2);
            this.lanes[7].TrafficLight.AdjustGreenTime(Green);
            this.lanes[9].TrafficLight.AdjustGreenTime(Green * 2);
            this.lanes[10].TrafficLight.AdjustGreenTime(Green);
        }

        public override void RemoveLanes()
        {
            for (int i = 0; i < lanes.Count; i++)
            {
                this.lanes.RemoveAt(i);
            }

        }

        public void resetTrafficLight()
        {
            Random rand = new Random();
            int r = rand.Next(0, 4);
            if (r == 0)
            {
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[1].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.greenInterval / 2;
                this.lanes[4].TrafficLight.Timer = this.lanes[4].TrafficLight.greenInterval * 2;
                this.lanes[6].TrafficLight.Timer = this.lanes[6].TrafficLight.greenInterval / 2;
                this.lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.greenInterval;

                this.lanes[4].TrafficLight.Colour = 1;
                this.lanes[6].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[9].TrafficLight.Colour = 1;
                this.lanes[10].TrafficLight.Colour = 1;
                this.lanes[1].TrafficLight.Timer = 0;
                this.lanes[3].TrafficLight.Timer = 0;
                this.lanes[9].TrafficLight.Timer = 0;
                this.lanes[10].TrafficLight.Timer = 0;
            }
            else if (r == 1)
            {
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[4].TrafficLight.Colour = 3;
                this.lanes[6].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.greenInterval / 2;
                this.lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.greenInterval * 2;
                this.lanes[9].TrafficLight.Timer = this.lanes[9].TrafficLight.greenInterval / 2;
                this.lanes[10].TrafficLight.Timer = this.lanes[10].TrafficLight.greenInterval;

                this.lanes[0].TrafficLight.Colour = 1;
                this.lanes[1].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[9].TrafficLight.Colour = 1;
                this.lanes[10].TrafficLight.Colour = 1;
                this.lanes[0].TrafficLight.Timer = 0;
                this.lanes[1].TrafficLight.Timer = 0;
                this.lanes[4].TrafficLight.Timer = 0;
                this.lanes[6].TrafficLight.Timer = 0;
            }
            else if (r == 2)
            {
                this.lanes[6].TrafficLight.Colour = 3;
                this.lanes[7].TrafficLight.Colour = 3;
                this.lanes[9].TrafficLight.Colour = 3;
                this.lanes[6].TrafficLight.Timer = this.lanes[6].TrafficLight.greenInterval / 2;
                this.lanes[10].TrafficLight.Timer = this.lanes[10].TrafficLight.greenInterval * 2;
                this.lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.greenInterval / 2;
                this.lanes[1].TrafficLight.Timer = this.lanes[1].TrafficLight.greenInterval;

                this.lanes[0].TrafficLight.Colour = 1;
                this.lanes[1].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Colour = 1;
                this.lanes[4].TrafficLight.Colour = 1;
                this.lanes[10].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Timer = 0;
                this.lanes[4].TrafficLight.Timer = 0;
                this.lanes[7].TrafficLight.Timer = 0;
                this.lanes[9].TrafficLight.Timer = 0;
            }
            else
            {
                this.lanes[9].TrafficLight.Colour = 3;
                this.lanes[10].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[9].TrafficLight.Timer = this.lanes[9].TrafficLight.greenInterval / 2;
                this.lanes[1].TrafficLight.Timer = this.lanes[1].TrafficLight.greenInterval * 2;
                this.lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.greenInterval / 2;
                this.lanes[4].TrafficLight.Timer = this.lanes[4].TrafficLight.greenInterval;

                this.lanes[1].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Colour = 1;
                this.lanes[4].TrafficLight.Colour = 1;
                this.lanes[6].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[0].TrafficLight.Timer = 0;
                this.lanes[6].TrafficLight.Timer = 0;
                this.lanes[7].TrafficLight.Timer = 0;
                this.lanes[10].TrafficLight.Timer = 0;
            }

        }

       
    }
}
