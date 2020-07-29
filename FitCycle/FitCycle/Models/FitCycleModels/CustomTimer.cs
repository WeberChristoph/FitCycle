using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace FitCycle.Models
{
    public enum TimerRefreshInterval { Second,Minute,Hour }
    public enum TimerStatus { Running, Paused, Stopped }
    public class CustomTimer
    {
        private Timer timer= new Timer();
        public int CurrentHour { get; private set; }
        public int CurrentMinute { get; private set; }
        public int CurrentSecond { get; private set; }
        public TimerStatus TimerStatus { get; private set; }

        public delegate void TimerTickedEventHandler(object source, EventArgs args);
        public event TimerTickedEventHandler TimerTicked;
        public TimerRefreshInterval Refreshinterval;


        public CustomTimer(TimerRefreshInterval refreshinterval)
        {
            Refreshinterval = refreshinterval;
            switch (Refreshinterval)
            {
                case TimerRefreshInterval.Second:
                    timer.Interval = 1000;
                    break;
                case TimerRefreshInterval.Minute:
                    timer.Interval = 60000;
                    break;
                case TimerRefreshInterval.Hour:
                    timer.Interval = 3600000;
                    break;
            }
            timer.AutoReset = true;
        }

        private void TimerElapsed(object source,ElapsedEventArgs e)
        {
            switch (Refreshinterval)
            {
                case TimerRefreshInterval.Second:
                    CurrentSecond++;
                    break;
                case TimerRefreshInterval.Minute:
                    CurrentMinute++;
                    break;
                case TimerRefreshInterval.Hour:
                    CurrentHour++;
                    break;
            }
            if (CurrentSecond >= 60)
            {
                CurrentSecond = 0;
                CurrentMinute++;
            }
            if (CurrentMinute >= 60)
            {
                CurrentSecond = 0;
                CurrentMinute = 0;
                CurrentHour++;
            }
            TimerTicked?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            timer.Elapsed += TimerElapsed;
            timer.Enabled = true;
            TimerStatus = TimerStatus.Running;
        }
        public void Pause()
        {
            timer.Elapsed -= TimerElapsed;
            TimerStatus = TimerStatus.Paused;
        }
        public void Continue()
        {
            timer.Elapsed += TimerElapsed;
            TimerStatus = TimerStatus.Running;
        }
        public void Stop()
        {
            timer.Elapsed -= TimerElapsed;
            timer.Enabled = false;
            TimerStatus = TimerStatus.Stopped;
        }

    }
}
