using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;

namespace hospital.Model
{
    class Notification
    {
        private DateTime startTime;
        private DateTime endTime;
        private int interval;
        private string text;
        //private Timer preTimer;
        private static Timer timer;
        private static Timer preTimer;

        public Notification(DateTime startTime, string text)
        {
            // notification that runs only once, on specified time
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = startTime - current;
            if (current <= startTime)
            {
                this.text = text;
                this.endTime = endTime;
                preTimer = new Timer();
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += RunOnce;
                preTimer.Start();
            }
        }
        public void RunOnce(Object source, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show(text);
        }

        public Notification(DateTime startTime, DateTime endTime, int interval, string text)
        {
            // notification that runs periodically
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = startTime - current;
            if(current <= startTime)
            {
                this.interval = interval;
                this.text = text;
                this.endTime = endTime;
                preTimer = new Timer();
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += RunPeriodically;
                preTimer.Start();
            }
        }

        public void RunPeriodically(Object source, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show(text);
            timer = new Timer();
            timer.Interval = 1000 * 30 * interval;
            timer.AutoReset = true;
            timer.Elapsed += NotifyPeriodically;
            timer.Enabled = true;
            timer.Start();
            
        }
        public void NotifyPeriodically(Object source, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show(text);
            if(endTime <= DateTime.Now)
            {
                timer.Enabled = false;
                timer.Stop();
            }
        }

    }
}
