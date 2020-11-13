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

        private ICommand changePageCommand;
        private List<ViewModelBase> pageViewModels;
        private ViewModelBase currentPage;

        public MainViewModel(IOptions<AppSettings> options, IConnectionService service)
        {
            settings = options.Value;
            this.service = service;

            // add avaliable pages/user control

            //set starting page


        }

       

    }
}
