using System;
using System.Collections.Generic;
using System.Text;

namespace FitCycle.Services
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;

        void Initialize();

        int ScheduleNotification(string title, string message, int id);

        void ReceiveNotification(string title, string message, int id);
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int ID { get; set; }
    }
}