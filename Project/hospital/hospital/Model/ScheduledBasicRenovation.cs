using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class ScheduledBasicRenovation
    {
        string id;
        private Room room;
        private TimeInterval interval;
        string description;

        public ScheduledBasicRenovation(string id, Room room, TimeInterval interval, string description) {
            this.id = id;
            this.room = room;
            this.interval = interval;
            this.description = description;
        }

        public string _Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public Room _Room {
            get {
                return room;
            }
            set {
                room = value;
            }
        }

        public TimeInterval _Interval {
            get {
                return interval;
            }
            set {
                interval = value;
            }
        }

        public string _Description {
            get {
                return description;
            }
            set {
                description = value;
            }
        }
    }
}
