using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Service;

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

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string username)
        {
            // returns doctors free appointments tomorrow
            return appointmentService.GetFreeAppointmentsByDoctor(username);
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date,string patientUsername)
        {
            return appointmentService.GetFreeAppointmentsByDate(date, patientUsername);
        }
        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string username,string patientUsername)
        {
            return appointmentService.GetFreeAppointmentsByDateAndDoctor(date, username, patientUsername);
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
        public ObservableCollection<Appointment> GetRecommendedByDoctor(DateTime startDate, DateTime endDate, Doctor doctor)
        {
            return appointmentService.GetRecommendedByDoctor(startDate, endDate, doctor);
        }
        public ObservableCollection<Appointment> GetRecommendedByDate(DateTime startDate, DateTime endDate, Doctor doctor)
        {
            return appointmentService.GetRecommendedByDate(startDate, endDate, doctor);
        }

        public bool DeleteAppointment(int id)
        {
            appointmentService.Delete(id);
            return true;
        }


        //secretary function for appointment--------------
        public bool tryMakeAppointment(string _hours,string _minuts,string patientUsername,string roomId, DateTime date, Doctor doctor)
        {
            return appointmentService.tryMakeAppointment(_hours, _minuts, patientUsername, roomId,date,doctor);
        }
        public void findFreeForward(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            appointmentService.findFreeForward(apointments, hours, minuts);
        }
        public Appointment RecommendedOne {
            set { appointmentService.RecommendedOne = value; }
            get { return appointmentService.RecommendedOne; }
        }
        public Appointment RecommendedTwo {
            set { appointmentService.RecommendedTwo = value; }
            get { return appointmentService.RecommendedTwo; }
        }
        public void findFreeBack(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            appointmentService.findFreeBack(apointments, hours,minuts);
        }
        public void findRecByTime(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            appointmentService.findRecByTime(apointments, hours,minuts);
        }
        public bool tryChangeAppointment(Appointment oldAppointment, DateTime newDate, string newTime)
        {
            return appointmentService.tryChangeAppointment(oldAppointment, newDate, newTime);
        }
        public void tryMakeEmergencyAppointment(string patientUsername, Specialization requiredSpecialization,bool isOperation)
        {
            appointmentService.tryMakeEmergencyAppointment(patientUsername, requiredSpecialization,isOperation);
        }
        //------------------------------------------------

    }
}