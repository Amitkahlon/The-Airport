using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class Station
    {
        public int StationNumber { get; set; }
        public string StationName { get; set; }
        public Plane CurrentPlane { get; set; }
        public List<Station> ConnectedStations { get; protected set; }

        public Station()
        {
            ConnectedStations = new List<Station>();
        }

        //logic

        public TimeSpan WaitingTime { get; internal set; }

        internal void AddStation(Station station)
        {
            if (!ConnectedStations.Contains(station))
            {
                ConnectedStations.Add(station);
            }
        }
    }
}
