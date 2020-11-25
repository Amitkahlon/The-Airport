using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class Airport
    {
        [Key]
        public int Id{ get; set; }
        public string AirportName { get; set; }
        public ICollection<Station> Stations{ get; set; }
    }
}
