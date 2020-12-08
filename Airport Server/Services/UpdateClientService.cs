using Airport_Common.Models;
using Airport_Logic;
using Airport_Server.Converter;
using Airport_Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public class UpdateClientService
    {
        private readonly LogicService logicService;
        private readonly IHubContext<AirportHub> hubContext;
        private readonly ConverterProvider converterProvider;

        public UpdateClientService(LogicService logicService, IHubContext<AirportHub> hubContext, ConverterProvider converterProvider)
        {
            this.logicService = logicService;
            logicService.ChangeInStateEvent += ChangeInStateEventHandler;

            this.hubContext = hubContext;
            this.converterProvider = converterProvider;

            this.logicService.LoadFromDb();
            this.logicService.AssignPlaneMakers();
        }


        public async void ChangeInStateEventHandler(object sender, LogicStationChangedEventArgs args)
        {
            IEnumerable<AirportStatus> airports = this.converterProvider.LogicCommon
                .ConvertAirports(this.logicService.GetAirports());

            string jsonAirports = JsonConvert.SerializeObject(airports, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            await hubContext.Clients.All.SendAsync("ReceiveAirports", jsonAirports);
        }
    }
}
