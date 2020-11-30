using Airport_Common.Models;
using AirportClient.Models;
using ChatClient.Services;
using ChatClient.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        public NavigationService NavigationService { get; private set; }

        private ObservableCollection<AirportStatus> airports;
        public ObservableCollection<AirportStatus> Airports { get => airports; set { Set(ref airports, value); } }
        public AirportStatus SelectedAirport { get; set; }
        public RelayCommand ViewDetailsCommand { get; }


        public HomeViewModel(IConnectionService service, NavigationService navigationService)
        {
            this.service = service;
            this.NavigationService = navigationService;
            this.service.ReceiveAirports += ReceiveAirportsEventHandler;
            this.ViewDetailsCommand = new RelayCommand(ViewDetails);
        }

        private void ViewDetails()
        {
            NavigationService.ChangeContent(new AirportView());
        }

        private void ReceiveAirportsEventHandler(object sender, IEnumerable<AirportStatus> AirportsArgs)
        {
            Airports = new ObservableCollection<AirportStatus>(AirportsArgs);
        }
    }
}
