using static Airport_Logic.Logic_Models.LogicStation;

namespace Airport_Logic
{
    internal interface IRaiseChangeInStateEvent
    {
        void RaiseChangeInStateEvent(object sender, LogicStationChangedEventArgs args);
    }
}