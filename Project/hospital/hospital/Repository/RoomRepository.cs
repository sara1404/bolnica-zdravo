using FileHandler;
using hospital.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Repository
{
    public class RoomRepository : IRoomRepository
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
        }

        public Room FindById(string id)
        {
            foreach (Room room in rooms) {
                if (room.id.Equals(id))
                    return room;
            }
            return null;
        }

        public Room FindByName(string name) {
            foreach (Room room in rooms)
            {
                if (room._Name.Equals(name))
                    return room;
            }
            return null;
        }

        public Room FindByPurpose(string purpose) {
            foreach (Room room in rooms)
            {
                if (room._Purpose.Equals(purpose))
                    return room;
            }
            return null;
        }
        public List<Room> FindRoomsByPurpose(string purpose)
        {
            List<Room> retVal = new List<Room>();
            foreach (Room room in rooms)
            {
                if (room._Purpose.Equals(purpose))
                    retVal.Add(room);
            }
            return retVal;
        }

        public ObservableCollection<Room> FindRoomsByEquipmentType(string type) {
            ObservableCollection<Room> filteredRooms = new ObservableCollection<Room>();
            foreach (Room room in rooms) {
                foreach (Equipment eq in room.equipment) {
                    if (eq.type.Contains(type))
                        filteredRooms.Add(room);
                }
            }
            return filteredRooms;
        }


        public ObservableCollection<Room> FindRoomsByEquipmentQuantity(int quantity) {
            ObservableCollection<Room> filteredRooms = new ObservableCollection<Room>();
            foreach (Room room in rooms)
            {
                if (RoomHasEnoughEquipment(quantity, room)) filteredRooms.Add(room);
            }
            return filteredRooms;
        }

        private bool RoomHasEnoughEquipment(int quantity, Room room)
        {
            foreach (Equipment eq in room.equipment)
            {
                if (eq.quantity >= quantity)
                    return true;
            }
            return false;
        }

        public ObservableCollection<Room> FindRoomsByEquipmentTypeAndQuantity(string type, int quantity) {
            ObservableCollection<Room> filteredRooms = new ObservableCollection<Room>();
            foreach (Room room in rooms)
            {
                foreach (Equipment eq in room.equipment)
                {
                    if (eq.quantity >= quantity && eq.type.Contains(type))
                    {
                        filteredRooms.Add(room);
                        break;
                    }
                }
            }
            return filteredRooms;
        }

        public ref ObservableCollection<Room> FindAll()
        {
            return ref rooms;
        }

        public bool UpdateById(string id, Model.Room room)
        {
            foreach (Room r in rooms)
            {
                if (r.id.Equals(id)) {
                    r.id = room.id;
                    r._Name = room.name;
                    r.floor = room.floor;
                    r.equipment = room.equipment;
                    r._Purpose = room.purpose;
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
                    return rooms.Remove(room);
            }
            return false;
        }

        public void LoadRoomData() {
            if (roomFileHandler.Read() != null) {
                foreach (Room room in roomFileHandler.Read())
                {
                    rooms.Add(room);
                }
            }
        }

        public void WriteRoomData() {
            roomFileHandler.Write(rooms.ToList());
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