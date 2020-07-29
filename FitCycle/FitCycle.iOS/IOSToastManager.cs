using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using FitCycle.Services;
using FitCycle.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(IOSToastManager))]
namespace FitCycle.iOS
{
    public class IOSToastManager : IToastManager
    {
        const double LONG_DELAY = 3.5;


        NSTimer alertDelay;
        UIAlertController alert;

        public void Show(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }


        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}