using System;
using System.Collections.Generic;

namespace Model
{
    public class Room
    {
        public Room(string name, string purpose, int floor, string id, List<Equipment> equipment)
        {
            this.name = name;
            this.purpose = purpose;
            this.floor = floor;
            this.id = id;
            this.equipment = equipment;
        }

        public String name { get; set; }
        public String purpose { get; set; }
        public int floor { get; set; }
        public String id { get; set; }
        public List<Equipment> equipment { get; set; }

    }
}