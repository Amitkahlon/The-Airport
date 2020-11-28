using Airport_Common.Models;
using Airport_DAL.Services;
using Airport_Server.Services;
using Airport_Simulator;
using Microsoft.AspNetCore.Builder;
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
        private readonly LogicService logicService;

        public AirportHub(LogicService logicService)
        {
            this.logicService = logicService;
            logicService.ChangeInStateEvent += AirportChangeEvent;
        }

        public async Task SendAirportsStatus()
        {
            AirportStatus airportStatus = new AirportStatus(logicService.GetStations());
            IEnumerable<AirportStatus> airports = new List<AirportStatus>()
            {
                airportStatus
            };
            await Clients.All.SendAsync("ReceiveAirports", airports);
        }

        private async void AirportChangeEvent(object sender, LogicStationChangedEventArgs args)
        {
            //todo: add to logs.
            //todo: send to ui

            await SendAirportsStatus();


        }


    }
}
