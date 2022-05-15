using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class TimeInterval
    {
        private DateTime start;
        private DateTime end;

        public TimeInterval(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public DateTime _Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public DateTime _End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }

        }

        public bool IsOverlaping(TimeInterval interval) {
            return DateIsInInterval(this, interval._Start.Date) || DateIsInInterval(this, interval._End.Date)
                       || IsInsideInterval(this, interval) || IsInsideInterval(interval, this);
        }

        private bool DateIsInInterval(TimeInterval interval, DateTime date)
        {
            return interval._Start.Date <= date && interval._End >= date;
        }

        private bool IsInsideInterval(TimeInterval outside, TimeInterval inside)
        {
            return outside._Start <= inside._Start && outside._End >= inside._End;
        }

        public override string ToString()
        {
            return start + "\n" + end;
        }
    }
}

