using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Models
{
    public class AirportStatus
    {
        public IEnumerable<Station> Stations { get; set; }
        public string Name { get; set; }
        public string ImageUrl{ get; set; }

        public AirportStatus() { }

        public AirportStatus(IEnumerable<Station> stations, string name)
        {
            Stations = stations;
            this.Name = name;
        }
        public AirportStatus(IEnumerable<Station> stations, string name, string imageUrl) : this(stations,name)
        {
            this.ImageUrl = imageUrl;
        }
    }
}
