using ChatClient.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Windows.Input;
using WpfNetCoreMvvm.Models;
using WpfNetCoreMvvm.Services;

namespace WpfNetCoreMvvm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly AppSettings settings;

        private readonly IConnectionService service;

        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; }
        }


        public MainViewModel(IOptions<AppSettings> options, IConnectionService service)
        {
            settings = options.Value;
            this.service = service;

            SelectedViewModel = new HomeViewModel();

            // add avaliable pages/user control

            //set starting page


        }

       

    }
}
