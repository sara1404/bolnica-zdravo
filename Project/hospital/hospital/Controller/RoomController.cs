using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Controller
{
    public class RoomController
    {
        private readonly RoomService roomService;

        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }

        public void Create(Room room)
        {
            if (room.purpose.ToLower().Equals("warehouse") && doesWarehouseExists()) {
                throw new Exception("Warehouse already exists");
            }
            if (room.floor < 0 || room.floor > 5)
                throw new Exception("Floor must be between 0 and 5");
            if (!isNewId(room.id))
                throw new Exception("Room with that ID already exists");
            roomService.Create(room);
        }

        public Room FindRoomById(String id)
        {
            return roomService.FindRoomById(id);
        }

        public ref ObservableCollection<Room> FindAll()
        {
            return ref roomService.FindAll();
        }

        public bool UpdateById(String id, Room room)
        {
            Room r = FindRoomById(id);
            if (r.purpose.ToLower().Equals("warehouse"))
            {
                throw new Exception("Warehouse cant be edited");
            }
            if(room.purpose.ToLower().Equals("warehouse") && doesWarehouseExists())
                throw new Exception("Warehouse already exists");
            if (room.floor < 0 || room.floor > 5)
                throw new Exception("Floor must be between 0 and 5");
            return roomService.UpdateById(id, room);
        }

        public bool DeleteById(string id)
        {
            if (FindRoomById(id).purpose == "warehouse")
                throw new Exception("Warehouse cant be deleted");
            return roomService.DeleteById(id);
        }

        private bool doesWarehouseExists() {
            List<Room> rooms = FindAll().ToList();
            foreach (Room r in rooms) {
                if (r.purpose.ToLower().Equals("warehouse"))
                    return true;
            }
            return false;
        }

        private bool isNewId(String id) {
            List<Room> rooms = FindAll().ToList();
            foreach (Room room in rooms) {
                if (room.id.Equals(id.Trim()))
                    return false;
            }
            return true;
        }
    }
}