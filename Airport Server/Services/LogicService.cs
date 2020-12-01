using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_Common.Models;
using Airport_Common.Routes;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Simulator;
using Plane_Maker.Services;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public class LogicService
    {
        private readonly AirportDataService dataService;
        private readonly IPlaneMaker benGurionPlaneMaker;
        private readonly IPlaneMaker ovdaPlaneMaker;

        public IAirport BenGurionAirport { get; private set; }
        public IAirport OvdaAirport { get; private set; }

        internal event LogicStationEvent ChangeInStateEvent;
        public LogicService(AirportDataService dataService)
        {
            this.dataService = dataService;
            ChangeInStateEvent += UpdateDatabase;

            //create OvdaRoutes
            Route[] ovdaRoutes = CreateOvdaRoutes();

            //create airports
            Airport benGurionAirport = SetBenGurionAirport();
            Airport ovdaAirport = SetOvdaAirport(ovdaRoutes);

            //subscribe to changes in the airport
            benGurionAirport.ChangeInState += RaiseChangeInStateEvent;
            ovdaAirport.ChangeInState += RaiseChangeInStateEvent;

            //assign plane maker to airports
            this.benGurionPlaneMaker = new PlaneMaker(benGurionAirport);
            this.ovdaPlaneMaker = new OvdaPlaneMaker(ovdaAirport, ovdaRoutes);

            //assign to public properties
            this.BenGurionAirport = benGurionAirport;
            this.OvdaAirport = ovdaAirport;

            //init timers
            InitBenGurionTimer();
            InitOvdaTimer();


            Airport SetBenGurionAirport()
            {
                return new Airport(builder =>
                {
                    builder.AddStation("Passangers Entrance 1", TimeSpan.FromSeconds(10));
                    builder.AddStation("Passangers Entrance 2", TimeSpan.FromSeconds(10));
                    builder.AddStation("Saftey Checks 1", TimeSpan.FromSeconds(10));
                    builder.AddStation("Saftey Checks 2", TimeSpan.FromSeconds(10));
                    builder.AddStation("Luggage 1", TimeSpan.FromSeconds(30));
                    builder.AddStation("Luggage 2", TimeSpan.FromSeconds(30));
                    builder.AddStation("Luggage 3", TimeSpan.FromSeconds(30));
                    builder.AddStation("Runway", TimeSpan.FromSeconds(15));

                    builder.AddRoute(new LandingRoute());
                    builder.AddRoute(new TakeOffRoute());

                }, "Ben Gurion", "https://www.gannett-cdn.com/-mm-/9e1f6e2ee20f44aa1f3be4f71e9f3e52b6ae2c7e/c=0-110-2121-1303/local/-/media/2020/04/02/USATODAY/usatsports/airport-airplanes-source-getty.jpg");
            }

            Airport SetOvdaAirport(Route[] routes)
            {
                return new Airport(builder =>
                {
                    builder.AddStation("Passangers Entrance 1", TimeSpan.FromSeconds(10));
                    builder.AddStation("Passangers Entrance 2", TimeSpan.FromSeconds(10));
                    builder.AddStation("Passangers Entranc 3", TimeSpan.FromSeconds(10));
                    builder.AddStation("Saftey Checks 1", TimeSpan.FromSeconds(10));
                    builder.AddStation("Saftey Checks 2", TimeSpan.FromSeconds(10));
                    builder.AddStation("Luggage 1", TimeSpan.FromSeconds(30));
                    builder.AddStation("Luggage 2", TimeSpan.FromSeconds(30));
                    builder.AddStation("Runway 1", TimeSpan.FromSeconds(15));
                    builder.AddStation("Runway 2", TimeSpan.FromSeconds(15));
                    builder.AddStation("Runway 3", TimeSpan.FromSeconds(15));

                    foreach (var route in routes)
                    {
                        builder.AddRoute(route);
                    }

                }, "Ovda", "https://www.ynet.co.il/PicServer2/24012010/2547139/2_wa.jpg");
            }

            void InitBenGurionTimer()
            {
                this.benGurionPlaneMaker.ConfigureTimer(TimeSpan.FromSeconds(5));
                this.benGurionPlaneMaker.StartTimer();
            }

            void InitOvdaTimer()
            {
                this.ovdaPlaneMaker.ConfigureTimer(TimeSpan.FromSeconds(10));
                this.ovdaPlaneMaker.StartTimer();
            }
        }

        private Route[] CreateOvdaRoutes()
        {
            int[][] landArr = new int[][]
{
                    new int[] { 10, 9, 8 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 3, 2, 1 },
                    new int[] { 3, 2, 1 },
                    new int[] { 5, 4 },
                    new int[] { 5, 4 },
                    new int[] { 7, 6 },
                    new int[] { 7, 6 },
                    new int[] { 7, 6 },
};
            Route landing = new Route(landArr, "Landing");
            int[][] takeoffArr = new int[][]
{
                    new int[] { 1, 2, 3 },
                    new int[] { 6, 7 },
                    new int[] { 6, 7 },
                    new int[] { 6, 7 },
                    new int[] { },
                    new int[] { },
                    new int[] { 8, 9, 10 },
                    new int[] { 8, 9, 10 },
                    new int[] { 0 },
                    new int[] { 0 },
                    new int[] { 0 },
};
            Route takeoff = new Route(takeoffArr, "TakeOff");

            return new Route[] { landing, takeoff };
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
            return BenGurionAirport.GetStations();
        }
    }
}
