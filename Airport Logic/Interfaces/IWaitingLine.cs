using Airport_Common.Models;
using System.Collections.Concurrent;

namespace Airport_Logic.Logic_Models
{
    internal interface IWaitingLine
    {
        ConcurrentQueue<Plane> WaitingLine { get; }
    }
}