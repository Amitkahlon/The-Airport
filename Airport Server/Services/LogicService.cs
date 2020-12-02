using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport_Common.Interfaces;
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
        protected IPlaneMaker benGurionPlaneMaker;
        protected IPlaneMaker OvdaPlaneMaker;

        public IAirport BenGurionAirport { get; private set; }
        public IAirport OvdaAirport { get; private set; }

        internal event LogicStationEvent ChangeInStateEvent;
        public LogicService(AirportDataService dataService)
        {
            this.dataService = dataService;
            ChangeInStateEvent += UpdateDatabase;

            LogicServiceBuilder builder = new LogicServiceBuilder(this);
            builder.InitializeService();
            builder.AssignEvents(RaiseChangeInStateEvent);
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


        class LogicServiceBuilder
        {
            private LogicService logicService;

            public LogicServiceBuilder(LogicService logicService)
            {
                this.logicService = logicService;
            }

            public void InitializeService()
            {
                Route[] ovdaRoutes = GetOvdaRoutes();

                Airport benGurion = SetBenGurionAirport();
                Airport ovda = SetOvdaAirport(ovdaRoutes);

                SetAndAssignPlaneMakers(benGurion, ovda, ovdaRoutes);

                AssignAirportsToProps(benGurion, ovda);

                InitTimers(this.logicService.benGurionPlaneMaker, this.logicService.OvdaPlaneMaker);
            }

            private Route[] GetOvdaRoutes()
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

            private void SetAndAssignPlaneMakers(IPushPlane benGurion, IPushPlane ovda, params Route[] ovdaRoutes)
            {
                this.logicService.benGurionPlaneMaker = new PlaneMaker(benGurion);
                this.logicService.OvdaPlaneMaker = new OvdaPlaneMaker(ovda, ovdaRoutes);
            }

            public void AssignEvents(LogicStationEvent eventHandler)
            {
                this.logicService.BenGurionAirport.ChangeInState += eventHandler;
                this.logicService.OvdaAirport.ChangeInState += eventHandler;
            }

            private void InitTimers(IPlaneMaker benGurionTimer, IPlaneMaker ovdaTimer)
            {
                InitBenGurionTimer();
                InitOvdaTimer();

                void InitBenGurionTimer()
                {
                    benGurionTimer.ConfigureTimer(TimeSpan.FromSeconds(5));
                    benGurionTimer.StartTimer();
                }

                void InitOvdaTimer()
                {
                    ovdaTimer.ConfigureTimer(TimeSpan.FromSeconds(10));
                    ovdaTimer.StartTimer();
                }
            }

            private Airport SetBenGurionAirport()
            {
                const string imgUrl = "https://www.gannett-cdn.com/-mm-/9e1f6e2ee20f44aa1f3be4f71e9f3e52b6ae2c7e/c=0-110-2121-1303/local/-/media/2020/04/02/USATODAY/usatsports/airport-airplanes-source-getty.jpg";
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

                }, "Ben Gurion", imgUrl);
            }

            private Airport SetOvdaAirport(Route[] routes)
            {
                const string imgUrl = "https://www.ynet.co.il/PicServer2/24012010/2547139/2_wa.jpg";
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

                }, "Ovda", imgUrl);
            }
            private void AssignAirportsToProps(Airport benGurion, Airport ovda)
            {
                this.logicService.BenGurionAirport = benGurion;
                this.logicService.OvdaAirport = ovda;
            }
        }
    }


}
