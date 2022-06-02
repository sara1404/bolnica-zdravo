using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Service;
using hospital.DTO;

namespace Controller
{
    public class AppointmentController
    {
        private readonly Service.AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this._appointmentService = appointmentService;
        }

        public bool CreateAppointment(Appointment appointment)
        {
            _appointmentService.Create(appointment);
            return true;
        }

        public ObservableCollection<Appointment> GetAppointmentByPatient(string id)
        {
            return _appointmentService.GetByPatient(id);
        }

        // service
        public ObservableCollection<Appointment> GetPastAppointmentsByPatient(string id)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach(Appointment a in _appointmentService.GetByPatient(id))
            {
                if(a.StartTime < DateTime.Now)
                {
                    appointments.Add(a);
                }
            }
            return appointments;
        }

        public ObservableCollection<Appointment> GetAppointmentByDoctor(string username)
        {
            return _appointmentService.GetByDoctor(username);
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return _appointmentService.GetAll();
        }

        public bool UpdateAppointment(Appointment oldAppointment, Appointment newAppointment)
        {
            _appointmentService.Update(oldAppointment, newAppointment);
            return true;
        }

        // service
        public bool CanBeDelayed(Appointment appointment)
        {
            return DateTime.Now <= appointment.StartTime.AddHours(-24);
        }

        public bool DeleteAppointment(int id)
        {
            _appointmentService.Delete(id);
            return true;
        }

        public List<ChartDataDTO> GetPopularTimes()
        {
            return _appointmentService.GetPopularTimes();
        }
    }
}