using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FitCycle.Droid;
using FitCycle.Services;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidToastManager))]
namespace FitCycle.Droid
{
    public class AndroidToastManager :IToastManager
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}