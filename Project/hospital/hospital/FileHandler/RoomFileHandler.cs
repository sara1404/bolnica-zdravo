using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileHandler
{
    public class RoomFileHandler
    {
        private readonly string path = @"../../Resources/Data/RoomData.txt";


        public List<Room> Read()
        {
            string serializedRooms = System.IO.File.ReadAllText(path);
            List<Room> rooms = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Room>>(serializedRooms);
            return rooms;
        }

        public void Write(List<Room> rooms)
        {
            string serializedRooms = Newtonsoft.Json.JsonConvert.SerializeObject(rooms);
            System.IO.File.WriteAllText(path, serializedRooms);
        }

    }
}