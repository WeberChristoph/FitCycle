using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitCycle.ViewModels;
using FitCycle.Services;
using FitCycle.Resources;
using System.Diagnostics;
using FitCycle.Models.DbManager;

namespace FitCycle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        LoginAccountViewModel viewModel;
        UserManager userManager;
        User user;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public AccountPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginAccountViewModel();
            userManager = UserManager.DefaultManager;
        }

        async void Logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        void OnPassword_tapped(object sender, EventArgs e)
        {
            Entry_password_copy.IsEnabled = true;
        }

        async void Create_Account(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;
            viewModel.IsBusy = true;
            var button_save_color = button.BackgroundColor;
            button.BackgroundColor = Color.Gray;
            //Create Account:
            if (!viewModel.Password.Equals(Entry_password_copy.Text))
            {
                await DisplayAlert(AppResources.Password_invalid, AppResources.Passwords_do_not_match, AppResources.OK);
            }
            else
            {
                user = new User();
                user.UserName = viewModel.Username;
                user.Weight = viewModel.Weight;
                if (await userManager.CreateUser(user,viewModel.Password))
                {
                    App.Current.MainPage = new LoginPage(user);
                }
                else
                {
                    Debug.WriteLine("CreateAccountAsync=false");
                    DependencyService.Get<IToastManager>().Show(AppResources.Account_could_not_be_created + "\r\n(xAccPxCreateAccount=false)");
                    user = null;
                }
            }
            viewModel.IsBusy = false;
            button.IsEnabled = true;
            button.BackgroundColor = button_save_color;
        }
    

        async void Refresh(object sender, EventArgs e)
        {
            viewModel.IsBusy = true;
            await Task.Run(() => System.Threading.Thread.Sleep(1000));
            viewModel.IsBusy = false;
            var newaccountpage = new NavigationPage(new AccountPage());
            RootPage.AddToolbar(newaccountpage);
            RootPage.Detail = newaccountpage;
        }

    }
}