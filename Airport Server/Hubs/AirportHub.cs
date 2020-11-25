using Airport_Common.Models;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Simulator;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Hubs
{
    public class AirportHub : Hub
    {
        private Airport airport;
        private IPlaneMaker planeMaker;
        private readonly AirportDataService airportDataService;

        public AirportHub(AirportDataService airportDataService)
        {
            this.airportDataService = airportDataService;

            airport.ChangeInStateEvent += Airport_ChangeInStateEvent;

            InitAirport();
            InitTimer();
            
            
            void InitAirport()
            {
                airport = new Airport(builder =>
                {
                    builder.AddDefualtStations();
                    builder.AddDefualtRoute();
                });
            }
            
            void InitTimer()
            {
                this.planeMaker = new PlaneMaker(airport);
                this.planeMaker.ConfigureTimer(TimeSpan.FromSeconds(5));
                this.planeMaker.StartTimer();
            }
        }

        private void Airport_ChangeInStateEvent(object sender, LogicStationChangedEventArgs args)
        {
            Station station = (Station)sender;
            //todo: add to logs.
        }
    }
}
