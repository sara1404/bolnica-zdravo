using System;
using System.Collections.Generic;

namespace Model
{
    public class Therapy
    {
        private DateTime timeStart;
        private DateTime timeEnd;
        private int interval; // interval is in hours

        private Medicine medicine;
        
        public Therapy(DateTime timeStart, DateTime timeEnd, int interval, Medicine medicine)
        {
            this.timeStart = timeStart;
            this.timeEnd = timeEnd;
            this.interval = interval;
            this.medicine = medicine;
        }

        public DateTime TimeStart{ get => timeStart; set => timeStart = value; }
        public DateTime TimeEnd { get => timeEnd; set => timeEnd = value; }
        public int Interval { get => interval; set => interval = value; }
        public Medicine Medicine { get => medicine; set => medicine = value; }

    }
}