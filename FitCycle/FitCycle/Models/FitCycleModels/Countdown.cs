using System;
using Xamarin.Forms;

namespace FitCycle.Models
{
    public class Countdown : BindableObject
    {
        TimeSpan _remainTime;

        public event Action<bool> Completed;
        public event Action Ticked;

        public DateTime EndDate { get; set; }

        public TimeSpan RemainTime
        {
            get { return _remainTime; }

            private set
            {
                _remainTime = value;
                OnPropertyChanged();
            }
        }

        public void Start(int seconds = 1)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                RemainTime = (EndDate - DateTime.Now);

                var ticked = RemainTime.TotalSeconds > 1;

                if (ticked)
                {
                    Ticked?.Invoke();
                }
                else
                {
                    Completed?.Invoke(true);
                }

                return ticked;
            });
        }

        public void Stop()
        {
            EndDate = DateTime.Now.AddSeconds(1);
            Start();
        }
    }
}
