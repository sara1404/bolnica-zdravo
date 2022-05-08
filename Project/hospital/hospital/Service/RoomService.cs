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
        private readonly AppointmentRepository _appointmentRepository;

        public RoomService(RoomRepository roomRepository, AppointmentRepository appointmentRepository)
        {
            this.roomRepository = roomRepository;
            this._appointmentRepository = appointmentRepository;
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

        public ObservableCollection<Room> FindAllOperationRooms()
        {
            ObservableCollection<Room> roomsForOperation = new ObservableCollection<Room>();
            foreach (Room room in FindAll())
            {
                if(room.purpose == "operation")
                {
                    roomsForOperation.Add(room);
                }
            }
            return roomsForOperation;
        }
        public Room FindRoomForOperationByTime(DateTime dateTime)
        {
            foreach(Room room in FindAllOperationRooms())
            {
                bool isBussy = false;
                foreach(Appointment appointment in _appointmentRepository.FindAll())
                {
                    if (dateTime == appointment.StartTime && appointment.roomId == room.id)
                    {
                        isBussy = true;
                        break;
                    }
                }
                if (!isBussy) return room;
            }
            return null;
        }
    }
}