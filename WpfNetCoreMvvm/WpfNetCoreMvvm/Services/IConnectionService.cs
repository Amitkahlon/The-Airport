

using Airport_Common.Models;
using System;
using System.Collections.Generic;

namespace WpfNetCoreMvvm.Services
{
    public interface IConnectionService
    {
        event EventHandler<IEnumerable<AirportStatus>> ReceiveAirports;
    }
}