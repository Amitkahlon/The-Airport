using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportClient.Models
{
    public class AirportViewModel
    {
        public int AiportNumber{ get; set; }
        public string AirportName { get; set; }
        public List<Station> Stations { get; set; }

        public AirportViewModel(AirportStatus airportStatus)
        {
            AirportName = airportStatus.Name;
            Stations = airportStatus.Stations.ToList();
        }

        public AirportViewModel() { }
    }
}
