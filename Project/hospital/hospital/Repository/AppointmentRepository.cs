using hospital.DTO;
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
        private ObservableCollection<Appointment> _appointments;
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

        public AppointmentRepository()
        {
            appointmentsFileHandler = new FileHandler.AppointmentsFileHandler();

            List<Appointment> deserializedList = appointmentsFileHandler.Read();
            if (deserializedList == null)
            {
                _appointments = new ObservableCollection<Appointment>();
            }
            else
            {
                _appointments = new ObservableCollection<Appointment>(appointmentsFileHandler.Read());
            }
        }

        public int GetNewId()
        {
            if (_appointments.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                foreach (Appointment a in _appointments)
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
            foreach (Appointment a in _appointments)
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
            return _appointments;
        }

        public void DeleteById(int id)
        {
            _appointments.Remove(FindById(id));
            appointmentsFileHandler.Write(_appointments.ToList());
        }

        public void Create(Appointment _appointment)
        {
            _appointments.Add(_appointment);
            appointmentsFileHandler.Write(_appointments.ToList());
        }

        public void AddAppointment(Model.Appointment newAppointment)
        {
            if (newAppointment == null)
            {
                return;
            }

            if (_appointments == null)
            {
                _appointments = new ObservableCollection<Appointment>();
            }

            if (!_appointments.Contains(newAppointment))
            {
                _appointments.Add(newAppointment);
                appointmentsFileHandler.Write(_appointments.ToList());
            }
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
            {
                return;
            }

            if (_appointments != null)
            {
                if (_appointments.Contains(oldAppointment))
                {
                    _appointments.Remove(oldAppointment);
                    appointmentsFileHandler.Write(_appointments.ToList());
                }
            }
        }

        public List<Appointment> FindAppointmentsForSpecifiedRoom(Room room)
        {
            List<Appointment> appointmentsInRoom = new List<Appointment>();
            foreach (Appointment appointment in _appointments)
            {
                if (appointment.RoomId.Equals(room.id))
                {
                    appointmentsInRoom.Add(appointment);
                }
            }
            return appointmentsInRoom;
        }

        public List<ChartDataDTO> GetPopularTimes()
        {
            List<ChartDataDTO> retVal = new List<ChartDataDTO>();
            foreach (Appointment a in _appointments)
            {
                if (retVal.Find(x => x.Time == ConvertDateToHours(a.StartTime)) == null)
                {
                    retVal.Add(new ChartDataDTO(ConvertDateToHours(a.StartTime)));
                }
                else
                {
                    retVal.Find(x => x.Time == ConvertDateToHours(a.StartTime)).NumberOfAppointments++;
                }
            }
            return retVal.OrderBy(x => x.Time).ToList();
        }

        private DateTime ConvertDateToHours(DateTime date)
        {
            return new DateTime(1, 1, 1, date.Hour, date.Minute, 0);
        }
    }
}