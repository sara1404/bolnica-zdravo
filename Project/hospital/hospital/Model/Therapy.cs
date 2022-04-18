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
    }
}