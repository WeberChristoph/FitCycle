using FitCycle.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FitCycle.ViewModels
{
    class LoginAccountViewModel : BaseViewModel
    {

        string mailadress = string.Empty;
        string message = string.Empty;
        string password = string.Empty;
        string username = string.Empty;
        int weight = 0;
        int age = 0;

        public string MailAdress
        {
            get { return mailadress; }
            set { SetProperty(ref mailadress, value); }
        }
        
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
       
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        public int Weight
        {
            get { return weight; }
            set { SetProperty(ref weight, value); }
        }

        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }


        public LoginAccountViewModel()
        {
            Title = AppResources.Account;
        }

    }
}
