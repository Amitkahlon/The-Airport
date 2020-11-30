using Airport_Logic.Logic_Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic.Interfaces
{
    internal interface IStationProvider
    {
        void CreateStation(string stationName, TimeSpan timeSpan);
        LogicStation GetStation(int stationNum);
        IEnumerable<LogicStation> GetStations();
    }
}
