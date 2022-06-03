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
    public class AppointmentManagementController
    {
        private readonly Service.AppointmentManagementService _appointmentService;

        public AppointmentManagementController(AppointmentManagementService appointmentService)
        {
            _appointmentService = appointmentService;
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

        public ObservableCollection<Appointment> GetPastAppointmentsByPatient(string patientId)
        {
            return _appointmentService.GetPastAppointmentsByPatient(patientId);
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

        public bool CanBeDelayed(Appointment appointment)
        {
            return _appointmentService.CanBeDelayed(appointment);
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