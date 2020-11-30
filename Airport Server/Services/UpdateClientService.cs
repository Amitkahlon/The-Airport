using Airport_Common.Models;
using Airport_Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public class UpdateClientService
    {
        private readonly LogicService logicService;
        private readonly IHubContext<AirportHub> hubContext;

        public UpdateClientService(LogicService logicService, IHubContext<AirportHub> hubContext)
        {
            this.logicService = logicService;
            this.hubContext = hubContext;

            logicService.ChangeInStateEvent += ChangeInStateEventHandler;
        }

        private async void ChangeInStateEventHandler(object sender, LogicStationChangedEventArgs args)
        {
            string airportName = this.logicService.Airport.Name;
            IEnumerable<Station> stations = this.logicService.Airport.GetStations();

            AirportStatus airportStatus = new AirportStatus(stations, airportName);

            IEnumerable<AirportStatus> airports = new List<AirportStatus>()
            {
                airportStatus,
                airportStatus,
                airportStatus,
            };

            string jsonAirpots = JsonConvert.SerializeObject(airports);

            await hubContext.Clients.All.SendAsync("ReceiveAirports", jsonAirpots);
          
        }
    }
}
