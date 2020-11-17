using Airport_Common.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Airport_Logic.Logic_Models
{
    public class LogicStation : Station, IEnterStation, IWaitingLine
    {
        private event Action ChangeInStateEvent;
        public ConcurrentQueue<Plane> WaitingLine { get; private set; }
        private bool isStationOccupied;
        private readonly object waitingLineLock = new object();

        public LogicStation()
        {
            WaitingLine = new ConcurrentQueue<Plane>();
            ChangeInStateEvent += PushToWait;
        }

        private void PushToWait()
        {
            Task.Run(() =>
            {
                if (!WaitingLine.IsEmpty && !isStationOccupied)
                {
                    WaitingLine.TryDequeue(out Plane plane);

                    Wait(plane);
                }
            });
        }

        public void EnterStation(Plane plane)
        {
                WaitingLine.Enqueue(plane);
                ChangeInStateEvent?.Invoke();
        }

        private void Wait(Plane plane)
        {
            isStationOccupied = true;
            lock (waitingLineLock)
            {
                base.CurrentPlane = plane;
                Thread.Sleep(base.WaitingTime);
                base.CurrentPlane = null;
            }
            isStationOccupied = false;
            ChangeInStateEvent?.Invoke();

            LogicStation nextStation = GetBestStation(plane);
            if (nextStation != null)
            {
                nextStation.EnterStation(plane);
            }
        }

        /// <summary>
        /// Get the least busy station for a plane(depends on the route)
        /// </summary>
        /// <param name="plane">plane to choose the next route for</param>
        /// <returns>least busy station for the plane</returns>
        private LogicStation GetBestStation(IRouteable plane)
        {
            var nextStationNumbers = plane.PlaneRoute.GetNextAvailableRoute(this.StationNumber);

            if (!nextStationNumbers.Any())
            {
                throw new Exception("Could not be empty, if we reach the end we receive -1");
            }
            else if (nextStationNumbers.Any(staionNum => staionNum == -1)) //if it reached the end
            {
                return null;
            }
            else
            {
                var nextStations = GetStations(nextStationNumbers);
                return LogicStation.GetBestStation(nextStations);
            }
        }

        
        private List<LogicStation> GetStations(IEnumerable<int> stationNumbers)
        {
            List<LogicStation> NextAvaliableStations = new List<LogicStation>();
            foreach (var num in stationNumbers)
            {
                var station = GetLogicStationByNumber(num);
                NextAvaliableStations.Add(station);
            }
            return NextAvaliableStations;
        }

        /// <summary>
        /// Returns the least busy station
        /// </summary>
        /// <param name="stations">stations to pick from</param>
        /// <returns>least busy station</returns>
        internal static LogicStation GetBestStation(IEnumerable<LogicStation> stations)
        {
            return stations.OrderBy(station =>
             {
                 int count = 0;
                 if (station.isStationOccupied)
                 {
                     count++;
                 }
                 count += station.WaitingLine.Count;
                 return count;
             }).First();
        }

        /// <summary>
        /// Get a LogicStation Instance from the connected stations.
        /// </summary>
        /// <param name="stationNumber">station number</param>
        /// <returns>Instance of LogicStation</returns>
        /// <exception cref="Exception">If station number does not exist in the station connections</exception>
        public LogicStation GetLogicStationByNumber(int stationNumber)
        {
            return (LogicStation)base.ConnectedStations.First(s => s.StationNumber == stationNumber);
        }
    }

}
