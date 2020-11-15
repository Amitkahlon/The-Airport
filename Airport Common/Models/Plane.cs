using Airport_Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Airport_Common.Models
{
    public class Plane
    {
        public string FlightNumber { get; set; }
        public Color Color { get; set; }
        public string AirplaneType{ get; set; }
        public int PassangersCount { get; set; }


        //logic 
        public IRoute PlaneRoute { get; set; }
    }
}
