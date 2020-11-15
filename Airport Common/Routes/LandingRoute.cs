using Airport_Common.Interfaces;
using System.Collections.Generic;

namespace Airport_Common.Routes
{
    public class LandingRoute : IRoute
    {
        public IEnumerable<int> GetNextAvailableRoute(int stationNumber)
        {
            switch (stationNumber)
            {
                case 0:
                    yield return 8;
                    break;
                case 8:
                    yield return 7;
                    yield return 6;
                    yield return 5;
                    break;
                case 7: case 6: case 5:
                    yield return 4;
                    yield return 3;
                    break;
                case 4: case 3:
                    yield return 2;
                    yield return 1;
                    break;
                case 2: case 1:
                    yield return 0;
                    break;
                default:
                    yield return -1;
                    break;
            }
        }
    }
}
