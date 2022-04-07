using System;
using System.Collections.Generic;

namespace Model
{
    public class Room
    {
        public Room(string name, string purpose, int floor, string id)
        {
            this.name = name;
            this.purpose = purpose;
            this.floor = floor;
            this.id = id;
        }

        public String name { get; set; }
        public String purpose { get; set; }
        public int floor { get; set; }
        public String id { get; set; }
        public List<Equipment> equipment { get; set; }

    }
}