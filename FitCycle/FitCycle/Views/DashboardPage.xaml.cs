using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitCycle.ViewModels;

namespace FitCycle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        User user;
        DashboardViewModel viewModel;
        MainPage mainPage { get => Application.Current.MainPage as MainPage; }
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DashboardViewModel();
            user = mainPage.User;
            viewModel.Subtitle = user.UserName + "'s Dashboard";
        }


        //Events:
        //async void Account_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AccountPage());
        //}
        //async void Logout_Clicked(object sender, EventArgs e)
        //{
        //    user = null;
        //    await Navigation.PushModalAsync(new LoginPage());
        //}
        //async void About_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AboutPage());
        //}

    }
}