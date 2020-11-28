using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Airport_Common.Models;
using Airport_Logic.Interfaces;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic.Services
{
    internal class StationProvider : IStationProvider
    {
        public event LogicStationEvent ChangeInStateEvent;
        private readonly List<LogicStation> stations = new List<LogicStation>();
        private int stationCount = 0;

        public void CreateStation(string stationName, TimeSpan timeSpan)
        {
            stationCount++;
            if (stationCount == 0)
            {
                throw new ArgumentException("Station number cannot be 0");
            }

            if (stations.Any(s => s.StationNumber == stationCount))
            {
                throw new ArgumentException("Station number must be unique");
            }

            LogicStation station = new LogicStation()
            {
                StationNumber = stationCount,
                StationName = stationName,
                WaitingTime = timeSpan,
            };

            station.ChangeInStateEvent += ChangeInStateEvent;
            //station.ChangeInStateEvent += TestEvent;

            stations.Add(station);
        }

        //private void TestEvent(object sender, LogicStationChangedEventArgs args)
        //{
        //    throw new NotImplementedException();
        //}

        public LogicStation GetStation(int stationNum)
        {
            if (stationNum == 0)
            {
                throw new ArgumentException("station Number cannot be 0");
            }
            try
            {
                return stations.First(s => s.StationNumber == stationNum);
            }
            catch (Exception)
            {
                throw new ArgumentException("Station does not exist");
            }
        }

        public IEnumerable<LogicStation> GetStations()
        {
            return stations;
        }
    }
}
