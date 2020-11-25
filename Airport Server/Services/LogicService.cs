using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Simulator;

namespace Airport_Server.Services
{
    public class LogicService
    {
        private readonly AirportDataService dataService;
        private readonly IPlaneMaker planeMaker;
        private Airport airport;

        public LogicService(AirportDataService dataService)
        {
            this.dataService = dataService;

            SetAirport();

            this.planeMaker = new PlaneMaker(airport);
        }

        private void SetAirport()
        {
            airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            });
        }


    }
}
