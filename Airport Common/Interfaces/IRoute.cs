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
        /// if 0 is returned, it means you reached the end of the route
        /// </summary>
        /// <param name="stationNumber">The station we are checking whats the next avaliable stations</param>
        /// <returns>List of next avaliable station numbers, 0 == end of route</returns>
        IEnumerable<int> GetNextAvailableRoute(int stationNumber);

        /// <summary>
        /// Name of the route
        /// </summary>
        string Name { get; }

    }
}
