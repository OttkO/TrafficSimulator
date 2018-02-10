using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tracy
{
    public class Lane
    {
        //Spawning stuff
        private readonly Timer spawnTimer;
        private readonly Timer moveTimer;
        private int spawnTime = 3000;
        private bool spawnable;

        //Positioning stuff
        private readonly string direction;
        private readonly int numberOfPositions;
        private readonly int increments;
        private readonly Point start;
        private readonly  Point end;
        private readonly Point[] Positions;


        //Traffic stuff
        private TrafficLightC trafficLight;
        private readonly  List<Lane> ConnectedLanes;
        private readonly List<Car> Cars;
        private readonly List<Car> CarsInQueue;
        readonly int carLimit;




        // <summary>
        /// Constructor of lane.
        /// </summary>
        /// <param name="hasLight">Indicates if lane has a traffic light.</param>
        public Lane(Timer time, bool hasLight, Point pStart, Point pEnd, string direction)
        {
            //initialize fields
            this.numberOfPositions = 150;
            Cars = new List<Car>();
            ConnectedLanes = new List<Lane>();
            CarsInQueue = new List<Car>();
            Positions = new Point[numberOfPositions];
            this.start = pStart;
            this.end = pEnd;
            this.direction = direction;

            //spawning
            this.spawnable = true;
            this.spawnTimer = time;
            this.spawnTimer.Interval = spawnTime;
            this.moveTimer = new Timer();
            moveTimer.Interval = 25;
            moveTimer.Tick += this.moveCars;
            this.carLimit = 14;

            spawnTimer.Tick += this.SpawnCars;

            moveTimer.Start();

            //increments of positions

            //Define positions depending on direction
            switch (direction)
            {
                case "V":
                    //150 was: Math.Abs(pStart.Y - pEnd.Y)
                    increments = 150 / numberOfPositions;
                    //If lane is going from top to bottom then Y is ascending.
                    if (pStart.Y < pEnd.Y)
                    {
                        for (int i = 0; i < numberOfPositions; i++)
                        {
                            Positions[i] = new Point(pStart.X, pStart.Y + i * increments);

                        }

                    }
                    else
                    {
                        for (int i = 0; i < numberOfPositions; i++)
                        {
                            Positions[i] = new Point(pStart.X, pStart.Y - i * increments);

                        }

                    }

                    break;
                case "H":
                    increments = 150 / numberOfPositions;
                    //If lane is going from top to bottom then Y is ascending.
                    if (pStart.X < pEnd.X)
                    {
                        for (int i = 0; i < numberOfPositions; i++)
                        {
                            Positions[i] = new Point(pStart.X + i * increments, pStart.Y);

                        }

                    }
                    else
                    {
                        increments = 150 / numberOfPositions;
                        for (int i = 0; i < numberOfPositions; i++)
                        {
                            Positions[i] = new Point(pStart.X - i * increments, pStart.Y);

                        }

                    }
                    break;
                case "NE":
                    //150 was: Math.Abs(pStart.X - pEnd.X)
                    increments = 150 / numberOfPositions;
                    //If lane is going from top to bottom then Y is ascending.
                    if (pStart.X < pEnd.X)
                    {
                        Positions = new Point[numberOfPositions + 10];
                        for (int i = 0; i < (numberOfPositions / 2) + 5; i++)
                        {
                            Positions[i] = new Point(pStart.X - 5, pStart.Y + (i * increments) - 5);

                        }

                        for (int i = (numberOfPositions / 2) + 5; i < numberOfPositions + 10; i++)
                        {
                            Positions[i] = new Point(pStart.X - 5 + ((i - ((numberOfPositions / 2) + 5)) * increments), pStart.Y + (80 * increments));

                        }

                        numberOfPositions += 10;
                    }
                    else
                    {
                        carLimit = 12;
                        Positions = new Point[numberOfPositions - 20];
                        for (int i = 0; i < (numberOfPositions / 2) - 10; i++)
                        {
                            Positions[i] = new Point(pStart.X - i * increments, pStart.Y - 5);

                        }

                        for (int i = (numberOfPositions / 2) - 10; i < numberOfPositions - 20; i++)
                        {
                            Positions[i] = new Point(pStart.X - (65 * increments), pStart.Y - 5 - (i - ((numberOfPositions / 2) - 10) * increments));

                        }

                        numberOfPositions -= 20;

                    }

                    break;

                case "NW":
                    //150 was: Math.Abs(pStart.X - pEnd.X)
                    increments = 150 / numberOfPositions;

                    //Lane 0 postitions
                    if (pStart.X > pEnd.X)
                    {
                        carLimit = 12;
                        Positions = new Point[numberOfPositions - 20];
                        for (int i = 0; i < (numberOfPositions / 2) - 15; i++)
                        {
                            Positions[i] = new Point(pStart.X - 5, pStart.Y + (i * increments));

                        }

                        for (int i = (numberOfPositions / 2) - 15; i < numberOfPositions - 20; i++)
                        {
                            Positions[i] = new Point(pStart.X - 5 - ((i - 60) * increments), pStart.Y + (60 * increments));

                        }
                        numberOfPositions -= 20;

                    }
                    //Lane 1 positions
                    else
                    {
                        Positions = new Point[numberOfPositions-10];
                        for (int i = 0; i < (numberOfPositions / 2) - 10; i++)
                        {
                            Positions[i] = new Point(pStart.X + i * increments, pStart.Y);

                        }

                        for (int i = (numberOfPositions / 2) - 10; i < (numberOfPositions / 2)+10; i++)
                        {
                            Positions[i] = new Point(pStart.X + i * increments, pStart.Y - ((i-60)* increments));

                        }


                        for (int i = (numberOfPositions / 2)+10; i < numberOfPositions-10; i++)
                        {
                            Positions[i] = new Point(pStart.X + 7 + (75 * increments), pStart.Y - ((i - (numberOfPositions / 2) + 20) * increments));

                        }

                        numberOfPositions -= 10;
                    }

                    break;
                case "SE":
                    increments = 150 / numberOfPositions;
                    //If lane is going from top to bottom then Y is ascending.
                    if (pStart.X > pEnd.X)
                    {

                        Positions = new Point[numberOfPositions + 10];
                        for (int i = 0; i < (numberOfPositions / 2) + 10; i++)
                        {
                            Positions[i] = new Point(pStart.X - (i * increments), pStart.Y - 5);

                        }

                        for (int i = (numberOfPositions / 2) + 10; i < numberOfPositions + 10; i++)
                        {
                            Positions[i] = new Point(pStart.X - (90 * increments), pStart.Y + ((i - 90) * increments));

                        }

                        numberOfPositions += 10;
                    }
                    else
                    {
                        carLimit = 12;
                        Positions = new Point[numberOfPositions - 20];
                        for (int i = 0; i < (numberOfPositions / 2) - 10; i++)
                        {
                            Positions[i] = new Point(pStart.X, pStart.Y - (i * increments) - 5);

                        }

                        for (int i = (numberOfPositions / 2) - 10; i < numberOfPositions - 20; i++)
                        {
                            Positions[i] = new Point(pStart.X + (i - 65 * increments), pStart.Y - (65 * increments) - 5);

                        }
                        numberOfPositions -= 20;

                    }
                    break;
                case "SW":

                    increments = 150 / numberOfPositions;
                    //If lane is going from top to bottom then Y is ascending.
                    if (pStart.X < pEnd.X)
                    {
                        Positions = new Point[numberOfPositions - 20];
                        for (int i = 0; i < (numberOfPositions / 2) - 10; i++)
                        {
                            Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - 5);

                        }

                        for (int i = (numberOfPositions / 2) - 10; i < numberOfPositions - 20; i++)
                        {
                            Positions[i] = new Point(pStart.X + (65 * increments), pStart.Y + ((i - 65) * increments));

                        }
                        numberOfPositions -= 20;

                    }
                    else
                    {
                        carLimit = 12;
                        Positions = new Point[numberOfPositions + 20];
                        numberOfPositions += 20;
                        for (int i = 0; i < ((numberOfPositions-10)/ 2) + 10; i++)
                        {
                            Positions[i] = new Point(pStart.X, pStart.Y - (i * increments));

                        }

                        for (int i = ((numberOfPositions - 10) / 2) + 10; i < numberOfPositions; i++)
                        {
                            Positions[i] = new Point(pStart.X - (i - 90 * increments), pStart.Y - (90 * increments));

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
            this.Cars.Clear();
            this.CarsInQueue.Clear();
        }
        public Lane(Timer time, bool hasLight, Point pStart, Point pEnd, string direction, int laneNumber)
        {
            //initialize fields
            this.numberOfPositions = 150;
            Cars = new List<Car>();
            ConnectedLanes = new List<Lane>();
            CarsInQueue = new List<Car>();
            Positions = new Point[numberOfPositions];
            this.start = pStart;
            this.end = pEnd;
            this.direction = direction;



            //spawning
            this.spawnable = true;
            this.spawnTimer = time;
            this.spawnTimer.Interval = spawnTime;
            this.moveTimer = new Timer();
            moveTimer.Interval = 25;
            moveTimer.Tick += this.moveCars;

            spawnTimer.Tick += this.SpawnCars;

            moveTimer.Start();

            //increments of positions


            //Define positions depending on direction
            switch (direction)
            {
                case "CrossC":
                    {
                        increments = 1;


                        if ((laneNumber == 2) || (laneNumber == 6))
                        {
                            //Added by JP:
                            this.carLimit = 4;
                            this.numberOfPositions = 40;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                        }
                        else
                            if (laneNumber == 7)
                            {
                                //Added by JP:
                                this.carLimit = 3;
                                this.numberOfPositions = 40;
                                this.Positions = new Point[numberOfPositions];
                                for (int i = 0; i < (numberOfPositions / 4); i++)
                                {
                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y - (i * increments));
                                }

                                for (int i = (numberOfPositions / 4); i < numberOfPositions; i++)
                                {
                                    Positions[i] = new Point(pStart.X - (15 * increments), pStart.Y - (i * increments));
                                }
                            }
                            else
                                if ((laneNumber == 8) || (laneNumber == 0))
                                {
                                    //Added by JP:
                                    this.carLimit = 4;
                                    this.numberOfPositions = 40;
                                    this.Positions = new Point[numberOfPositions];
                                    for (int i = 0; i < numberOfPositions; i++)
                                    {
                                        Positions[i] = new Point(pStart.X, pStart.Y + (i * increments));
                                    }
                                }
                                else
                                    if (laneNumber == 1)
                                    {
                                        //Added by JP:
                                        this.carLimit = 3;
                                        this.numberOfPositions = 40;
                                        this.Positions = new Point[numberOfPositions];
                                        for (int i = 0; i < 9; i++)
                                        {
                                            Positions[i] = new Point(pStart.X + (i * increments), pStart.Y + (i * increments));
                                        }

                                        for (int i = 9; i < numberOfPositions; i++)
                                        {
                                            Positions[i] = new Point(pStart.X + (9 * increments), pStart.Y + (i * increments));
                                        }
                                    }
                                    else
                                        if ((laneNumber == 11))
                                        {
                                            //Added by JP:
                                            this.carLimit = 3;
                                            this.numberOfPositions = 45;
                                            this.Positions = new Point[numberOfPositions];
                                            for (int i = 0; i < numberOfPositions; i++)
                                            {
                                                Positions[i] = new Point(pStart.X - (i * increments), pStart.Y);
                                            }
                                        }
                                        else
                                            if ((laneNumber == 3))
                                            {
                                                //Added by JP:
                                                this.carLimit = 4;
                                                this.numberOfPositions = 40;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y);
                                                }
                                            }
                                        else if (laneNumber == 4)
                                        {
                                            //Added by JP:
                                            this.carLimit = 3;
                                            this.numberOfPositions = 40;
                                            this.Positions = new Point[numberOfPositions];
                                            for (int i = 0; i < ((numberOfPositions) / 4) * 0.7; i++)
                                            {
                                                Positions[i] = new Point(pStart.X - (i * increments), pStart.Y + (i * increments));
                                            }

                                            for (int i = (int)((numberOfPositions / 4) * 0.7); i < numberOfPositions; i++)
                                            {
                                                Positions[i] = new Point(pStart.X - (i * increments), pStart.Y + (7 * increments));
                                            }
                                        }
                                        else
                                            if ((laneNumber == 5) || (laneNumber == 9))
                                            {
                                                //Added by JP:
                                                this.carLimit = 4;
                                                this.numberOfPositions = 40;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y);
                                                }
                                            }
                                            else if (laneNumber == 10)
                                            {
                                                //Added by JP:
                                                this.carLimit = 4;
                                                this.numberOfPositions = 40;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions / 4) * 1.3); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - (i * increments));
                                                }

                                                for (int i = (int)((numberOfPositions / 4) * 1.3); i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - (13 * increments));
                                                }
                                            }
                                            else if (laneNumber == 12)
                                            {
                                                carLimit=2;
                                                this.numberOfPositions = 10;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y + (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 13)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 50;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < (numberOfPositions) / 4; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X, pStart.Y + (i * increments));
                                                }

                                                for (int i = (int)Math.Floor((numberOfPositions) / 4.0); i < (numberOfPositions / 3) * 1.5; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - ((i - 12) * increments), pStart.Y + (i * increments));
                                                }

                                                for (int i = (int)((numberOfPositions / 3) * 1.5); i < (numberOfPositions); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (12 * increments), pStart.Y + (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 14)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 40;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions - 10; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X, pStart.Y + (i * increments));
                                                }

                                                for (int i = numberOfPositions - 10; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + ((i - 30) * increments), pStart.Y + (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 15)
                                            {
                                                carLimit=2;
                                                this.numberOfPositions = 10;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y - (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 16)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 50;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions) / 4); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y);
                                                }

                                                for (int i = (int)Math.Floor((numberOfPositions) / 4.0); i < ((numberOfPositions) / 3) * 1.5; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y - ((i - 12) * increments));
                                                }

                                                for (int i = (int)(((numberOfPositions) / 3) * 1.5); i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y - (12 * increments));
                                                }
                                            }
                                            else if (laneNumber == 17)
                                            {
                                                this.numberOfPositions = 50;
                                                carLimit = 6;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions) / 2); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y);
                                                }

                                                for (int i = ((numberOfPositions) / 2); i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - (i * increments), pStart.Y + ((i - 25) * increments));
                                                }
                                            }
                                            else if (laneNumber == 18)
                                            {
                                                carLimit=2;
                                                this.numberOfPositions = 10;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions / 2; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - (i * increments));
                                                }
                                                for (int i = numberOfPositions / 2; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - (5 * increments));
                                                }
                                            }
                                            else if (laneNumber == 19)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 50;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions) / 2) / 2; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X, pStart.Y - (i * increments));
                                                }

                                                for (int i = ((numberOfPositions) / 2) / 2; i < ((numberOfPositions) / 3) * 1.5; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + ((i - 12) * increments), pStart.Y - (i * increments));
                                                }

                                                for (int i = (int)(((numberOfPositions) / 3) * 1.5); i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (12 * increments), pStart.Y - (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 20)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 45;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions) / 2); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X, pStart.Y - (i * increments));
                                                }

                                                for (int i = (int)Math.Floor((numberOfPositions) / 2.0); i < (numberOfPositions); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X - ((i - 25) * increments), pStart.Y - (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 21)
                                            {
                                                carLimit=2;
                                                this.numberOfPositions = 10;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < numberOfPositions; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y + (i * increments));
                                                }
                                            }
                                            else if (laneNumber == 22)
                                            {
                                                this.numberOfPositions = 50; carLimit = 6;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < ((numberOfPositions) / 2) / 2; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y);
                                                }

                                                for (int i = (int)Math.Floor((numberOfPositions) / 4.0); i < ((numberOfPositions) / 3) * 2; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y + ((i - 12) * increments));
                                                }

                                                for (int i = (int)Math.Floor(((numberOfPositions) / 3) * 2.0); i < (numberOfPositions); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y + (15 * increments));
                                                }
                                            }
                                            else if (laneNumber == 23)
                                            {
                                                carLimit = 6;
                                                this.numberOfPositions = 45;
                                                this.Positions = new Point[numberOfPositions];
                                                for (int i = 0; i < (numberOfPositions / 9) * 4; i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y);
                                                }

                                                for (int i = (numberOfPositions / 9) * 4; i < (numberOfPositions); i++)
                                                {
                                                    Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - ((i - 20) * increments));
                                                }
                                            }
                        break;
                    }
                case "CrossP":
                    {
                        increments = 1;
                        if ((laneNumber == 6))
                        {
                            //Added by JP:
                            this.carLimit = 4;
                            this.numberOfPositions = 40;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y + i * increments);
                            }
                        }
                        else if (laneNumber == 0)
                        {
                            this.carLimit = 2;
                            this.numberOfPositions = Math.Abs(pStart.Y-pEnd.Y);
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y + i * increments);
                            }
                        }
                        else if ((laneNumber == 1))
                        {
                            //Added by JP:
                            this.carLimit = 4;
                            this.numberOfPositions = 40;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                        }
                        else if (laneNumber == 5)
                        {
                            this.carLimit = 2;
                            this.numberOfPositions = Math.Abs(pStart.Y - pEnd.Y);
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                        }
                        else if ((laneNumber == 2) || (laneNumber == 9))
                        {
                            //Added by JP:
                            this.carLimit = 4;
                            this.numberOfPositions = 43;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y);
                            }
                        }
                        else if (laneNumber == 3)
                        {
                            //Added by JP:
                            this.carLimit = 3;
                            this.numberOfPositions = 40;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < (numberOfPositions / 4) * 1.8; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y + i * increments);
                            }
                            for (int i = (int)((numberOfPositions / 4) * 1.8); i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y + (18 * increments));
                            }
                        }
                        else if ((laneNumber == 4) || (laneNumber == 7))
                        {
                            //Added by JP:
                            this.carLimit = 4;
                            this.numberOfPositions = 43;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y);
                            }
                        }
                        else if (laneNumber == 8)
                        {
                            //Added by JP:
                            this.carLimit = 3;
                            this.numberOfPositions = 40;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < (numberOfPositions / 4) * 1.5; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y - i * increments);
                            }
                            for (int i = (int)((numberOfPositions / 4) * 1.5); i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y - (15 * increments));
                            }
                        }
                        else if (laneNumber == 10)
                        {
                            carLimit=2;
                            this.numberOfPositions = 30;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < (numberOfPositions/3)*2; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y + i * increments);
                            }
                            for (int i = (numberOfPositions / 3) * 2; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - ((i-20) * increments), pStart.Y + i * increments);
                            }

                        }
                        else if (laneNumber == 11)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 80;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y + i * increments);
                            }

                        }
                        else if (laneNumber == 12)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 60;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions / 2; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y + i * increments);
                            }
                            for (int i = numberOfPositions / 2; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + ((i - 30) * increments), pStart.Y + i * increments);
                            }

                        }
                        else if (laneNumber == 13)
                        {
                            carLimit=2;
                            this.numberOfPositions = 10;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y - i * increments);
                            }
                        }
                        else if (laneNumber == 14)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 60;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y);
                            }
                        }
                        else if (laneNumber == 15)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 45;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions / 2-5; i++)
                            {
                                Positions[i] = new Point(pStart.X - i * increments, pStart.Y);
                            }
                            for (int i = numberOfPositions / 2-5; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - (i * increments), pStart.Y + ((i - 18) * increments));
                            }
                        }
                        else if (laneNumber == 16)
                        {
                            carLimit=2;
                            this.numberOfPositions = 30;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < (numberOfPositions / 3) * 2; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                            for (int i = (numberOfPositions / 3) * 2; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + ((i - 20) * increments), pStart.Y - i * increments);
                            }
                        }
                        else if (laneNumber == 17)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 80;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                        }
                        else if (laneNumber == 18)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 60;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions / 2; i++)
                            {
                                Positions[i] = new Point(pStart.X, pStart.Y - i * increments);
                            }
                            for (int i = numberOfPositions / 2; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X - ((i - 30) * increments), pStart.Y - (i * increments));
                            }
                        }
                        else if (laneNumber == 19)
                        {
                            carLimit=2;
                            this.numberOfPositions = 10;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y + i * increments);
                            }
                        }
                        else if (laneNumber == 20)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 60;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y);
                            }
                        }
                        else if (laneNumber == 21)
                        {
                            carLimit = 6;
                            this.numberOfPositions = 45;
                            this.Positions = new Point[numberOfPositions];
                            for (int i = 0; i < numberOfPositions / 2; i++)
                            {
                                Positions[i] = new Point(pStart.X + i * increments, pStart.Y);
                            }

                            for (int i = numberOfPositions / 2; i < numberOfPositions; i++)
                            {
                                Positions[i] = new Point(pStart.X + (i * increments), pStart.Y - ((i - 22) * increments));
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

        public bool SpawnAble
        {
            get { return spawnable; }
            set { spawnable = value; }
        }

        public int SpawnTime
        {
            get
            {
                return this.spawnTime;
            }
            set
            {
                this.spawnTime = value;
            }
        }


        public void AddCar(Car newCar)
        {
            newCar.Coordinates = this.Positions[0];
            //add new car.
            this.Cars.Add(newCar);

        }

        /// <summary>
        /// Once the car has reached the end of the lane, assuming the light is green. 
        /// It will move the car to the next connected lane.
        /// </summary>
        /// <param name="Car">Car to be moved to next lane</param>
        public void ChangeCarToNextLane(Car pCar)
        {
            //if there are no other lanes, then destroy
            if (this.ConnectedLanes.Count == 0)
            {
                Cars.Remove(pCar);
            }
            //if there is one connected lane, then add to next lane and remove from current
            else if (this.ConnectedLanes.Count == 1)
            {
                if (this.ConnectedLanes[0].carLimit > this.ConnectedLanes[0].Cars.Count)
                {
                    this.ConnectedLanes[0].AddCar(pCar);
                    Cars.Remove(pCar);
                }

            }
            //if there a more lanes, then choose random lane
            else if (this.ConnectedLanes.Count == 2)
            {
                Random rand = new Random();
                int number = rand.Next(0, 2);
                    if (this.ConnectedLanes[number].carLimit > this.ConnectedLanes[number].Cars.Count)
                    {
                        this.ConnectedLanes[number].AddCar(pCar);
                        Cars.Remove(pCar);
                    }

            }
            else
            {
                Random rand = new Random();
                int number = rand.Next(0, 3);
                    if (this.ConnectedLanes[number].carLimit > this.ConnectedLanes[number].Cars.Count)
                    {
                        this.ConnectedLanes[number].AddCar(pCar);
                        Cars.Remove(pCar);
                    }
            }

        }


        public void ConnectToLane(Lane lane)
        {
            this.ConnectedLanes.Add(lane);
            lane.spawnable = false;
        }

        public void DisconnectLanes()
        {
            for (int i = 0; i < ConnectedLanes.Count; i++)
            {
                ConnectedLanes[i].spawnable = true;
            }

            this.ConnectedLanes.Clear();
        }

        //JP Version
        public void moveCars(object obj, EventArgs args)
        {

            int positionIndex = -1;

            //for each car, check if can move, if so, move car
            //if first car and there is room, then move
            for (int i = 0; i < this.Cars.Count; i++)
            {
                positionIndex = this.getPositionIndexFromCar(Cars[i]);

                //if first car is at last postion
                if (Cars[i].Coordinates == this.Positions[numberOfPositions - 1])
                {
                    if (TrafficLight == null)
                    {
                        //there's no light so, just move to next
                        ChangeCarToNextLane(Cars[i]);
                    }
                    //if trafficLight is present and is green then change To next lane
                    else if (TrafficLight.Colour == 3)
                    {
                        ChangeCarToNextLane(Cars[i]);
                    }
                }
                else if (i == 0 || positionIndex == 0)
                {
                    //there is room, so move
                    MoveToNextPosition(i);
                }
                try
                {
                    if ((positionIndex + 7 + Cars[i].Speed) >= Positions.Length) { continue; }
                }
                catch
                {
                    
                    continue; 
                }
                try
                {
                    if (i != 0 && Cars[i - 1] != null &&
                                      (
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 1] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 2] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 3] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 4] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 5] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 6] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 8] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 9] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 10] ||
                                       Cars[i - 1].Coordinates == Positions[positionIndex + 11] ||
                                      Cars[i - 1].Coordinates == Positions[positionIndex + 1 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 2 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 3 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 4 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 5 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 6 + Cars[i].Speed]
                                      || Cars[i - 1].Coordinates == Positions[positionIndex + 7 + Cars[i].Speed]
                                      ))
                    {
                        //method check for collision or something
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
                    continue; }


            }


        }


        private void MoveToNextPosition(int i)
        {
            int positionIndex = 0;
            int cycles = 0;
            for (int j = 0; j < this.numberOfPositions - 1; j++)
            {


                if (Cars[i].Coordinates == Positions[j])
                {
                    positionIndex = this.getPositionIndexFromCar(Cars[i]);
                    if (i != 0 && Cars[i - 1] != null &&
                        (Cars[i - 1].Coordinates == Positions[positionIndex + 1] ||
                         Cars[i - 1].Coordinates == Positions[positionIndex + 2] ||
                         Cars[i - 1].Coordinates == Positions[positionIndex + 3] ||
                         Cars[i - 1].Coordinates == Positions[positionIndex + 4] ||
                         Cars[i - 1].Coordinates == Positions[positionIndex + 5] ||
                         Cars[i - 1].Coordinates == Positions[positionIndex + 6]))
                    {

                        while (Cars[i].Speed == 1)
                        {
                            Cars[i].decellerate();
                        }
                    }


                    else
                    {
                        if (Cars[i].Speed <= Cars[i].CruiseSpeed)
                        {
                            Cars[i].accelerate();
                        }
                    }

                    if (((j + Cars[i].Speed) < Positions.Length) && (cycles == 2))
                    {
                        Cars[i].Coordinates = Positions[j + Cars[i].Speed];
                        cycles = 0;
                    }
                    else
                    {
                        Cars[i].Coordinates = Positions[j + 1];
                    }

                    return;
                }
                cycles++;
            }
        }

        public Timer MoveTimer
        {
            get { return this.moveTimer;}

        }

        public TrafficLightC TrafficLight { get => trafficLight; set => trafficLight = value; }

        /// <summary>
        /// Return number of cars in the cars list.
        /// </summary>
        /// <returns></returns>
        public int GetNrOfCars()
        {
            return this.Cars.Count;
        }

        /// <summary>
        /// Return number of cars in the cars in queque list.
        /// </summary>
        /// <returns></returns>
        public int GetNrOfCarsInQueue()
        {
            return this.CarsInQueue.Count;
        }

        public void SpawnCars(object obj, EventArgs args)
        {
            //if lane is not spawnable, or  is full, do nothing.
            if (!this.spawnable || this.carLimit == this.Cars.Count)
            {
                return;
            }

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Coordinates == this.Positions[0])
                {
                    return;
                }
            }

            this.Cars.Add(new Car(this.Positions[0]));


        }

        /// <summary>
        /// Draw all cars to the passed graphics
        /// </summary>
        /// <param name="gr"></param>
        public void drawCars(ref Graphics gr)
        {
            for (int i = 0; i < this.Cars.Count; i++)
            {
                this.Cars[i].Draw(ref gr);
            }

        }

        /// <summary>
        /// returns index of the position in the position array of the car.
        /// </summary>
        /// <param name="aCar">The car of which you want to find out the position index</param>
        /// <returns></returns>

        public int getPositionIndexFromCar(Car aCar)
        {


            for (int j = 0; j < this.numberOfPositions; j++)
            {
                if (aCar.Coordinates == this.Positions[j])
                    return j;
            }


            //to satisfy compiler, should never be reached.
            return -1;

        }

        public bool checkForCollisionCourse(Car aCar)
        {
            //check if there's a car in front
            //check is speed,
            //check my speed,
            //will we collide?
            //yes? Slow down.
            //no? maintain speed.
            return false;
        }

        public void increaseSpawnTime()
        {
            this.spawnTime += 500;
        }

        public void decreaseSpawnTime()
        {
            this.spawnTime -= 500;
        }
    }
}
