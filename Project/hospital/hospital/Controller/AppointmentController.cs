using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {
        private readonly Service.AppointmentService appointmentService;

        public bool CreateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentByPatient(string id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentByDoctor(string username)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointments()
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