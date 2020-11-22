using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }

        public HomeViewModel()
        {
            Users = new ObservableCollection<User>()
            {
                new User
                {
                    name = "amit",
                    age = 21
                }
            };
        }
    }

    public class User
    {
        public int age { get; set; }
        public string name{ get; set; }
    }
}
