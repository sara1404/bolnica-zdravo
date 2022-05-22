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

        public Notification(string username, DateTime startTime, string text)
        {
            _username = username;
            _startTime = startTime;
            _text = text;
        }

        public string Username { get { return _username; } set { _username = value; } }
        public DateTime StartTime { get { return _startTime; } set { _startTime = value; } }
        public DateTime EndTime { get { return _endTime; } set { _endTime = value; } }
        public int Interval { get { return _interval; } set { _interval = value; } }
        public string Text { get { return _text; } set { _text = value; } }
    }
}
