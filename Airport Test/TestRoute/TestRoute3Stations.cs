using Airport_Common.Interfaces;
using Airport_Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Test.TestRoute
{
    internal class TestRoute3Stations : Route
    {
        public new string Name => "ThreeStationsTestRoute";

        public new IEnumerable<int> GetNextAvailableRoute(int stationNumber)
        {
            switch (stationNumber)
            {
                case 0:
                    yield return 1;
                    break;
                case 1:
                    yield return 2;
                    break;
                case 2:
                    yield return 3;
                    break;
                case 3:
                    yield return 0;
                    break;
                default:
                    yield return -1;
                    break;
            }
        }
    }
}
