using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitCycle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileMenuPage : PopupPage
    {
        MainPage mainpage { get => Application.Current.MainPage as MainPage; }
        public ProfileMenuPage()
        {
            InitializeComponent();
        }

        async void ChangeAccount_Clicked(object sender, EventArgs e)
        {
            //var accountpage = new NavigationPage(new AccountPage());
            //mainpage.AddToolbar(accountpage);
            //await Navigation.PopPopupAsync();
            //mainpage.Detail = accountpage;
            //if (Device.RuntimePlatform == Device.Android)
            //    await Task.Delay(100);
            //mainpage.IsPresented = false;
        }
        async void Logout_Clicked(object sender, EventArgs e)
        {
            //Analytics.TrackEvent("User logged out", new Dictionary<string, string> { { "UserID", mainpage.User.UserID.ToString() }, { "Time", DateTime.UtcNow.ToString() } });
            //mainpage.User = null;
            //await Navigation.PopPopupAsync();
            //App.Current.MainPage = new LoginPage();
        }

        async void CloseMenu_Clicked(object sender,EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}