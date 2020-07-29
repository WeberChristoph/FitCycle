using FitCycle.Models;
using FitCycle.Resources;
using FitCycle.Services;
using FitCycle.ViewModels;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitCycle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        ToolbarItem toolbar_profile;
        ToolbarItem toolbar_logo;
        List<ToolbarItem> toolbarItems = new List<ToolbarItem>();
        public User User { get; set; }


        public MainPage(User CurrentUser)
        {
            Device.SetFlags(new[] { "SwipeView_Experimental" });

            InitializeComponent();

            //Check User:
            if (CurrentUser.UserID > 0)
            {
                User = CurrentUser;
                Analytics.TrackEvent("User logged in successfully", new Dictionary<string, string> { { "UserID", User.UserID.ToString() }, { "Time", DateTime.UtcNow.ToString() } });
            }
            else
            {
                var error = new KeyNotFoundException(AppResources.UserID_not_available + ". " + AppResources.Access_not_allowed);
                Crashes.TrackError(error);
                throw error;
            }


            //Define Page-Standards:
            toolbar_logo = new ToolbarItem { IconImageSource = "badge_250x250.png", Order = ToolbarItemOrder.Primary, Priority = 1 };
            toolbar_logo.Clicked += OnLogoClicked;
            toolbar_profile = new ToolbarItem { IconImageSource = "profile_placeholder.png", Order = ToolbarItemOrder.Primary, Priority = 0 };
            toolbar_profile.Clicked += OnProfileClicked;
            toolbarItems.Add(toolbar_logo);
            toolbarItems.Add(toolbar_profile);

            MasterBehavior = MasterBehavior.Popover;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("MainPage OnAppearing started");
            var viewModel = new BaseViewModel();
            BindingContext = viewModel;

            NavigateIntoApp();
            Analytics.TrackEvent("MainPage OnAppearing ended");
            Debug.WriteLine("MainPage OnAppearing ended");
        }


        public void NavigateIntoApp()
        {
            //Standard Main Page:
            if (!MenuPages.ContainsKey((int)MenuItemType.Dashboard)) { MenuPages.Add((int)MenuItemType.Dashboard, new NavigationPage(new DashboardPage())); }
            var startpage = MenuPages[(int)MenuItemType.Dashboard];
            AddToolbar(startpage);
            Detail = startpage;
        }

        public Page GetMenuPage(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Dashboard:
                        MenuPages.Add(id, new NavigationPage(new DashboardPage()));
                        break;
                    //case (int)MenuItemType.Zyklus:
                    //    MenuPages.Add(id, new NavigationPage(new CyclePage()));
                    //    break;
                    //case (int)MenuItemType.Übungen:
                    //    MenuPages.Add(id, new NavigationPage(new ÜbungenPage()));
                    //    break;
                    //case (int)MenuItemType.Maximalkraftwerte:
                    //    MenuPages.Add(id, new NavigationPage(new MaxPowerPage()));
                    //    break;
                    //case (int)MenuItemType.Chat:
                    //    MenuPages.Add(id, new NavigationPage(new UserChatPage()));
                    //    break;
                }
                AddToolbar(MenuPages);
            }
            return (MenuPages[id]);
        }

        public async Task NavigateFromMenu(int id)
        {
            var newPage = GetMenuPage(id);

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public void DeleteFromMenuPages<T>(T page)
        {
             var key = MenuPages.Where(n => n.Value.CurrentPage.Equals(page)).FirstOrDefault().Key;
             MenuPages.Remove(key);
        }


        public void AddToolbar(Dictionary<int, NavigationPage> PagesDict)
        {
            foreach (var page in PagesDict)
            {
                foreach (var toolbar in toolbarItems)
                {
                    if (!page.Value.ToolbarItems.Contains(toolbar)) { page.Value.ToolbarItems.Add(toolbar); }
                }
            }
        }
        public void AddToolbar(NavigationPage Page)
        {
            foreach (var toolbar in toolbarItems)
            {
                if (!Page.ToolbarItems.Contains(toolbar)) { Page.ToolbarItems.Add(toolbar); }
            }
        }





        //Events:
        async private void OnProfileClicked(object sender,EventArgs e)
        {
            var profilemenu = new ProfileMenuPage();
            await Navigation.PushPopupAsync(profilemenu,false);
        }
        async private void OnLogoClicked(object sender, EventArgs e)
        {
            //Go to Cycle

        }
    }

}