using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        private RoomRepository roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public void Create(Room room)
        {
            roomRepository.Create(room);
        }

        public Room FindRoomById(String id)
        {
            return roomRepository.FindRoomById(id);
        }

        public List<Room> FindAll()
        {
            return roomRepository.FindAll();
        }

        public bool UpdateById(String id, Room room)
        {
            return roomRepository.UpdateById(id, room);
        }

        public bool DeleteById(String id)
        {
            return roomRepository.DeleteById(id);
        }

    }
}