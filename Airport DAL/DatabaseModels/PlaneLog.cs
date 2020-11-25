using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_DAL.DatabaseModels
{
    public class PlaneLog
    {
        public int Id{ get; set; }
        public Plane PlaneId { get; set; }
        public Station StationId { get; set; }
        public DateTime Time { get; set; }
    }
}
