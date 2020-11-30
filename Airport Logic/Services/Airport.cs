using Airport_Common.Interfaces;
using Airport_Common.Models;
using Airport_Common.Routes;
using Airport_Logic.Interfaces;
using Airport_Logic.Logic_Models;
using Airport_Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic
{
    public class Airport : IPushPlane, IRaiseChangeInStateEvent, IAirport
    {
        private readonly IStationProvider stationProvider;
        public EntryPointsManager EntryManager { get; private set; }
        public event LogicStationEvent ChangeInState;
        public string Name { get; private set; }

        public Airport(Action<AirportBuilder> builder, string airportName)
        {
            stationProvider = new StationProvider(this);
            EntryManager = new EntryPointsManager();

            AirportBuilder airportBuilder = new AirportBuilder(stationProvider, EntryManager);
            builder.Invoke(airportBuilder);

            this.Name = airportName;
        }

        public IEnumerable<Station> GetStations()
        {
            return stationProvider.GetStations();
        }

        public void PushPlane(Plane plane)
        {
            Task.Run(() =>
            {
                LogicStation.GetBestStation(EntryManager.GetEntryStations(plane.PlaneRoute.Name)).EnterStation(plane);
            });
        }

        public void RaiseChangeInStateEvent(object sender, LogicStationChangedEventArgs args)
        {
            this.ChangeInState?.Invoke(sender, args);
        }

        public class AirportBuilder
        {
            private readonly IStationProvider stationService;
            private readonly EntryPointsManager entryManager;
            internal AirportBuilder(IStationProvider stationService, EntryPointsManager entryPoints)
            {
                this.stationService = stationService;
                this.entryManager = entryPoints;
            }
            public void AddStation(string stationName, TimeSpan waitingTime)
            {
                stationService.CreateStation(stationName, waitingTime);
            }
            public void AddRoute(IRoute route)
            {
                entryManager.InitializeEntryPoint(route.Name);

                int stationNum = 0; //0 == entry point
                bool isItarates = true;
                while (isItarates)
                {
                    IEnumerable<int> avaliableRoutenNums = route.GetNextAvailableRoute(stationNum);

                    //if there is no such station in the route, stop the loop.
                    if (avaliableRoutenNums.Any(stationNums => stationNums == -1))
                    {
                        isItarates = false;
                    }
                    //if the IEnumbrable is empty, it means the station does not participate in the route
                    //and won't be manipulate by the builder
                    else if (avaliableRoutenNums.Any())
                    {
                        LogicStation currentStation = null;

                        //if not the entry point, get the station
                        if (stationNum != 0)
                        {
                            currentStation = stationService.GetStation(stationNum);
                        }

                        foreach (var stationId in avaliableRoutenNums)
                        {
                            //if it is an exist point, dont add any stations and break.
                            if (stationId == 0)
                            {
                                break;
                            }
                            else
                            {
                                LogicStation stationToAdd = stationService.GetStation(stationId);
                                //if in the entry point, add the station to the entryManager.
                                if (stationNum == 0)
                                {
                                    entryManager.AddStationToEntry(route.Name, stationToAdd);
                                }
                                //else add the station to the station connections
                                else
                                {
                                    currentStation.AddStation(stationToAdd);
                                }
                            }
                        }
                        //go to the next station
                        stationNum++;
                    }
                }
            }
            public void AddDefualtStations()
            {
                AddStation("Station1", TimeSpan.FromSeconds(15));
                AddStation("Station2", TimeSpan.FromSeconds(10));
                AddStation("Station3", TimeSpan.FromSeconds(30));
                AddStation("Station4", TimeSpan.FromSeconds(5));
                AddStation("Station5", TimeSpan.FromSeconds(10));
                AddStation("Station6", TimeSpan.FromSeconds(15));
                AddStation("Station7", TimeSpan.FromSeconds(20));
                AddStation("Station8", TimeSpan.FromSeconds(15));
            }
            public void AddDefualtRoute()
            {
                AddRoute(new LandingRoute());
            }
        }
    }
}
