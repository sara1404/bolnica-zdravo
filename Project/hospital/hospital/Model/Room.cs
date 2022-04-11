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

        public string name { get; set; }
        public string purpose { get; set; }
        public int floor { get; set; }
        public string id { get; set; }
        public List<Equipment> equipment { get; set; }

        public override string ToString()
        {
            return this.name;
        }

    }
}