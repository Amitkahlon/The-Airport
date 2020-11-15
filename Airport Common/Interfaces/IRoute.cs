using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Common.Interfaces
{
    public interface IRoute
    {
        /// <summary>
        /// Returns a IEnumarable of numbers which represent the next avaliable station
        /// </summary>
        /// <param name="stationNumber">The station we are checking whats the next avaliable stations</param>
        /// <returns>List of next avaliable station numbers, case 0 == no next station</returns>
        IEnumerable<int> GetNextAvailableRoute(int stationNumber);

    }
}
