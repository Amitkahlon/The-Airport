using Airport_Common.Models;
using ChatClient.Services;
using ChatClient.ViewModels;
using ChatClient.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfNetCoreMvvm.Services;

namespace AirportClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IConnectionService service;

        public NavigationService NavigationService { get; private set; }
        public AirportViewModel AirportViewModel { get; }

        private ObservableCollection<AirportStatus> airports;
        public ObservableCollection<AirportStatus> Airports { get => airports; set { Set(ref airports, value); } }
        public RelayCommand<AirportStatus> ViewDetailsCommand { get; }


        public HomeViewModel(IConnectionService service, NavigationService navigationService, AirportViewModel airportViewModel)
        {
            this.service = service;
            this.NavigationService = navigationService;
            AirportViewModel = airportViewModel;
            this.service.ReceiveAirports += ReceiveAirportsEventHandler;
            this.ViewDetailsCommand = new RelayCommand<AirportStatus>(airport => ViewDetails(airport));
        }

        private void ViewDetails(AirportStatus selectedAirport)
        {
            NavigationService.ChangeContent(new AirportView());
            AirportViewModel.Airport = selectedAirport;
        }

        private void ReceiveAirportsEventHandler(object sender, IEnumerable<AirportStatus> AirportsArgs)
        {
            Airports = new ObservableCollection<AirportStatus>(AirportsArgs);
            if(this.AirportViewModel.Airport != null)
            {
                this.AirportViewModel.Airport = AirportsArgs.FirstOrDefault(airport => airport.Name == this.AirportViewModel.Airport.Name);
            }
        }
    }
}
