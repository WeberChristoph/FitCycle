using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FitCycle.ViewModels;
using FitCycle.Resources;
using System.Diagnostics;
using FitCycle.Models.DbManager;

namespace FitCycle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        
        LoginAccountViewModel viewModel;
        UserManager userManager;
        public LoginPage(User CurrentUser = null)
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginAccountViewModel();
            userManager = UserManager.DefaultManager;
            if (CurrentUser != null)
            {
                viewModel.MailAdress = CurrentUser.MailAdress;
            }
        }

        //Events:
        async void Login_Clicked(object sender, EventArgs e)
        {
            viewModel.IsBusy = true;
            if (!(sender is Button button))
                return;
            viewModel.Message = "";
            button.IsEnabled = false;
            try
            {
                var user = await userManager.Login(viewModel.MailAdress, viewModel.Password);
                if (user != null)
                {
                    viewModel.Message = AppResources.Login_successful;
                    Debug.WriteLine("Login sucessful");
                    App.Current.MainPage = new MainPage(user);
                }
                else
                {
                    viewModel.Message = AppResources.Login_failed + ": " + AppResources.Email_or_password_incorrect;
                }

            }
            catch (OperationCanceledException ex)
            {
                viewModel.Message = AppResources.Login_failed + ": " + AppResources.Connection_could_not_be_established;
                if (Testing.IsToggled) { viewModel.Message += "\r\nFehlercode:" + ex; }
            }
            catch (Exception ex)
            {
                viewModel.Message = AppResources.Login_failed;
                if (Testing.IsToggled) { viewModel.Message += "\r\nFehlercode:" + ex; }
            }
            finally
            {
                button.IsEnabled = true;
                viewModel.IsBusy = false;
            }
        }

        void CreateAccount_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AccountPage();
        }
        protected override bool OnBackButtonPressed()
        {
            //stop the back button
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("", AppResources.Would_you_like_to_exit_from_application, AppResources.Yes, AppResources.No);
                if (result)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        //DependencyService.Get<IAndroidMethods>().CloseApp();
                        System.Threading.Thread.CurrentThread.Abort();
                    }
                    else if (Device.RuntimePlatform == Device.iOS)
                    {
                        System.Threading.Thread.CurrentThread.Abort();
                    }
                    else if (Device.RuntimePlatform == Device.UWP)
                    {
                        //UWP
                    }
                }
            });
            return true;

        }

        void Testing_Toggled(object sender, EventArgs e)
        {
            viewModel.MailAdress = "chrisweb@aon.at";
            viewModel.Password = "123";
        }
    }

    internal interface IAndroidMethods
    {
        void CloseApp();
    }
}