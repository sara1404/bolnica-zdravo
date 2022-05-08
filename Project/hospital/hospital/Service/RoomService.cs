using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            Console.WriteLine("Added new user");
            roomRepository.Create(room);
        }

        public Room FindRoomById(String id)
        {
            return roomRepository.FindRoomById(id);
        }

        public Room FindRoomByName(string name) {
            return roomRepository.FindRoomByName(name);
        }

        public Room FindRoomByPurpose(string purpose) {
            return roomRepository.FindRoomByPurpose(purpose);
        }
        public ref ObservableCollection<Room> FindAll()
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