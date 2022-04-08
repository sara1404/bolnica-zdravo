using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class RoomService
    {
        private readonly RoomRepository roomRepository;

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

        public ref List<Room> FindAll()
        {
            return ref roomRepository.FindAll();
        }

        public bool UpdateById(String id, Room room)
        {
            return roomRepository.UpdateById(id, room);
        }

        public bool DeleteById(string id)
        {
            return roomRepository.DeleteById(id);
        }

    }
}