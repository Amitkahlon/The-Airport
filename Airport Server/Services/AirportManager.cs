using Airport_Logic;
using Airport_Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport_Server.Services
{
    public class AirportManager
    {
        private readonly List<Airport>  airports;

        public AirportManager()
        {
            this.airports = new List<Airport>();
        }

        public void AddAirport(Airport airport)
        {
            this.airports.Add(airport);
        }

        public Airport GetAirport(int id)
        {
            return this.airports.FirstOrDefault(a => a.Id == id);
        }

        public Airport GetAirport(string name)
        {
            return this.airports.FirstOrDefault(a => a.Name == name);
        }

        public IEnumerable<Airport> GetAirports()
        {
            return airports;
        }

        public void AddAirports(IEnumerable<Airport> airports)
        {
            foreach (var airport in airports)
            {
                AddAirport(airport);
            }
        }
        
    }


}
