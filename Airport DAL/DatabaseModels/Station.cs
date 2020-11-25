using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class Station
    {
        [Key]
        public int Id{ get; set; }
        public Airport Airport { get; set; }
        public int StationNumber{ get; set; }
        public string StationName { get; set; }
        public TimeSpan WaitingTime { get; set; }
    }
}
