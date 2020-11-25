using Airport_DAL.Context;
using Airport_DAL.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_DAL.Services
{
    public class AirportDataService
    {
        private readonly DataContext context;
        public AirportDataService()
        {
            context = new DataContext();
        }

        public async void AddAirport(Airport airport)
        {
            context.Airports.Add(airport);
            await context.SaveChangesAsync();
        }
    }
}
