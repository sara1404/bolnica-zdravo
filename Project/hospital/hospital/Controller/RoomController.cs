using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            return roomService.UpdateById(id, room);
        }

        public bool DeleteById(string id)
        {
            return roomService.DeleteById(id);
        }

    }
}