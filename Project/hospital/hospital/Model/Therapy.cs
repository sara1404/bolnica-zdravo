using System;
using System.Collections.Generic;

namespace Model
{
    public class Therapy
    {
        private DateTime _timeStart;
        private DateTime _timeEnd;
        private int _interval;
        private Medicine _medicine;
        
        public Therapy(DateTime timeStart, DateTime timeEnd, int interval, Medicine medicine)
        {
            _timeStart = timeStart;
            _timeEnd = timeEnd;
            _interval = interval;
            _medicine = medicine;
        }
        public DateTime TimeStart{ get => _timeStart; set => _timeStart = value; }
        public DateTime TimeEnd { get => _timeEnd; set => _timeEnd = value; }
        public int Interval { get => _interval; set => _interval = value; }
        public Medicine Medicine { get => _medicine; set => _medicine = value; }
    }
}