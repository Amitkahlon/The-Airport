using ChatClient.ViewModels;
using ChatClient.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using WpfNetCoreMvvm.Models;
using WpfNetCoreMvvm.Services;

namespace WpfNetCoreMvvm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AppSettings settings;

        private readonly IConnectionService service;

        private UserControl _control;
        public UserControl Control
        {
            get { return _control; }
            set { Set(ref _control, value); }
        }

        Dictionary<string, UserControl> Views = new Dictionary<string, UserControl>();

        public RelayCommand HomeNavCommand { get; }
        public RelayCommand DatabaseCommand { get; }
        public RelayCommand VisualAirportCommand { get; }



        public MainViewModel(IOptions<AppSettings> options, IConnectionService service)
        {
            settings = options.Value;
            this.service = service;
            //commands
            HomeNavCommand = new RelayCommand(HomeNav);
            DatabaseCommand = new RelayCommand(DatabaseNav);
            VisualAirportCommand = new RelayCommand(VisualAirport);


            //views
            Views.Add("Home", new HomeView());
            Views.Add("Database", new DatabaseView());
            Views.Add("VisualAirport", new VisualAirportView());

            //set the home user control.
            //Control = Views["Home"];


            // add avaliable pages/user control

            //set starting page

        }

        private void DatabaseNav()
        {
            Control = Views["Database"];
        }

        private void VisualAirport()
        {
            Control = Views["VisualAirport"];
        }

        private void HomeNav()
        {
            Control = Views["Home"];
        }

       
    }
}
