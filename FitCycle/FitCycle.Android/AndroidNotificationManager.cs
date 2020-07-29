using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using FitCalc.Droid;
using FitCycle.Services;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(FitCycle.Droid.AndroidNotificationManager))]
namespace FitCycle.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";
        const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public const string IDKey = "id";

        bool channelInitialized = false;
        int messageId = -1;
        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        public int ScheduleNotification(string title, string message, int id)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            messageId++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            intent.PutExtra(IDKey, id);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            Android.Net.Uri alarmSound = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            long[] vibrate = { 0, 100, 200, 300 };

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSound(alarmSound)
                .SetVibrate(vibrate)
                //.SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.abc_seekbar_tick_mark_material))
                .SetSmallIcon(Resource.Drawable.notification_icon_background)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId, notification);

            return messageId;
        }

        public void ReceiveNotification(string title, string message, int id)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
                ID = id
            };
            NotificationReceived?.Invoke(null, args);
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}