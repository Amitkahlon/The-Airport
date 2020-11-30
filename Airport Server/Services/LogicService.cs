using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_Common.Models;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Simulator;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public class LogicService
    {
        private readonly AirportDataService dataService;
        private IPlaneMaker planeMaker;
        public IAirport Airport { get; private set; }
        internal event LogicStationEvent ChangeInStateEvent;
        public LogicService(AirportDataService dataService)
        {
            this.dataService = dataService;

            Airport airport = SetAirport();

            airport.ChangeInState += RaiseChangeInStateEvent;
            ChangeInStateEvent += UpdateDatabase;

            this.planeMaker = new PlaneMaker(airport);
            this.Airport = airport;

            InitTimer();

            Airport SetAirport()
            {
                return new Airport(builder =>
                {
                    builder.AddDefualtStations();
                    builder.AddDefualtRoute();
                }, "Ben Gurion");
            }

            void InitTimer()
            {
                this.planeMaker.ConfigureTimer(TimeSpan.FromSeconds(5));
                this.planeMaker.StartTimer();
            }
        }

        private void RaiseChangeInStateEvent(object sender, LogicStationChangedEventArgs args)
        {
            ChangeInStateEvent?.Invoke(sender, args);
        }

        private void UpdateDatabase(object sender, LogicStationChangedEventArgs args)
        {
            Station station = (Station)sender;

            //todo: update server
        }

        public IEnumerable<Station> GetStations()
        {
            return Airport.GetStations();
        }
    }
}
