using FitCycle.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitCycle.ViewModels
{
    class DashboardViewModel : BaseViewModel
    {
        string subtitle = string.Empty;

        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        public DashboardViewModel()
        {
            Title = AppResources.Dashboard;
        }
    }
}
