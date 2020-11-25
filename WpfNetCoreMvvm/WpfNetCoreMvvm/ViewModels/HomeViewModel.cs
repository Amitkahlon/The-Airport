using Airport_Common.Models;
using AirportClient.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AirportClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<AirportViewModel> Airports { get; set; }

        public HomeViewModel()
        {
            var airportTest = new AirportViewModel()
            {
                AiportNumber = 1,
                AirportName = "Ben Gurion",
                Stations = new List<Station>()
                {
                    new Station()
                    {
                        StationNumber = 1,
                        StationName = "Fuel",
                        WaitingTime = TimeSpan.FromSeconds(15),
                    }
                }
            };


            Airports = new ObservableCollection<AirportViewModel>()
           {
               airportTest,
               airportTest,
               airportTest,
               airportTest,
               airportTest,

           };
        }
    }
}
