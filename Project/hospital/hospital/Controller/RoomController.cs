using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        private RoomService roomService;

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

        public List<Room> FindAll()
        {
            return roomService.FindAll();
        }

        public bool UpdateById(String id, Room room)
        {
            return roomService.UpdateById(id, room);
        }

        public bool DeleteById(String id)
        {
            return roomService.DeleteById(id);
        }

    }
}