using Airport_Common.Models;
using Airport_Logic;
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
            IEnumerable<AirportStatus> airports = GetAirportList();

            string jsonAirports = JsonConvert.SerializeObject(airports, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            await hubContext.Clients.All.SendAsync("ReceiveAirports", jsonAirports);
        }

        private IEnumerable<AirportStatus> GetAirportList()
        {
            AirportStatus benGurion = CreateAirportStatus(this.logicService.BenGurionAirport);
            AirportStatus ovda = CreateAirportStatus(this.logicService.OvdaAirport);

            yield return benGurion;
            yield return ovda;
        }

        private AirportStatus CreateAirportStatus(IAirport airport)
        {
            IEnumerable<Station> stations = airport.GetStations();
            string airportName = airport.Name;
            string imageUrl = airport.ImageUrl;

            return new AirportStatus(stations, airportName, imageUrl);
        }
    }
}
