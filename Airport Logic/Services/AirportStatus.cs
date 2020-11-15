using Airport_Logic.Logic_Interfaces;
using Airport_Logic.Logic_Models;
using Airport_Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airport_Logic
{
    public class AirportStatus
    {
        private StationService stationService;
        public Dictionary<string, List<LogicStation>> EntryPoints { get; set; }


        public AirportStatus(Action<AirportBuilder> builderFunction)
        {
            stationService = new StationService();
            EntryPoints = new Dictionary<string, List<LogicStation>>();

            AirportBuilder airportBuilder = new AirportBuilder(stationService, EntryPoints);
            builderFunction.Invoke(airportBuilder);
        }
    }

    public class AirportBuilder
    {
        private StationService stationService;
        private Dictionary<string, List<LogicStation>> entryPoints;
        public AirportBuilder(StationService stationService, Dictionary<string, List<LogicStation>> entryPoints)
        {
            this.stationService = stationService;
            this.entryPoints = entryPoints;
        }
        public void AddStation(int stationNum, string stationName, TimeSpan waitingTime)
        {
            stationService.CreateStation(stationNum, stationName, waitingTime);
        }
        public void RegisterRoute(IRoute route, string routeName)
        {
            try
            {
                entryPoints.Add(routeName, new List<LogicStation>());
            }
            catch (Exception)
            {
                throw new ArgumentException("routeName cannot be null and must be unique");
            }

            int stationNum = 0; //entry point
            bool isItarates = true;
            while (isItarates)
            {
                IEnumerable<int> avaliableRoutenNums = route.GetNextAvailableRoute(stationNum);

                if (avaliableRoutenNums.Any(stationNums => stationNums == -1))
                {
                    isItarates = false;
                }
                else if (avaliableRoutenNums.Any())
                {
                    LogicStation currentStation = null;
                    if (stationNum != 0)
                    {
                        currentStation = stationService.GetStation(stationNum);
                    }

                    foreach (var stationId in avaliableRoutenNums)
                    {
                        if (stationId == 0)
                        {
                            break;
                        }
                        else
                        {
                            var stationToAdd = stationService.GetStation(stationId);
                            if (stationNum == 0) //Entry Point
                            {
                                entryPoints[routeName].Add(stationToAdd);
                            }
                            else
                            {
                                currentStation.AddStation(stationToAdd);
                            }
                        }
                    }
                    stationNum++;
                }
            }
        }
    }
}
