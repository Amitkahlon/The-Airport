using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Airport_Logic.Services
{
    public class StationService
    {
        //private static StationService singletonService;
        private List<LogicStation> stations = new List<LogicStation>();

        public void CreateStation(int stationNum, string stationName, TimeSpan timeSpan)
        {
            if(stationNum == 0)
            {
                throw new ArgumentException("Station number cannot be 0");
            }

            if (stations.Any(s => s.StationNumber == stationNum))
            {
                throw new ArgumentException("Station number must be unique");
            }

            stations.Add(new LogicStation()
            {
                StationNumber = stationNum,
                StationName = stationName,
                WaitingTime = timeSpan,
            });
        }
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

        //public static StationService GetService()
        //{
        //    if (singletonService == null)
        //    {
        //        singletonService = new StationService();
        //    }
        //    return singletonService;
        //}

        public StationService()
        {

        }
    }
}
