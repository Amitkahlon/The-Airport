using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class Plane
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Color { get; set; }
        public string AirplaneType { get; set; }
        public int PassangerCount { get; set; }
        public string Country { get; set; }
        public string Route{ get; set; }
    }
}
