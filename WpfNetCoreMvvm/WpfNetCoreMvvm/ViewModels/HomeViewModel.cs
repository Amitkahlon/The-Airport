using Airport_Common.Models;
using AirportClient.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfNetCoreMvvm.Services;

namespace AirportClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IConnectionService service;

        public ObservableCollection<AirportStatus> Airports { get; set; }


        public HomeViewModel(IConnectionService service)
        {
            this.service = service;
            this.service.ReceiveAirports += ReceiveAirportsEventHandler;
        }

        private void ReceiveAirportsEventHandler(object sender, IEnumerable<AirportStatus> AirportsArgs)
        {
            Airports = new ObservableCollection<AirportStatus>(AirportsArgs);
        }
    }
}
