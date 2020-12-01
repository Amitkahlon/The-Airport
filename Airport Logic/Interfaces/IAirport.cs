using Airport_Common.Models;
using System.Collections.Generic;
using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic
{
    public interface IAirport
    {
        string Name { get; }
        string ImageUrl { get; }
        IEnumerable<Station> GetStations();
        event LogicStationEvent ChangeInState;
    }
}