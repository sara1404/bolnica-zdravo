using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository;
using hospital;
using System.Windows;
using Controller;
using System.Linq;
using hospital.DTO;

namespace Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        //private readonly RoomService _roomService; // ovaj servis je ostao viska, pogledati gde bi trebalo da se ubaci

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
            //this._roomService = roomService;
        }

        public void Create(Appointment appointment)
        {
            appointment.Id = _appointmentRepository.GetNewId();
            _appointmentRepository.Create(appointment);
        }
        public void Update(Appointment oldAppointment, Appointment newAppointment)
        {
            _appointmentRepository.RemoveAppointment(oldAppointment);
            newAppointment.Id = oldAppointment.Id;
            newAppointment.DoctorUsername = oldAppointment.DoctorUsername;
            newAppointment.PatientUsername = oldAppointment.PatientUsername;
            newAppointment.RoomId = oldAppointment.RoomId;
            _appointmentRepository.AddAppointment(newAppointment);
        }
        public void Delete(int id)
        {
            _appointmentRepository.DeleteById(id);
        }

        public ObservableCollection<Appointment> GetAll()
        {
            return _appointmentRepository.FindAll();
        }

        public ObservableCollection<Appointment> GetByDoctor(string username)
        {
            // refactor this method and GetByPatient so that if FindAll() returns null the method should return empty list
            if(_appointmentRepository.FindAll() == null)
            {
                throw new Exception();
            }
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();

            foreach (Appointment a in _appointmentRepository.FindAll())
            {
                if (a.DoctorUsername.Equals(username))
                {
                    retVal.Add(a);
                }
            }
            return retVal;

        }
        public ObservableCollection<Appointment> GetByPatient(string username)
        {
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (Appointment a in _appointmentRepository.FindAll())
            {
                if (a.PatientUsername.Equals(username))
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

        public List<ChartDataDTO> GetPopularTimes()
        {
            return _appointmentRepository.GetPopularTimes();
        }
    }
}