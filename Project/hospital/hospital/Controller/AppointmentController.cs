using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Controller
{
    public class AppointmentController
    {
        private readonly Service.AppointmentService appointmentService;

        public AppointmentController()
        {
            appointmentService = new Service.AppointmentService();
        }

        public bool CreateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Appointment> GetAppointmentByPatient(string id)
        {
            // returns all appointments of one patient 
            return appointmentService.GetByPatient(id);
           
        }

        public ObservableCollection<Appointment> GetAppointmentByDoctor(string username)
        {
            return appointmentService.GetByDoctor(username);
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string username)
        {
            // returns doctors free appointments tomorrow
            return appointmentService.GetFreeAppointmentsByDoctor(username);
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppointment(Appointment oldAppointment, Appointment newAppointment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

    }
}