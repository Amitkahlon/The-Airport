using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class AirportStatus
    {
        public IEnumerable<Station> Stations { get; private set; }

        public AirportStatus() { }

        public AirportStatus(IEnumerable<Station> stations)
        {
            Stations = stations;
        }
    }
}
