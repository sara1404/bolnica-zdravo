using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Repository
{
    public class AppointmentRepository
    {
        public ObservableCollection<Appointment> appointments;
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

        public AppointmentRepository()
        {
            appointmentsFileHandler = new FileHandler.AppointmentsFileHandler();

            List<Appointment> deserializedList = appointmentsFileHandler.Read();
            if (deserializedList == null)
            {
                appointments = new ObservableCollection<Appointment>();
            }
            else
            {
                appointments = new ObservableCollection<Appointment>(appointmentsFileHandler.Read());
            }
        }

        public int GetNewId()
        {
            if (appointments.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                foreach (Appointment a in appointments)
                {
                    if (a.Id > max)
                    {
                        max = a.Id;
                    }
                }
                return max + 1;
            }
        }
        public Appointment FindById(int id)
        {
            foreach (Appointment a in appointments)
            {
                if (a.Id == id)
                {
                    return a;
                }
            }
            return null;
        }

        public ObservableCollection<Appointment> FindAll()
        {
            return appointments;
        }

        public void DeleteById(int id)
        {
            appointments.Remove(FindById(id));
            appointmentsFileHandler.Write(appointments.ToList());
        }

        public void Create(Appointment _appointment)
        {
            appointments.Add(_appointment);
            appointmentsFileHandler.Write(appointments.ToList());
        }

        public void AddAppointment(Model.Appointment newAppointment)
        {
            if (newAppointment == null)
            {
                return;
            }

            if (appointments == null)
            {
                appointments = new ObservableCollection<Appointment>();
            }

            if (!appointments.Contains(newAppointment))
            {
                appointments.Add(newAppointment);
                appointmentsFileHandler.Write(appointments.ToList());
            }
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
            {
                return;
            }

            if (appointments != null)
            {
                if (appointments.Contains(oldAppointment))
                {
                    appointments.Remove(oldAppointment);
                    appointmentsFileHandler.Write(appointments.ToList());
                }
            }
        }

        public List<Appointment> FindAppointmentsForSpecifiedRoom(Room room)
        {
            List<Appointment> appointmentsInRoom = new List<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.RoomId.Equals(room.id))
                {
                    appointmentsInRoom.Add(appointment);
                }
            }
            return appointmentsInRoom;
        }

        public Dictionary<DateTime, int> GetPopularTimes()
        {
            Dictionary<DateTime, int> retVal = new Dictionary<DateTime, int>();
            foreach (Appointment a in appointments)
            {
                if (retVal.ContainsKey(a.StartTime))
                {
                    retVal[a.StartTime] = 1;
                }
                else
                {
                    retVal[a.StartTime] += 1;
                }
            }
            return retVal;
        }
    }
}