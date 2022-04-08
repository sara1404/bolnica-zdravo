using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class RoomRepository
    {
        List<Room> rooms;
        Room room1 = new Room("123", "operation", 1, "o1");
        Room room2 = new Room("1234", "operation", 2, "o2");
        Room room3 = new Room("1235", "operation", 1, "o3");


        public RoomRepository()
        {
            rooms = new List<Room>();
            rooms.Add(room1);
            rooms.Add(room2);
            rooms.Add(room3);

        }
        public void Create(Model.Room room)
        {
            rooms.Add(room);
        }

        public Room FindRoomById(string id)
        {
            foreach (Room room in rooms) {
                if (room.id.Equals(id))
                    return room;
            }
            return null;
        }

        public ref List<Room> FindAll()
        {
            return ref rooms;
        }

        public bool UpdateById(string id, Model.Room room)
        {
            foreach (Room r in rooms)
            {
                if (r.id.Equals(id)) {
                    r.id = room.id;
                    r.name = room.name;
                    r.floor = room.floor;
                    r.equipment = room.equipment;
                    r.purpose = room.purpose;
                    return true;
                }
                    
            }
            return false;
        }

        public bool DeleteById(string id)
        {
            foreach (Room room in rooms)
            {
                if (room.id.Equals(id))
                {
                    return rooms.Remove(room);
                }
            }
            return false;
        }

        public System.Collections.Generic.List<Room> room;


        public System.Collections.Generic.List<Room> Room
        {
            get
            {
                if (room == null)
                {
                    room = new System.Collections.Generic.List<Room>();
                }

                return room;
            }
            set
            {
                RemoveAllRoom();
                if (value != null)
                {
                    foreach (Model.Room oRoom in value)
                    {
                        AddRoom(oRoom);
                    }
                }
            }
        }


        public void AddRoom(Model.Room newRoom)
        {
            if (newRoom == null)
            {
                return;
            }

            if (room == null)
            {
                room = new System.Collections.Generic.List<Room>();
            }

            if (!room.Contains(newRoom))
            {
                room.Add(newRoom);
            }
        }


        public void RemoveRoom(Model.Room oldRoom)
        {
            if (oldRoom == null)
            {
                return;
            }

            if (room != null)
            {
                if (room.Contains(oldRoom))
                {
                    room.Remove(oldRoom);
                }
            }
        }


        public void RemoveAllRoom()
        {
            if (room != null)
            {
                room.Clear();
            }
        }
        public FileHandler.RoomFileHandler roomFileHandler;

    }
}