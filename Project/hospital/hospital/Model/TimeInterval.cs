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

        public TimeInterval(string id, DateTime start, DateTime end)
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

        public override string ToString()
        {
            return start + "\n" + end;
        }
    }
}

