using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace tracy
{
    public abstract class Road
    {
        protected Image image;
        protected Point coordinates;
        protected int nrOfNeighbours;
        protected List<Lane> lanes;
        protected Road[] connectedRoads;
        protected Timer time = new Timer(); 
        protected Timer TrafTime = new Timer();
        protected Graphics Gra;


        public Graphics graphic
        {
            get
            {
                return Gra;
            }
            set
            {
                Gra = value;
            }

        }
        public Point Coordinates
        {
            get
            {
                return coordinates;
            }
            set
            {
                coordinates = value;
            }
        }

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                Image = value;
            }
        }

        public Timer Time
        {
            get
            {
                return this.time; 
            }
        }

        public int NrOfNeighbours
        {
            get
            {
                return nrOfNeighbours;
            }
            set
            {
                nrOfNeighbours = value;
            }
        }

        /// <summary>
        /// Connecting the calling road to its neighbours "other" 
        /// </summary>
        /// <param name="side">The position of the neighbour, using 4 cardinal points (NESW)</param>
        /// <param name="other">The road object to be connected to</param>
        /// <returns>True if allowed to be connected, false otherwise</returns>
        public abstract bool ConnectToRoad(string side, Road other);


        /// <summary>
        /// Draw the calling road in the Graphics gr
        /// </summary>
        /// <param name="gr">The graphics of the grid</param>
        public abstract void Draw(ref Graphics gr);
        

        /// <summary>
        /// Connects the lanes of the calling road to the corresponding lanes of its neighbours
        /// </summary>
        /// <param name="side">The position of neighbour</param>
        /// <param name="other">The road object it is connected to</param>
        public abstract void ConnectLanesTo(string side, Road other);

        /// <summary>
        /// Returns the list of lanes of the calling road
        /// </summary>
        /// <returns>The list of lanes</returns>
        public List<Lane> getListOfLane()
        {
            return this.lanes;
        }

        /// <summary>
        /// Change the spawn time of spawnable lane to specified time
        /// </summary>
        /// <param name="time">The interval for which the car is spawned</param>
        public abstract void AdjustSpawnTime(int time);
        /// <summary>
        /// Disconnect this road from the grid, disconnect itself from neighbour along with all of its lanes
        /// </summary>
        public abstract void DisconnectRoad();
        /// Disconnect the neighbour road r from the list of neighbours
        /// </summary>
        /// <param name="r">the neighbour road object to be deleted</param>
        public abstract void DisconnectNeighbour(string side);

        public abstract string getDirection();

        /// <summary>
        /// Change the spawnability of all lanes in this road located in the side 'side'
        /// </summary>
        /// <param name="side">The side where the spawnability of the lanes are going to be changed</param>
        /// <param name="spawnAble">The new spawnable status of the lanes on that side</param>
        public abstract void changeSpawnability(string side, bool spawnAble);

        public abstract void RemoveLanes();

        public virtual void disableTimer()
        {
            time.Enabled = false;
            TrafTime.Enabled = false;
            time.Stop();
            TrafTime.Stop();
        }
        public virtual void startTimer()
        {
            time.Enabled = true;
            TrafTime.Enabled = true;
            time.Start();
            TrafTime.Start();
        }

        public virtual void DrawCars(ref Graphics g)
        {
            for (int i = 0; i < this.lanes.Count; i++)
            {
                this.lanes[i].drawCars(ref g);
            }
        }

        public virtual void DrawTrafficLight(ref Graphics g)
        {
            return;
        }

    }
}
