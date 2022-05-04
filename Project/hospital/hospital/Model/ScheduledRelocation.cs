using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class ScheduledRelocation
    {
        private string id;
        private Room fromRoom;
        private Room toRoom;
        private string typeOfEquipment;
        private int quantity;
        private TimeInterval relocation;

        public ScheduledRelocation(string id, Room froomRoom, Room toRoom, string typeOfEquipment, int quantity, TimeInterval relocation) {
            this.id = id;
            this.fromRoom = froomRoom;
            this.toRoom = toRoom;
            this.typeOfEquipment = typeOfEquipment;
            this.quantity = quantity;
            this.relocation = relocation;
        }

        public string _Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        public Room _FromRoom {
            get {
                return fromRoom;
            }
            set {
                fromRoom = value;
            }
        }

        public Room _ToRoom
        {
            get
            {
                return toRoom;
            }
            set
            {
                toRoom = value;
            }
        }

        public string _TypeOfEquipment
        {
            get
            {
                return typeOfEquipment;
            }
            set
            {
                typeOfEquipment = value;
            }
        }
        public int _Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
        public TimeInterval _Relocation
        {
            get
            {
                return relocation;
            }
            set
            {
                relocation = value;
            }
        }


    }
}
