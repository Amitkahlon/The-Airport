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

        private UserControl _frame;
        public UserControl Frame
        {
            get { return _frame; }
            set { Set(ref _frame, value); }
        }

        Dictionary<string, UserControl> Views = new Dictionary<string, UserControl>();

        public RelayCommand DatabaseNavCommand { get; }


        public MainViewModel(IOptions<AppSettings> options, IConnectionService service)
        {
            settings = options.Value;
            this.service = service;

            Frame = new UserControl();
            DatabaseNavCommand = new RelayCommand(DatabaseNav);

            Views.Add("Home", new HomeView());


            // add avaliable pages/user control

            //set starting page

        }

        private void DatabaseNav()
        {
            Frame = Views["Home"];
        }

       
    }
}
