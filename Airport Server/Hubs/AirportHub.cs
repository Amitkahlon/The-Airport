using Airport_Common.Models;
using Airport_DAL.Services;
using Airport_Logic;
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
            logicService.ChangeInStateEvent += UpdateUiOnChange;
        }

        public async Task SendMessageAll()
        {
            await Clients.All.SendAsync("UpdateState");
        }

        private void UpdateUiOnChange(object sender, LogicStationChangedEventArgs args)
        {
            //todo: add to logs.
        }


    }
}
