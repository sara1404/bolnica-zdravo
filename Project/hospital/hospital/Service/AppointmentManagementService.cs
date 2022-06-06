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
    public class AppointmentManagementService
    {
        private readonly AppointmentRepository _appointmentRepository;

        public AppointmentManagementService(AppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
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

        public ObservableCollection<Appointment> GetPastAppointmentsByPatient(string id)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment a in GetByPatient(id))
            {
                if (a.StartTime < DateTime.Now)
                {
                    appointments.Add(a);
                }
            }
            return appointments;
        }

        public bool CanBeDelayed(Appointment appointment)
        {
            return DateTime.Now <= appointment.StartTime.AddHours(-24);
        }

        public ObservableCollection<Appointment> GetByDoctor(string doctorUsername)
        {
            if(_appointmentRepository.FindAll() == null)
            {
                throw new Exception();
            }
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();

            foreach (Appointment a in _appointmentRepository.FindAll())
            {
                if (a.DoctorUsername.Equals(doctorUsername))
                {
                    appointments.Add(a);
                }
            }
            return appointments;

        }
        public ObservableCollection<Appointment> GetByPatient(string patientUsername)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment a in _appointmentRepository.FindAll())
            {
                if (a.PatientUsername.Equals(patientUsername))
                {
                    appointments.Add(a);
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmenetsBetweenDate(DateTime startDate, DateTime endDate)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach(Appointment appointment in GetAll())
            {
                if(appointment.StartTime >= startDate && appointment.StartTime <= endDate)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }

        public List<ChartDataDTO> GetPopularTimes()
        {
            return _appointmentRepository.GetPopularTimes();
        }

    }
}