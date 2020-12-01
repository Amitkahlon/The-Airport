using Airport_Common.Models;
using AirportClient.Views;
using ChatClient.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportClient.ViewModels
{
    public class PlaneViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;

        public Plane Plane { get; set; }
        public Station Station { get; set; }
        public AirportStatus Airport { get; set; }
        public RelayCommand GoBackCommand { get; set; }

        public PlaneViewModel(NavigationService navigationService)
        {
            this.GoBackCommand = new RelayCommand(GoBack);
            this.navigationService = navigationService;
        }

        private void GoBack()
        {
            this.navigationService.Navigate(new StationView());
        }

        public void SetProperties(Plane plane, Station station, AirportStatus airport)
        {
            this.Plane = plane;
            this.Station = station;
            this.Airport = airport;
        }
    }
}
