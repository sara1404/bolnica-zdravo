using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class EquipmentRelocation
    {
        private string id;
        private DateTime start;
        private DateTime end;

        public EquipmentRelocation(string id, DateTime start, DateTime end)
        {
            this.id = id;
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

        public string _Id {
            get 
            {
                return id;
            }
            set {
                id = value;
            }
        }

    }
}

