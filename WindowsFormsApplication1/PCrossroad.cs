using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace tracy
{
    public class PCrossroad : Road
    {
        private readonly TrafficLightP[] trafPed = new TrafficLightP[4];
        private readonly System.EventHandler OnTimerTick;

        //For pedestrian stuff
        private System.EventHandler OnSensorPress;
        private System.EventHandler OnPedestrianStart;
        private readonly System.EventHandler[] OnPedestrianMove = new EventHandler[4];
        private readonly List<TrafficLightC> tempList = new List<TrafficLightC>();
        private readonly Timer pedTimer;
        private bool pressedSensor = false;
        private readonly Pedestrian[] ped;
        private int[] laneID;


        /// <summary>
        /// Constructor of PCrossroad
        /// </summary>
        /// <param name="Coord">Coordinates of the road</param>
        public PCrossroad(Point Coord)
        {
            //assign coordinates and images
            //creates list of lane and neighbours
            //Make all spawnable lane spawnable
            image = Resource1.CrossP;
            this.lanes = new List<Lane>();
            this.coordinates = Coord;



            //0 to 4
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 58, coordinates.Y), new Point(coordinates.X + 58, coordinates.Y + 20), "CrossP", 0));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 82, coordinates.Y + 38), new Point(coordinates.X + 82, coordinates.Y), "CrossP", 1));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 140, coordinates.Y + 52), new Point(coordinates.X + 98, coordinates.Y + 52), "CrossP", 2));
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 140, coordinates.Y + 52), new Point(coordinates.X + 98, coordinates.Y + 69), "CrossP", 3));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 98, coordinates.Y + 85), new Point(coordinates.X + 141, coordinates.Y + 85), "CrossP", 4));

            //5 to 9
            lanes.Add(new Lane(time, true, new Point(coordinates.X + 80, coordinates.Y + 140), new Point(coordinates.X + 80, coordinates.Y + 120), "CrossP", 5));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 58, coordinates.Y + 100), new Point(coordinates.X + 58, coordinates.Y + 140), "CrossP", 6));
            lanes.Add(new Lane(time, true, new Point(coordinates.X, coordinates.Y + 85), new Point(coordinates.X + 42, coordinates.Y + 85), "CrossP", 7));
            lanes.Add(new Lane(time, true, new Point(coordinates.X, coordinates.Y + 85), new Point(coordinates.X + 42, coordinates.Y + 70), "CrossP", 8));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 42, coordinates.Y + 52), new Point(coordinates.X, coordinates.Y + 52), "CrossP", 9));

            //10 to 15
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 58, coordinates.Y + 20), new Point(coordinates.X + 42, coordinates.Y + 52), "CrossP", 10));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 58, coordinates.Y + 20), new Point(coordinates.X + 58, coordinates.Y + 100), "CrossP", 11));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 58, coordinates.Y + 20), new Point(coordinates.X + 98, coordinates.Y + 85), "CrossP", 12));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 98, coordinates.Y + 52), new Point(coordinates.X + 82, coordinates.Y + 48), "CrossP", 13));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 98, coordinates.Y + 52), new Point(coordinates.X + 42, coordinates.Y + 52), "CrossP", 14));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 98, coordinates.Y + 71), new Point(coordinates.X + 58, coordinates.Y + 100), "CrossP", 15));

            //16 ro 21
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 80, coordinates.Y + 120), new Point(coordinates.X + 98, coordinates.Y + 85), "CrossP", 16));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 80, coordinates.Y + 120), new Point(coordinates.X + 82, coordinates.Y + 38), "CrossP", 17));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 80, coordinates.Y + 120), new Point(coordinates.X + 42, coordinates.Y + 52), "CrossP", 18));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 42, coordinates.Y + 85), new Point(coordinates.X + 58, coordinates.Y + 100), "CrossP", 19));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 42, coordinates.Y + 85), new Point(coordinates.X + 98, coordinates.Y + 85), "CrossP", 20));
            lanes.Add(new Lane(time, false, new Point(coordinates.X + 42, coordinates.Y + 70), new Point(coordinates.X + 58, coordinates.Y + 48), "CrossP", 21));

            this.changeSpawnability("N", true);
            this.changeSpawnability("E", true);
            this.changeSpawnability("S", true);
            this.changeSpawnability("W", true);


            Random rand = new Random();
            int r = rand.Next(0, 4);
            if (r == 0)
            {
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[2].TrafficLight.Timer = 20;
                this.lanes[3].TrafficLight.Timer = 20;
                this.lanes[5].TrafficLight.Timer = 10;
            }
            else if (r == 1)
            {
                this.lanes[2].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[5].TrafficLight.Timer = 20;
                this.lanes[7].TrafficLight.Timer = 10;
                this.lanes[8].TrafficLight.Timer = 10;
            }
            else if (r == 2)
            {
                this.lanes[5].TrafficLight.Colour = 3;
                this.lanes[6].TrafficLight.Timer = 20;
                this.lanes[7].TrafficLight.Timer = 20;
                this.lanes[0].TrafficLight.Timer = 10;
            }
            else
            {
                this.lanes[7].TrafficLight.Colour = 3;
                this.lanes[8].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Timer = 20;
                this.lanes[2].TrafficLight.Timer = 10;
                this.lanes[3].TrafficLight.Timer = 10;

            }

            this.lanes[0].TrafficLight.greenInterval = 10;
            this.lanes[2].TrafficLight.greenInterval = 10;
            this.lanes[3].TrafficLight.greenInterval = 10;
            this.lanes[5].TrafficLight.greenInterval = 10;
            this.lanes[7].TrafficLight.greenInterval = 10;
            this.lanes[8].TrafficLight.greenInterval = 10;

            this.lanes[0].TrafficLight.redInterval = 30;
            this.lanes[2].TrafficLight.redInterval = 30;
            this.lanes[3].TrafficLight.redInterval = 30;
            this.lanes[5].TrafficLight.redInterval = 30;
            this.lanes[7].TrafficLight.redInterval = 30;
            this.lanes[8].TrafficLight.redInterval = 30;

            this.lanes[0].ConnectToLane(this.lanes[10]);
            this.lanes[10].ConnectToLane(this.lanes[9]);
                this.lanes[0].ConnectToLane(this.lanes[11]);
                this.lanes[11].ConnectToLane(this.lanes[6]);
            this.lanes[0].ConnectToLane(this.lanes[12]);
            this.lanes[12].ConnectToLane(this.lanes[4]);
                this.lanes[2].ConnectToLane(this.lanes[13]);
                this.lanes[13].ConnectToLane(this.lanes[1]);
            this.lanes[2].ConnectToLane(this.lanes[14]);
            this.lanes[14].ConnectToLane(this.lanes[9]);
               this.lanes[3].ConnectToLane(this.lanes[15]);
               this.lanes[15].ConnectToLane(this.lanes[6]);
            this.lanes[5].ConnectToLane(this.lanes[16]);
            this.lanes[16].ConnectToLane(this.lanes[4]);
                this.lanes[5].ConnectToLane(this.lanes[17]);
                this.lanes[17].ConnectToLane(this.lanes[1]);
            this.lanes[5].ConnectToLane(this.lanes[18]);
            this.lanes[18].ConnectToLane(this.lanes[9]);
                this.lanes[7].ConnectToLane(this.lanes[19]);
                this.lanes[19].ConnectToLane(this.lanes[6]);
            this.lanes[7].ConnectToLane(this.lanes[20]);
            this.lanes[20].ConnectToLane(this.lanes[4]);
                this.lanes[8].ConnectToLane(this.lanes[21]);
                this.lanes[21].ConnectToLane(this.lanes[1]);

            nrOfNeighbours = 0;

            connectedRoads = new Road[4];

            this.lanes[0].TrafficLight.position = new Point(coordinates.X + 27, coordinates.Y + 4);
            this.lanes[0].TrafficLight.CPost = coordinates;

            this.lanes[2].TrafficLight.position = new Point(coordinates.X + 110, coordinates.Y + 28);
            this.lanes[2].TrafficLight.CPost = coordinates;
            this.lanes[3].TrafficLight.position = new Point(coordinates.X + 110, coordinates.Y + 38);
            this.lanes[3].TrafficLight.CPost = coordinates;

            this.lanes[5].TrafficLight.position = new Point(coordinates.X + 110, coordinates.Y + 120);
            this.lanes[5].TrafficLight.CPost = coordinates;

            this.lanes[7].TrafficLight.position = new Point(coordinates.X + 20, coordinates.Y + 113);
            this.lanes[7].TrafficLight.CPost = coordinates;
            this.lanes[8].TrafficLight.position = new Point(coordinates.X + 20, coordinates.Y + 103);
            this.lanes[8].TrafficLight.CPost = coordinates;

            for (int i = 0; i < 4; i++)
            {
                trafPed[i] = new TrafficLightP(10, new Point(1, 1));
            }

            trafPed[0].position = new Point(coordinates.X + 20, coordinates.Y + 33);
            trafPed[0].CPost = coordinates;
            trafPed[1].position = new Point(coordinates.X + 110, coordinates.Y + 14);
            trafPed[1].CPost = coordinates;
            trafPed[2].position = new Point(coordinates.X + 110, coordinates.Y + 105);
            trafPed[2].CPost = coordinates;
            trafPed[3].position = new Point(coordinates.X + 20, coordinates.Y + 123);
            trafPed[3].CPost = coordinates;

            pedTimer = new Timer();
            pedTimer.Enabled = false;
            
            this.TrafTime.Interval = 1000;
            pedTimer.Interval = 1000;
            OnTimerTick = new EventHandler(incTimer);
            this.TrafTime.Tick += OnTimerTick;

            ped = new Pedestrian[4];
            for (int i = 0; i < 4; i++)
            {
                ped[i] = new Pedestrian();
                ped[i].Cpos = coordinates;
            }
            ped[0].pos = new Point(Coord.X + 42, Coord.Y + 35);
            ped[1].pos = new Point(Coord.X + 102, Coord.Y + 35);
            ped[2].pos = new Point(Coord.X + 102, Coord.Y + 110);
            ped[3].pos = new Point(Coord.X + 42, Coord.Y + 110);
            this.disableTimer();
        }


        public void incTimer(object sender, EventArgs e)
        {
            this.lanes[0].TrafficLight.IncTimer();
            this.lanes[2].TrafficLight.IncTimer();
            this.lanes[3].TrafficLight.IncTimer();
            this.lanes[5].TrafficLight.IncTimer();
            this.lanes[7].TrafficLight.IncTimer();
            this.lanes[8].TrafficLight.IncTimer();
        }

        public void PressSensor()
        {
            pressedSensor = true;
            //Check the green light, timer 0 or not, pressed or not  
            OnSensorPress = new EventHandler(startPedestrian);
            TrafTime.Tick += OnSensorPress;
        }

        public void stopPedestrian(object sender, EventArgs e)
        {
            if ((ped[0].fin == true) || (ped[1].fin == true) || (ped[2].fin == true) || (ped[3].fin == true))
            {
                pedTimer.Enabled = false;
                pedTimer.Stop();
                TrafTime.Tick += OnTimerTick;
                TrafTime.Tick -= OnSensorPress;
                pressedSensor = false;
                pedTimer.Tick -= OnPedestrianStart;
                ped[0].fin = false;
                ped[1].fin = false;
                ped[2].fin = false;
                ped[3].fin = false;


                Tracy f1 = (Tracy)Tracy.ActiveForm;
                f1.pedestrianDone();

                if (laneID[0] == 0)
                {
                    this.lanes[0].TrafficLight = tempList[0];
                }
                else if (laneID[0] == 1)
                {
                    this.lanes[2].TrafficLight = tempList[0];
                    this.lanes[3].TrafficLight = tempList[1];
                }
                else if (laneID[0] == 3)
                {
                    this.lanes[5].TrafficLight = tempList[0];
                }
                else if (laneID[0] == 4)
                {
                    this.lanes[7].TrafficLight = tempList[0];
                    this.lanes[8].TrafficLight = tempList[1];
                }
                tempList.Clear();

                for (int j = 0; j < 4; j++)
                {
                    pedTimer.Tick -= OnPedestrianMove[j];
                }

                for (int i = 0; i < trafPed.Count(); i++)
                {
                    trafPed[i].Colour = 1;
                    trafPed[i].ColorChanged = true;
                }

            }

            TrafTime.Start();
        }

        public void startPedestrian(object sender, EventArgs e)
        {
            List<Lane> TEMP = new List<Lane>();
            TEMP.Add(this.lanes[0]);
            TEMP.Add(this.lanes[2]);
            TEMP.Add(this.lanes[3]);
            TEMP.Add(this.lanes[5]);
            TEMP.Add(this.lanes[7]);
            TEMP.Add(this.lanes[8]);
            if (pressedSensor)
            {
                for (int i = 0; i < 6; i++)
                {
                    if ((TEMP[i].TrafficLight.Colour == 2 && TEMP[i].TrafficLight.Timer == 9))
                    {
                        TrafTime.Tick -= OnTimerTick;
                        TrafTime.Enabled = false;
                        TrafTime.Stop();
                        if ((i == 0) || (i == 3))
                        {
                            tempList.Add(new TrafficLightC(10, new Point(1, 1)));
                            tempList.Add(new TrafficLightC(10, new Point(1, 1)));
                            laneID = new int[2];
                            laneID[0] = i + 1;
                            laneID[1] = i + 2;
                            for (int k = 0; k < 2; k++)
                            {
                                TrafficLightC tp = new TrafficLightC(10, new Point(1, 1));
                                tp.Colour = 3;
                                tp.position = TEMP[i + k + 1].TrafficLight.position;
                                tp.CPost = TEMP[i+k+1].TrafficLight.CPost;
                                tp.greenInterval = TEMP[i + k + 1].TrafficLight.greenInterval;
                                tp.redInterval = TEMP[i + k + 1].TrafficLight.redInterval;
                                tp.Timer = 0;
                                tp.ColorChanged = true;
                                tempList[k] = tp;
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
                            laneID = new int[1];
                            laneID[0] = (i + 2) % 6;

                            TrafficLightC tp = new TrafficLightC(10, new Point(1, 1));
                            tp.Colour = 3;
                            tp.position = TEMP[laneID[0]].TrafficLight.position;
                            tp.CPost = TEMP[laneID[0]].TrafficLight.CPost;
                            tp.greenInterval = TEMP[laneID[0]].TrafficLight.greenInterval;
                            tp.redInterval = TEMP[laneID[0]].TrafficLight.redInterval;
                            tp.Timer = 0;
                            tp.ColorChanged = true;
                            tempList.Add(tp);
                            TEMP[laneID[0]].TrafficLight.Colour = 1;
                            TEMP[laneID[0]].TrafficLight.Timer = 0;
                            TEMP[laneID[0]].TrafficLight.ColorChanged = true;
                            TEMP[i].TrafficLight.ColorChanged = true;
                            TEMP[i].TrafficLight.Colour = 1;
                            TEMP[i+1].TrafficLight.Colour = 1;
                            TEMP[i + 1].TrafficLight.ColorChanged = true;
                            TEMP[i].TrafficLight.Timer = 0;
                            TEMP[i+1].TrafficLight.Timer = 0;
                            TEMP[(i + 3) % 6].TrafficLight.Timer += 1;
                            TEMP[(i + 4) % 6].TrafficLight.Timer += 1;
                            TEMP[(i + 5) % 6].TrafficLight.Timer += 1;
                        }
                        else if ((i == 2) || (i == 5))
                        {
                            laneID = new int[1];
                            laneID[0] = (i + 1) % 6;

                            TrafficLightC tp = new TrafficLightC(10, new Point(1, 1));
                            tp.Colour = 3;
                            tp.position = TEMP[laneID[0]].TrafficLight.position;
                            tp.CPost = TEMP[laneID[0]].TrafficLight.CPost;
                            tp.greenInterval = TEMP[laneID[0]].TrafficLight.greenInterval;
                            tp.redInterval = TEMP[laneID[0]].TrafficLight.redInterval;
                            tp.Timer = 0;
                            tp.ColorChanged = true;
                            tempList.Add(tp);
                            TEMP[laneID[0]].TrafficLight.Colour = 1;
                            TEMP[laneID[0]].TrafficLight.Timer = 0;
                            TEMP[laneID[0]].TrafficLight.ColorChanged = true;

                            TEMP[i].TrafficLight.ColorChanged = true;
                            TEMP[i -1].TrafficLight.ColorChanged = true;
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
                            OnPedestrianMove[j] = new EventHandler(ped[j].Move);
                            pedTimer.Tick += OnPedestrianMove[j];
                        }

                        for (int c = 0; c < trafPed.Count(); c++)
                        {
                            trafPed[c].Colour = 3;
                            trafPed[c].ColorChanged = true;
                        }

                        OnPedestrianStart = new EventHandler(stopPedestrian);
                        pedTimer.Enabled = true;
                        pedTimer.Tick += OnPedestrianStart;

                        pedTimer.Start(); 
                        pressedSensor = false;
                        break;
                    }
                }
            }
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
                            return true;
                        }
                        else
                        {
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
                            connectedRoads[2] = other;
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
                            connectedRoads[3] = other;
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
                            this.lanes[1].ConnectToLane(other.getListOfLane()[7]);
                            this.lanes[1].ConnectToLane(other.getListOfLane()[6]);
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
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                            }
                            else //SE
                            {
                                //done
                                //not tested
                                this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[1].ConnectToLane(other.getListOfLane()[1]);
                            other.getListOfLane()[0].ConnectToLane(this.lanes[0]);
                        }
                    }
                    break;
                case "E":
                    {
                        if (other is CCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[4].ConnectToLane(other.getListOfLane()[9]);
                            this.lanes[4].ConnectToLane(other.getListOfLane()[10]);
                            other.getListOfLane()[11].ConnectToLane(this.lanes[2]); 
                            other.getListOfLane()[11].ConnectToLane(this.lanes[3]);
                        }
                        else if (other is PCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[4].ConnectToLane(other.getListOfLane()[7]);
                            this.lanes[4].ConnectToLane(other.getListOfLane()[8]);
                            other.getListOfLane()[9].ConnectToLane(this.lanes[2]);
                            other.getListOfLane()[9].ConnectToLane(this.lanes[3]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "SW")
                            {
                                //done
                                //not tested
                                this.lanes[4].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[2]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[3]);
                            }
                            else //NW
                            {
                                //done
                                //not tested
                                this.lanes[4].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[2]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[3]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
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
                            //done
                            //not tested
                            this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
                            this.lanes[6].ConnectToLane(other.getListOfLane()[1]);
                            other.getListOfLane()[2].ConnectToLane(this.lanes[5]);
                        }
                        else if (other is PCrossroad)
                        {

                            //done
                            //not tested
                            this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "NW")
                            {
                                //done
                                //not tested
                                this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
                            }
                            else //NE
                            {
                                //done
                                //not tested
                                this.lanes[6].ConnectToLane(other.getListOfLane()[1]);
                                other.getListOfLane()[0].ConnectToLane(this.lanes[5]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[6].ConnectToLane(other.getListOfLane()[0]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[5]);
                        }
                    }
                    break;
                case "W":
                    {
                        if (other is CCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[9].ConnectToLane(other.getListOfLane()[3]);
                            this.lanes[9].ConnectToLane(other.getListOfLane()[4]);
                            other.getListOfLane()[5].ConnectToLane(this.lanes[7]);
                            other.getListOfLane()[5].ConnectToLane(this.lanes[8]);
                        }
                        else if (other is PCrossroad)
                        {
                            //done
                            //not tested
                            this.lanes[9].ConnectToLane(other.getListOfLane()[2]);
                            this.lanes[9].ConnectToLane(other.getListOfLane()[3]);
                            other.getListOfLane()[4].ConnectToLane(this.lanes[7]);
                            other.getListOfLane()[4].ConnectToLane(this.lanes[8]);
                        }
                        else if (other is Curve)
                        {
                            if (other.getDirection() == "SE")
                            {
                                //done
                                //not tested
                                this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
                            }
                            else //NE
                            {
                                //done
                                //not tested
                                this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                                other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
                            }
                        }
                        else
                        {
                            //done
                            //not tested
                            this.lanes[9].ConnectToLane(other.getListOfLane()[0]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[7]);
                            other.getListOfLane()[1].ConnectToLane(this.lanes[8]);
                        }
                    }
                    break;
                default:
                    {
                        return;
                    }
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

            for (int i = 0; i < temp.Count(); i++)
            {
                temp[i].SpawnTime = time;
            }
        }


        public override void DisconnectRoad()
        {
            time.Tick -= OnTimerTick;
            for (int i = 0; i < connectedRoads.Count(); i++)
            {
                if (connectedRoads[i] != null)
                {
                    //north
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
                        //east
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
                        connectedRoads[i].changeSpawnability("W", true);
                    }
                    else if (i == 2)
                    {
                        if (connectedRoads[i] is Curve)
                        {
                            if (connectedRoads[i].getDirection() == "SW")
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
                        connectedRoads[i].changeSpawnability("N", true);
                    }
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

        public override string getDirection()
        {
            return "";
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
                    }
                    break;
                case "E":
                    {
                        this.lanes[3].SpawnAble = spawnAble;
                        this.lanes[2].SpawnAble = spawnAble;
                    }
                    break;
                case "S":
                    {
                        this.lanes[5].SpawnAble = spawnAble;
                    }
                    break;
                case "W":
                    {
                        this.lanes[7].SpawnAble = spawnAble;
                        this.lanes[8].SpawnAble = spawnAble;
                    }
                    break;
            }
        }


        public override void Draw(ref Graphics gr)
        {
            gr.DrawImage(image, coordinates.X, coordinates.Y, 150, 150);
            int[] id = { 0, 2, 3, 5, 7, 8 };
            for (int i = 0; i < id.Length; i++)
            {
                this.lanes[id[i]].TrafficLight.Draw(ref gr);
            }
            for (int i = 0; i < trafPed.Count(); i++)
            {
                if (trafPed[i] != null)
                    trafPed[i].Draw(ref gr);
            }
        }

        public override void DrawTrafficLight(ref Graphics gr)
        {
            int[] id = { 0, 2, 3, 5, 7, 8 };
            for (int i = 0; i < id.Length; i++)
            {
                if (this.lanes[id[i]].TrafficLight.ColorChanged==true)
                {
                    this.lanes[id[i]].TrafficLight.Draw(ref gr);
                    this.lanes[id[i]].TrafficLight.ColorChanged = false;
                }
            }
            for (int i = 0; i < trafPed.Count(); i++)
            {
                if (trafPed[i] != null)
                {
                    if (trafPed[i].ColorChanged == true)
                    {
                        trafPed[i].Draw(ref gr);
                        trafPed[i].ColorChanged = false;
                    }
                }
                    
            }
        }

        public override void DrawCars(ref Graphics g)
        {
            base.DrawCars(ref g);
            for (int i = 0; i < ped.Count(); i++)
            {
                    ped[i].draw(ref g);
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

        public override void startTimer()
        {
            base.startTimer();
            try
            {
                
                TrafTime.Tick -= OnTimerTick;
                TrafTime.Tick += OnTimerTick;          
                
            }
            catch
            {
                TrafTime.Tick += OnTimerTick;
            }
            
        }
        public override void disableTimer() 
        {
            base.disableTimer();
            pedTimer.Enabled = false;
            pedTimer.Stop();
            pedTimer.Tick -= OnPedestrianStart;
            for (int j = 0; j < 4; j++)
            {
                pedTimer.Tick -= OnPedestrianMove[j];
                ped[j].pos = ped[j].oriPosition;
            }
            
        }

        public void resetTrafficLight()
        {
            Random rand = new Random();
            int r = rand.Next(0, 4);
            if (r == 0)
            {
                this.lanes[0].TrafficLight.Colour = 3;
                this.lanes[2].TrafficLight.Timer = this.lanes[2].TrafficLight.greenInterval * 2;
                this.lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.greenInterval * 2;
                this.lanes[5].TrafficLight.Timer = this.lanes[5].TrafficLight.greenInterval;

                this.lanes[2].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Colour = 1;
                this.lanes[5].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[8].TrafficLight.Colour = 1;
                this.lanes[0].TrafficLight.Timer = 0;
                this.lanes[7].TrafficLight.Timer = 0;
                this.lanes[8].TrafficLight.Timer = 0;
            }
            else if (r == 1)
            {
                this.lanes[2].TrafficLight.Colour = 3;
                this.lanes[3].TrafficLight.Colour = 3;
                this.lanes[5].TrafficLight.Timer = this.lanes[5].TrafficLight.greenInterval * 2;
                this.lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.greenInterval;
                this.lanes[8].TrafficLight.Timer = this.lanes[8].TrafficLight.greenInterval;

                this.lanes[0].TrafficLight.Colour = 1;
                this.lanes[5].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[8].TrafficLight.Colour = 1;
                this.lanes[0].TrafficLight.Timer = 0;
                this.lanes[2].TrafficLight.Timer = 0;
                this.lanes[3].TrafficLight.Timer = 0;
            }
            else if (r == 2)
            {
                this.lanes[5].TrafficLight.Colour = 3;
                this.lanes[8].TrafficLight.Timer = this.lanes[8].TrafficLight.greenInterval * 2;
                this.lanes[7].TrafficLight.Timer = this.lanes[7].TrafficLight.greenInterval * 2;
                this.lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.greenInterval;

                this.lanes[0].TrafficLight.Colour = 1;
                this.lanes[2].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Colour = 1;
                this.lanes[7].TrafficLight.Colour = 1;
                this.lanes[8].TrafficLight.Colour = 1;
                this.lanes[5].TrafficLight.Timer = 0;
                this.lanes[2].TrafficLight.Timer = 0;
                this.lanes[3].TrafficLight.Timer = 0;
            }
            else
            {
                this.lanes[7].TrafficLight.Colour = 3;
                this.lanes[8].TrafficLight.Colour = 3;
                this.lanes[0].TrafficLight.Timer = this.lanes[0].TrafficLight.greenInterval * 2;
                this.lanes[2].TrafficLight.Timer = this.lanes[2].TrafficLight.greenInterval;
                this.lanes[3].TrafficLight.Timer = this.lanes[3].TrafficLight.greenInterval;

                this.lanes[2].TrafficLight.Colour = 1;
                this.lanes[3].TrafficLight.Colour = 1;
                this.lanes[0].TrafficLight.Colour = 1;
                this.lanes[5].TrafficLight.Colour = 1;
                this.lanes[5].TrafficLight.Timer = 0;
                this.lanes[7].TrafficLight.Timer = 0;
                this.lanes[8].TrafficLight.Timer = 0;

            }

            for (int i = 0; i < trafPed.Count(); i++)
            {
                trafPed[i].Colour = 1;
            }

        }

    }
}
