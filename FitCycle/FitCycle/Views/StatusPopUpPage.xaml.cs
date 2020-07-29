using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using FitCycle.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitCycle.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPopUpPage : PopupPage
    {
        public StatusPopUpPage(object bindingcontext)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            BindingContext = bindingcontext;
        }
    }
}