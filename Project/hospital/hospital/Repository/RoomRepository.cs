using FileHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Repository
{
    public class RoomRepository
    {
        ObservableCollection<Room> rooms;

        public RoomRepository()
        {
            roomFileHandler = new RoomFileHandler();
            rooms = new ObservableCollection<Room>(); 
        }
        public void Create(Model.Room room)
        {
            rooms.Add(room);
            roomFileHandler.Write(rooms.ToList());
        }

        public Room FindRoomById(string id)
        {
            foreach (Room room in rooms) {
                if (room.id.Equals(id))
                    return room;
            }
            return null;
        }

        public ref ObservableCollection<Room> FindAll()
        {
            if(roomFileHandler.Read() == null)
                return ref rooms;
            foreach (Room room in roomFileHandler.Read()) {
                rooms.Add(room);
            }
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
                    roomFileHandler.Write(rooms.ToList());


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
                    rooms.Remove(room);
                    roomFileHandler.Write(rooms.ToList());
                    return true;
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