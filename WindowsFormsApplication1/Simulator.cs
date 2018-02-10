using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace tracy
{
    public class Simulator
    {
        private readonly int cellSize;
        private readonly int nrOfColumns;
        private readonly int nrOfRows;
        private int nrOfCrossroads;
        readonly System.Windows.Forms.Timer masterTimer;
       
        /// <summary>
        /// 2D array that contains all road elements
        /// </summary>
        private readonly Road[,] Roads;
        /// <summary>
        /// Constructor of Simulator.
        /// </summary>
        /// <param name="cellsize">Sice of grid cell.</param>
        public Simulator(int cellsize)
        {
            this.cellSize = cellsize;
            this.nrOfColumns = 7;
            this.nrOfRows = 6;
            this.nrOfCrossroads = 0;
            Roads = new Road[nrOfRows,nrOfColumns];

            masterTimer = new System.Windows.Forms.Timer();

        }


        public bool pressSensor(Point coor)
        {
            int[] cellNumber = getSpotNumber(coor);
            if (Roads[cellNumber[0], cellNumber[1]] is PCrossroad)
            {
                ((PCrossroad)Roads[cellNumber[0], cellNumber[1]]).PressSensor();
                return true;
            }
            else return false;
        }

        public Point Pcross(Point coor)
        {
            int[] cellNum = getSpotNumber(coor);
            if (Roads[cellNum[0], cellNum[1]] != null && (Roads[cellNum[0], cellNum[1]] is PCrossroad))
            {        
                    return Roads[cellNum[0], cellNum[1]].Coordinates;
            }
            return new Point(-10, -10);
        }


        public Point existCrossRoad(int[] spotNum)
        {
            if (Roads[spotNum[0], spotNum[1]] != null && (Roads[spotNum[0], spotNum[1]] is CCrossroad) || (Roads[spotNum[0], spotNum[1]] is PCrossroad))
            {
                return Roads[spotNum[0], spotNum[1]].Coordinates;
            }
            return new Point(-10, -10);

        }

        /// <summary>
        /// Starts timer
        /// </summary>
        public void StartSim()
        {
           // this.masterTimer.Enabled = true;
            //this.masterTimer.Start();
            for (int i = 0; i < nrOfRows; i++)
            {
                for (int j = 0; j < nrOfColumns; j++)
                {
                    if (Roads[i,j] != null)
                    {
                        Roads[i,j].startTimer();
                    }            
                }
            }
            
        }

        /// <summary>
        /// Stops simulation timers and removes all cars.
        /// </summary>
        public void StopSim()
        {
            for (int i = 0; i < nrOfRows; i++)
            {
                for (int j = 0; j < nrOfColumns; j++)
                {
                    if (Roads[i, j] != null)
                    {
                        if (Roads[i, j] is PCrossroad)
                        {
                            ((PCrossroad)Roads[i, j]).disableTimer();
                        }
                        else
                        Roads[i, j].disableTimer();
                        for (int c = 0; c < Roads[i, j].getListOfLane().Count; c++)
                        {
                            Roads[i, j].getListOfLane()[c].ClearCarList();
                        }

                        if (Roads[i, j] is CCrossroad)
                        {
                            ((CCrossroad)Roads[i, j]).resetTrafficLight();
                        }
                        else if (Roads[i, j] is PCrossroad)
                        {
                            ((PCrossroad)Roads[i, j]).resetTrafficLight();
                        }

                        List<Lane> lr = Roads[i, j].getListOfLane();
                        for(int k = 0; k< lr.Count;k++)
                        {
                            lr[k].SpawnTime = 500 + 5 * 500;  
                        }
                        Roads[i, j].Time.Interval = 500 + 5 * 500;      
                               
                    }
                }

            }

            masterTimer.Stop();
        }

        public void PaintAll(ref Graphics graph)
        {
            DrawGrid(ref graph);
            foreach (Road r in Roads)
            {
                if (r != null)
                {
                    r.Draw(ref graph);
                }
            }
        }

        public void PaintLights(ref Graphics graph)
        {
            foreach (Road r in Roads)
            {
                if (r != null && (r is CCrossroad || r is PCrossroad))
                {
                        r.DrawTrafficLight(ref graph);
                }
            }
        }

        public void PaintCars(ref Graphics graph)
        {
            graph.Clear(Color.Transparent);
            foreach (Road r in Roads)
            {
                if (r != null)
                {
                    if (r is PCrossroad)
                    {
                        ((PCrossroad)r).DrawCars(ref graph);
                    }
                    else
                    {
                        r.DrawCars(ref graph);
                    }
                }
            }
        }

        /// <summary>
        /// Draws a grid on the passed picturebox.
        /// Size and cells depend on variables of Simulator.
        /// </summary>
        /// <param name="pic"></param>
        public void DrawGrid(ref Graphics g)
        {
            Pen p = new Pen(Color.Black);

            //Draw horizontal lines.
            for (int y = 0; y < (nrOfRows + 2); ++y)
            {
                g.DrawLine(p, 0, y * cellSize, (nrOfRows + 1) * cellSize, (y * cellSize));
            }

            //draw vertical lines.
            for (int x = 0; x < (nrOfColumns + 2); ++x)
            {
                g.DrawLine(p, x * cellSize, 0, x * cellSize, (nrOfColumns + 1)* cellSize);
            }

        }

        public void adjustGreenTime(Point coord, int time)
        {
            int[] cell = getSpotNumber(coord);
            Road R = Roads[cell[0], cell[1]];
            if (R != null)
            {
                if (R is CCrossroad)
                {
                    ((CCrossroad)R).adjustGreenTime(time);
                }
                else if (R is PCrossroad)
                {
                    ((PCrossroad)R).adjustGreenTime(time);
                }
            }
        }

        //doesnt mean anything
        public void ResetSim()
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Adds Road to the 2D array, if it is allwed, and also connects to neighbours.
        /// </summary>
        /// <param name="Coordinate"></param>
        /// <param name="type"></param>
        public bool AddRoad(Point coordinates, string type, string direction, ref Graphics g)
        {

            int[] cellNumber = getSpotNumber(coordinates);

            Point placementCoordinates = new Point();

            placementCoordinates.X = cellNumber[1] * cellSize;
            placementCoordinates.Y = cellNumber[0] * cellSize;

            //if selected spot if filled, do nothing
            if ((Roads[cellNumber[0], cellNumber[1]] != null))
            {
                return false;
            }


            if (checkNeighbours(cellNumber, type, direction))
            {

                switch (type)
                {
                    case "PCrossroad":
                        if (nrOfCrossroads == 12)
                        {
                            return false;
                        }
                        this.Roads[cellNumber[0], cellNumber[1]] = new PCrossroad(placementCoordinates);
                        this.Roads[cellNumber[0], cellNumber[1]].graphic = g;
                        this.connectToNeighbours(cellNumber);
                        nrOfCrossroads++;
                        break;
                    case "CCrossroad":
                        if (nrOfCrossroads == 12)
                        {
                            return false;
                        }
                        this.Roads[cellNumber[0], cellNumber[1]] = new CCrossroad(placementCoordinates);
                        this.Roads[cellNumber[0], cellNumber[1]].graphic = g;
                        this.connectToNeighbours(cellNumber);
                        nrOfCrossroads++;
                        break;
                    case "Straight":

                        this.Roads[cellNumber[0], cellNumber[1]] = new Straight(placementCoordinates, direction);
                        this.connectToNeighbours(cellNumber);
                        break;
                    case "Curve":

                        this.Roads[cellNumber[0], cellNumber[1]] = new Curve(placementCoordinates, direction);
                        this.connectToNeighbours(cellNumber);
                        break;
                }
                return true;
            }
            return false;

        }

        public void RemoveRoad(Point coordinates)
        {
            int[] cellNumber = getSpotNumber(coordinates);

            //if selected spot if empty, do nothing
            if (Roads[cellNumber[0], cellNumber[1]] == null)
            {
                return;
            }

            //disconnect from neighbours
            Roads[cellNumber[0], cellNumber[1]].DisconnectRoad();

            //make neigbours spawnable


            //remove lanes
            Roads[cellNumber[0], cellNumber[1]].RemoveLanes();
            if ((Roads[cellNumber[0], cellNumber[1]] is PCrossroad) || (Roads[cellNumber[0], cellNumber[1]] is CCrossroad))
            {
                nrOfCrossroads--;
            }
            //Remove Road from Array 
            Roads[cellNumber[0], cellNumber[1]] = null;
            
        }

        //implement later
        public void AdjustSpawnTime(int newTime)
        {
            foreach(Road r in this.Roads)
            {
                if (r != null)
                {
                    foreach (Lane l in r.getListOfLane())
                    {
                        l.SpawnTime = 500 + newTime * 500;
                        
                    }
                    r.Time.Interval = 500 + newTime * 500; 
                }
            }
        }

        public void AdjustCarSpeed(int newSpeed)
        {
            foreach (Road r in this.Roads)
            {
                if (r != null)
                {
                    foreach (Lane l in r.getListOfLane())
                    {
                        l.MoveTimer.Interval =  5 + newSpeed * 5;
                    }
                    
                }
            }
        }

        /// <summary>
        /// Returns number of the selected cell
        /// </summary>
        /// <param name="Coordinates">Coordinates of mouse click</param>
        /// <returns></returns>
        public int[] getSpotNumber(Point Coordinates)
        {
            int[] cellNumber = new int[2];

            cellNumber[0] = (Coordinates.Y / cellSize);
            cellNumber[1] = (Coordinates.X / cellSize);

            return cellNumber;

        }

        /// <summary>
        /// Checks if there are neighbours present and if it is allowed to add road to certain spot.
        /// Returns True if it is allowed.
        /// </summary>
        /// <param name="cellNumber"></param>
        /// <returns></returns>
        public bool checkNeighbours(int[] cellNumber, string type, string direction)
        {

            int[] North = new int[2];
            int[] East = new int[2];
            int[] South = new int[2];
            int[] West = new int[2];

            North[0] = cellNumber[0] - 1;
            North[1] = cellNumber[1];
            East[0] = cellNumber[0];
            East[1] = cellNumber[1] + 1;
            South[0] = cellNumber[0] + 1;
            South[1] = cellNumber[1];
            West[0] = cellNumber[0];
            West[1] = cellNumber[1] - 1;

            bool ignoreNorth = false;
            bool ignoreEast = false;
            bool ignoreSouth = false;
            bool ignoreWest = false;

            //Check all situations where we could ignore checking a side
            //Check if on north border or have neighbour to north
            if (cellNumber[0] == 0 || Roads[North[0], North[1]] == null)
            {
                ignoreNorth = true;
            }

            //Check if on East border or have neighbour to east
            if (cellNumber[1] == this.nrOfColumns - 1 || Roads[East[0], East[1]] == null)
            {
                ignoreEast = true;
            }

            //Check if on south border or have neighour to south
            if (cellNumber[0] == this.nrOfRows - 1 || Roads[South[0], South[1]] == null)
            {
                ignoreSouth = true;
            }

            //Check if on west border or have neighbour to west
            if (cellNumber[1] == 0 || Roads[West[0], West[1]] == null)
            {
                ignoreWest = true;
            }




            //Check all non-ignored sides if allowed
            //return false at point where is is not allowed.
            switch (type)
            {

                case "PCrossroad":


                    //check north
                    if (!ignoreNorth)
                    {
                        if (Roads[North[0], North[1]] is Curve &&
                           (Roads[North[0], North[1]].getDirection() == "NE" || Roads[North[0], North[1]].getDirection() == "NW"))
                        {
                            return false;
                        }

                        if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "H")
                        {
                            return false;
                        }

                    }

                    //check east
                    if (!ignoreEast)
                    {
                        if (Roads[East[0], East[1]] is Curve &&
                           (Roads[East[0], East[1]].getDirection() == "SE" || Roads[East[0], East[1]].getDirection() == "NE"))
                        {
                            return false;
                        }

                        if (Roads[East[0], East[1]] is Straight && Roads[East[0], East[1]].getDirection() == "V")
                        {
                            return false;
                        }

                    }

                    //check south
                    if (!ignoreSouth)
                    {
                        if (Roads[South[0], South[1]] is Curve &&
                        (Roads[South[0], South[1]].getDirection() == "SE" || Roads[South[0], South[1]].getDirection() == "SW"))
                        {
                            return false;
                        }

                        if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "H")
                        {
                            return false;
                        }

                    }
                    //check west
                    if (!ignoreWest)
                    {

                        if (Roads[West[0], West[1]] is Curve &&
                           (Roads[West[0], West[1]].getDirection() == "SW" || Roads[West[0], West[1]].getDirection() == "NW"))
                        {
                            return false;
                        }

                        if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "V")
                        {
                            return false;
                        }

                    }


                    break;

                case "CCrossroad":

                    //check north
                    if (!ignoreNorth)
                    {
                        if (Roads[North[0], North[1]] is Curve &&
                           (Roads[North[0], North[1]].getDirection() == "NE" || Roads[North[0], North[1]].getDirection() == "NW"))
                        {
                            return false;
                        }

                        if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "H")
                        {
                            return false;
                        }

                    }

                    //check east
                    if (!ignoreEast)
                    {
                        if (Roads[East[0], East[1]] is Curve &&
                           (Roads[East[0], East[1]].getDirection() == "SE" || Roads[East[0], East[1]].getDirection() == "NE"))
                        {
                            return false;
                        }

                        if (Roads[East[0], East[1]].getDirection() == "Straight" && Roads[East[0], East[1]].getDirection() == "V")
                        {
                            return false;
                        }

                    }

                    //check south
                    if (!ignoreSouth)
                    {
                        if (Roads[South[0], South[1]] is Curve &&
                        (Roads[South[0], South[1]].getDirection() == "SE" || Roads[South[0], South[1]].getDirection() == "SW"))
                        {
                            return false;
                        }

                        if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "H")
                        {
                            return false;
                        }

                    }
                    //check west
                    if (!ignoreWest)
                    {

                        if (Roads[West[0], West[1]] is Curve &&
                           (Roads[West[0], West[1]].getDirection() == "SW" || Roads[West[0], West[1]].getDirection() == "NW"))
                        {
                            return false;
                        }

                        if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "V")
                        {
                            return false;
                        }

                    }


                    break;
                case "Straight":

                    if (direction == "H")
                    {
                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "SE" || Roads[North[0], North[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "V")
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is CCrossroad || Roads[North[0], North[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "NE" || Roads[East[0], East[1]].getDirection() == "SE"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]].getDirection() == "Straight" && Roads[East[0], East[1]].getDirection() == "V")
                            {
                                return false;
                            }

                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "NW" || Roads[South[0], South[1]].getDirection() == "NE"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "V")
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is CCrossroad || Roads[South[0], South[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "NW" || Roads[West[0], West[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "V")
                            {
                                return false;
                            }

                        }
                    }
                    else if (direction == "V")
                    {
                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "NW" || Roads[North[0], North[1]].getDirection() == "NE"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "H")
                            {
                                return false;
                            }



                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "NW" || Roads[East[0], East[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]].getDirection() == "Straight" && Roads[East[0], East[1]].getDirection() == "H")
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is CCrossroad || Roads[East[0], East[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "SE" || Roads[South[0], South[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "H")
                            {
                                return false;
                            }



                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "NE" || Roads[West[0], West[1]].getDirection() == "SE"))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "H")
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is CCrossroad || Roads[West[0], West[1]] is PCrossroad)
                            {
                                return false;
                            }


                        }
                    }


                    break;

                case "Curve":


                    if (direction == "NE")
                    {
                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "NE" || Roads[North[0], North[1]].getDirection() == "NW"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "H")
                            {
                                return false;
                            }

                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "NE" || Roads[East[0], East[1]].getDirection() == "SE"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is Straight && Roads[East[0], East[1]].getDirection() == "V")
                            {
                                return false;
                            }

                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "NE"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "V")
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is CCrossroad || Roads[South[0], South[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "NE" || Roads[West[0], West[1]].getDirection() == "SE" ))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "H")
                            {
                                return false;
                            }
                            if (Roads[West[0], West[1]] is CCrossroad || Roads[West[0], West[1]] is PCrossroad)
                            {
                                return false;
                            }
                        }

                    }
                    else if (direction == "NW")
                    {

                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "NE" || Roads[North[0], North[1]].getDirection() == "NW"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "H")
                            {
                                return false;
                            }

                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "NW"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is Straight && Roads[East[0], East[1]].getDirection() == "H")
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is CCrossroad || Roads[East[0], East[1]] is PCrossroad)
                            {
                                return false;
                            }


                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "NW"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "V")
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is CCrossroad || Roads[South[0], South[1]] is PCrossroad)
                            {
                                return false;
                            }


                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "SW" || Roads[West[0], West[1]].getDirection() == "NW"))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "V")
                            {
                                return false;
                            }

                        }
                    }
                    else if (direction == "SE")
                    {
                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "SE" || Roads[North[0], North[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "V")
                            {
                                return false;
                            }


                            if (Roads[North[0], North[1]] is CCrossroad || Roads[North[0], North[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "SE" || Roads[East[0], East[1]].getDirection() == "NE"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is Straight && Roads[East[0], East[1]].getDirection() == "V")
                            {
                                return false;
                            }


                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "SE" || Roads[South[0], South[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "H")
                            {
                                return false;
                            }

                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "NE" || Roads[West[0], West[1]].getDirection() == "SE"))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "H")
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is CCrossroad || Roads[West[0], West[1]] is PCrossroad)
                            {
                                return false;
                            }
                        }
                    }
                    else if (direction == "SW")
                    {
                        //check north
                        if (!ignoreNorth)
                        {
                            if (Roads[North[0], North[1]] is Curve &&
                               (Roads[North[0], North[1]].getDirection() == "SE" || Roads[North[0], North[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[North[0], North[1]] is Straight && Roads[North[0], North[1]].getDirection() == "V")
                            {
                                return false;
                            }


                            if (Roads[North[0], North[1]] is CCrossroad || Roads[North[0], North[1]] is PCrossroad)
                            {
                                return false;
                            }

                        }

                        //check east
                        if (!ignoreEast)
                        {
                            if (Roads[East[0], East[1]] is Curve &&
                               (Roads[East[0], East[1]].getDirection() == "NW" || Roads[East[0], East[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is Straight && Roads[East[0], East[1]].getDirection() == "H")
                            {
                                return false;
                            }

                            if (Roads[East[0], East[1]] is CCrossroad || Roads[East[0], East[1]] is PCrossroad)
                            {
                                return false;
                            }


                        }

                        //check south
                        if (!ignoreSouth)
                        {
                            if (Roads[South[0], South[1]] is Curve &&
                            (Roads[South[0], South[1]].getDirection() == "SE" || Roads[South[0], South[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[South[0], South[1]] is Straight && Roads[South[0], South[1]].getDirection() == "H")
                            {
                                return false;
                            }

                        }
                        //check west
                        if (!ignoreWest)
                        {

                            if (Roads[West[0], West[1]] is Curve &&
                               (Roads[West[0], West[1]].getDirection() == "NW" || Roads[West[0], West[1]].getDirection() == "SW"))
                            {
                                return false;
                            }

                            if (Roads[West[0], West[1]] is Straight && Roads[West[0], West[1]].getDirection() == "V")
                            {
                                return false;
                            }

                        }
                    }


                    break;

                default:
                    return false;

            }



            return true;
        }

        private void connectToNeighbours(int[] cellNumber)
        {

            int[] North = new int[2];
            int[] East = new int[2];
            int[] South = new int[2];
            int[] West = new int[2];

            North[0] = cellNumber[0] - 1;
            North[1] = cellNumber[1];
            East[0] = cellNumber[0];
            East[1] = cellNumber[1] + 1;
            South[0] = cellNumber[0] + 1;
            South[1] = cellNumber[1];
            West[0] = cellNumber[0];
            West[1] = cellNumber[1] - 1;

            bool ignoreNorth = false;
            bool ignoreEast = false;
            bool ignoreSouth = false;
            bool ignoreWest = false;

            //Check all situations where we could ignore checking a side
            //Check if on north border or have neighbour to north
            if (cellNumber[0] == 0 || Roads[North[0], North[1]] == null)
            {
                ignoreNorth = true;
            }

            //Check if on East border or have neighbour to east
            if (cellNumber[1] == this.nrOfColumns - 1 || Roads[East[0], East[1]] == null)
            {
                ignoreEast = true;
            }

            //Check if on south border or have neighour to south
            if (cellNumber[0] == this.nrOfRows - 1 || Roads[South[0], South[1]] == null)
            {
                ignoreSouth = true;
            }

            //Check if on west border or have neighbour to west
            if (cellNumber[1] == 0 || Roads[West[0], West[1]] == null)
            {
                ignoreWest = true;
            }

            if (!ignoreNorth)
            {
                //Connect new road to northern neighbour
                if (Roads[cellNumber[0], cellNumber[1]].ConnectToRoad("N", Roads[North[0], North[1]]))
                {
                    //connect northern neighbour to new road .
                    Roads[North[0], North[1]].ConnectToRoad("S", Roads[cellNumber[0], cellNumber[1]]);
                    //Connect lanes
                    Roads[cellNumber[0], cellNumber[1]].ConnectLanesTo("N", Roads[North[0], North[1]]);
                }
                

               

            }

            if (!ignoreEast)
            {
                //Connect new road to eastern neighbour
                if (Roads[cellNumber[0], cellNumber[1]].ConnectToRoad("E", Roads[East[0], East[1]]))
                {
                    //connect eastern neighbour to new road .
                    Roads[East[0], East[1]].ConnectToRoad("W", Roads[cellNumber[0], cellNumber[1]]);

                    //if connection allowed, then connect lanes
                    Roads[cellNumber[0], cellNumber[1]].ConnectLanesTo("E", Roads[East[0], East[1]]);
                }

                
               
            }

            if (!ignoreSouth)
            {
                //Connect new road to southern neighbour
                if (Roads[cellNumber[0], cellNumber[1]].ConnectToRoad("S", Roads[South[0], South[1]])) 
                {
                    //connect southern neighbour to new road .
                    Roads[South[0], South[1]].ConnectToRoad("N", Roads[cellNumber[0], cellNumber[1]]);
                    //if connection allowed, connect lanes
                    Roads[cellNumber[0], cellNumber[1]].ConnectLanesTo("S", Roads[South[0], South[1]]);
                }
               

               

            }

            if (!ignoreWest)
            {
                //Connect new road to western neighbour
                if (Roads[cellNumber[0], cellNumber[1]].ConnectToRoad("W", Roads[West[0], West[1]]))
                {
                    //connect western neighbour to new road .
                    Roads[West[0], West[1]].ConnectToRoad("E", Roads[cellNumber[0], cellNumber[1]]);

               
                    //if connection allowed, connect lanes
                    Roads[cellNumber[0], cellNumber[1]].ConnectLanesTo("W", Roads[West[0], West[1]]);

                }
               

            }

        }
    }
}
