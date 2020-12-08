using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Airport_DAL.Services;
using Airport_Logic;
using Airport_Server.Converter;
using Airport_Simulator;
using Plane_Maker.Services;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Server.Services
{
    public class LogicService
    {
        private readonly AirportDataService dataService;
        private readonly AirportManager airportManager;
        private readonly ConverterProvider converterProvider;

        internal event LogicStationEvent ChangeInStateEvent;
        public LogicService(AirportDataService dataService, ConverterProvider converterProvider,
            bool createAirports = false)
        {
            this.airportManager = new AirportManager();
            this.dataService = dataService;
            this.converterProvider = converterProvider;

            if (createAirports)
            {
                CreateAirports();
            }

            ChangeInStateEvent += UpdateDatabase;
        }

        public virtual void AssignPlaneMakers()
        {
            Airport benGurion = this.airportManager.GetAirport("Ben Gurion");
            benGurion.ChangeInState += RaiseChangeInStateEvent;
            Airport ovda = this.airportManager.GetAirport("Ovda");
            ovda.ChangeInState += RaiseChangeInStateEvent;

            IdManager.Id = dataService.GetIdCount();

            IPlaneMaker planeMaker = new PlaneMaker(benGurion);
            planeMaker.ConfigureTimer(TimeSpan.FromSeconds(5));
            planeMaker.StartTimer();

            IPlaneMaker ovdaPlaneMaker = new OvdaPlaneMaker(ovda, ovda.Routes.ToArray());
            ovdaPlaneMaker.ConfigureTimer(TimeSpan.FromSeconds(10));
            ovdaPlaneMaker.StartTimer();
        }

        public IEnumerable<Airport> GetAirports()
        {
            return this.airportManager.GetAirports();
        }

        public void LoadFromDb()
        {
            var db = dataService.GetAirports();
            
            IEnumerable<Airport> airports = this.converterProvider.LogicDatabase.ConvertAirports(dataService.GetAirports());
            this.airportManager.AddAirports(airports);

            this.ChangeInStateEvent(null, null);
        }

        public virtual async void CreateAirports()
        {
            AirportBuilder builder = new AirportBuilder();

            Airport benGurion = builder.BuildBenGurionAirport();
            Airport ovda = builder.BuildOvdaAirport();

            await AddAiportToDatabaseAsync(benGurion);
            await AddAiportToDatabaseAsync(ovda);
        }

        private async Task AddAiportToDatabaseAsync(IAirport airport)
        {
            DbAirport dbAirport = this.converterProvider.LogicDatabase.ConvertAirport(airport);
            await this.dataService.AddAirport(dbAirport);
        }

        private void RaiseChangeInStateEvent(object sender, LogicStationChangedEventArgs args)
        {
            ChangeInStateEvent?.Invoke(sender, args);
        }

        private void UpdateDatabase(object sender, LogicStationChangedEventArgs args)
        {
            if (sender != null && args != null)
            {
                Station station = (Station)sender;
                DbStation dbStation = this.converterProvider.CommonToDb.ConvertStation(station);

                this.dataService.UpdateStation(dbStation);
            }
        }
    }
}
