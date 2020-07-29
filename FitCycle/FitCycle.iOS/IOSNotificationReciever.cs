using FitCycle.Services;
using System;
using UserNotifications;
using Xamarin.Forms;

namespace FitCycle.iOS
{
    public class IOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            if (!(Int32.TryParse(notification.Request.Identifier, out int messageid)))
                messageid = -1;
            DependencyService.Get<INotificationManager>().ReceiveNotification(notification.Request.Content.Title, notification.Request.Content.Body,messageid);

            // alerts are always shown for demonstration but this can be set to "None"
            // to avoid showing alerts if the app is in the foreground
            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}