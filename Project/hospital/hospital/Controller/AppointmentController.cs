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
        private readonly Service.AppointmentService appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public bool CreateAppointment(Appointment appointment)
        {
            appointmentService.Create(appointment);
            return true;
        }

        public ObservableCollection<Appointment> GetAppointmentByPatient(string id)
        {
            return appointmentService.GetByPatient(id);
        }

        public ObservableCollection<Appointment> GetAppointmentByDoctor(string username)
        {
            return appointmentService.GetByDoctor(username);
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return appointmentService.GetAll();
        }

        public bool UpdateAppointment(Appointment oldAppointment, Appointment newAppointment)
        {
            appointmentService.Update(oldAppointment, newAppointment);
            return true;
        }

        public bool CanBeDelayed(Appointment appointment)
        {
            DateTime now = DateTime.Now;
            if(now <= appointment.StartTime.AddHours(-24))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAppointment(int id)
        {
            appointmentService.Delete(id);
            return true;
        }

        public List<ChartDataDTO> GetPopularTimes()
        {
            return appointmentService.GetPopularTimes();
        }
    }
}