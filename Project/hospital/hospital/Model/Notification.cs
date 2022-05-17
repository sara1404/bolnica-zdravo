using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;

namespace Model
{
    public class Notification
    {
        private string _username;
        private DateTime _startTime = DateTime.MinValue;
        private DateTime _endTime = DateTime.MinValue;
        private int _interval;
        private string _text;


        public Notification() { }
        public Notification(string username)
        {
            _username = username;
        }
        public Notification(string username, DateTime startTime, DateTime endTime, int interval, string text)
        {
            _username = username;
            _startTime = startTime;
            _endTime = endTime;
            _interval = interval;
            _text = text;
        }

        public Notification(string username, DateTime startTime, int interval, string text)
        {
            _username = username;
            _startTime = startTime;
            _interval = interval;
            _text = text;
        }

        public string Username { get { return _username; } set { _username = value; } }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Interval { get; set; }
        public string Text { get; set; }
    }
}
