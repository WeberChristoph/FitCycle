using FitCycle.Models;
using FitCycle.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;


namespace FitCycle.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        BaseViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BaseViewModel();
            viewModel.Title = "FitCycle Menü";

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Dashboard, Title="Dashboard" },
                new HomeMenuItem {Id = MenuItemType.Zyklus, Title="Trainings-Zyklus" },
                new HomeMenuItem {Id = MenuItemType.Übungen, Title="Übungen" },
                new HomeMenuItem {Id = MenuItemType.Maximalkraftwerte, Title="Maximalkraftwerte" },
                new HomeMenuItem {Id = MenuItemType.Chat, Title="Chat" }
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemTapped += async (sender, e) =>
            {
                viewModel.IsBusy = true;
                if (e.Item == null)
                    return;
                
                var id = (int)((HomeMenuItem)e.Item).Id;
                await RootPage.NavigateFromMenu(id);
                viewModel.IsBusy = false;
            };
        }
    }
}