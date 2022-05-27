using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository;
using hospital;
using System.Windows;
using Controller;
using System.Linq;

namespace Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepository appointmentRepository;
        private readonly DoctorRepository doctorRepository;
        private readonly UserController userController;
        private readonly NotificationRepository notificationRepository;
        private readonly DoctorService doctorService;
        private readonly RoomService _roomService;

        public AppointmentService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository, UserController userController, NotificationRepository notificationRepository, DoctorService doctorService,RoomService roomService)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.userController = userController;
            this.notificationRepository = notificationRepository;
            this.doctorService = doctorService;
            this._roomService = roomService;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string username)
        {
            List<DateTime> startTimes = new List<DateTime>();
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.DoctorUsername.Equals(username))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            DateTime tmrw = DateTime.Now.AddDays(1);
            // hospital starts working tomorrow at 7 and ends work at 19
            DateTime tomorrow = new DateTime(tmrw.Year, tmrw.Month, tmrw.Day, 7, 0, 0);
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (int i = 0; i < 24; i++)
            {
                allTimeSlots.Add(tomorrow.AddMinutes(i * 30));
            }
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.PatientUsername.Equals("peromir"))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            foreach (DateTime time in allTimeSlots)
            {
                App app = Application.Current as App;
                PatientController pc = app.patientController;
                retVal.Add(new Appointment(-1, username, userController.CurentLoggedUser.Username, time)); 
            }
            
            return retVal;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date,string patientUsername)
        {
            List<DateTime> startTimes = new List<DateTime>();
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            
            List<DateTime> allTimeSlots = new List<DateTime>();
            DateTime day = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            for (int i = 0; i < 24; i++)
            {
                allTimeSlots.Add(day.AddMinutes(i * 30));
            }
            
            // remove those appointments from available timeslots
            /*foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }*/

            // search all available timeslots
            foreach (DateTime time in allTimeSlots)
            {
                // look for every doctor 
                foreach (Doctor d in doctorRepository.FindAll())
                {
                    bool found = false;
                    // and search for every one of his appointments
                    foreach (Appointment a in GetByDoctor(d.Username))
                    {
                        if (a.StartTime == time)
                        {
                            found = true;
                        }
                    }
                    // if an appointment is not found than that timeslot is available so fill it
                    if (!found)
                    {
                        retVal.Add(new Appointment(-1, d.Username, patientUsername, time));
                    }
                }
            }

            foreach (Appointment appointment in retVal.ToList())
            {
                foreach (Appointment a in appointmentRepository.FindAll())
                {
                    if (a.PatientUsername.Equals(patientUsername))
                    {
                        if(appointment.StartTime == a.StartTime && appointment.PatientUsername.Equals(patientUsername)){
                            retVal.Remove(appointment);
                        }
                    }
                }
            }

            return retVal;

        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string username,string patientUsername)
        {
            List<DateTime> startTimes = new List<DateTime>();
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.DoctorUsername.Equals(username) && a.StartTime.Day == date.Day && a.StartTime.Month == date.Month && a.StartTime.Year == date.Year)
                {
                    startTimes.Add(a.StartTime);
                }
            }
            List<DateTime> allTimeSlots = new List<DateTime>();
            DateTime day = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            for (int i = 0; i < 24; i++)
            {
                allTimeSlots.Add(day.AddMinutes(i * 30));
            }
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.PatientUsername.Equals(patientUsername))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            foreach (DateTime time in allTimeSlots)
            {
                retVal.Add(new Appointment(-1, username, patientUsername, time));
            }
            return retVal;
        }

        public void Delete(int id)
        {
            appointmentRepository.DeleteById(id);
        }

        public void Create(Appointment appointment)
        {
            appointment.Id = appointmentRepository.GetNewId();
            appointmentRepository.Create(appointment);
        }

        public void Update(Appointment oldAppointment, Appointment newAppointment)
        {
            appointmentRepository.RemoveAppointment(oldAppointment);
            newAppointment.Id = oldAppointment.Id;
            newAppointment.DoctorUsername = oldAppointment.DoctorUsername;
            newAppointment.PatientUsername = oldAppointment.PatientUsername;
            newAppointment.RoomId = oldAppointment.RoomId;
            appointmentRepository.AddAppointment(newAppointment);
        }

        public ObservableCollection<Appointment> GetAll()
        {
            return appointmentRepository.FindAll();
        }

        public ObservableCollection<Appointment> GetByDoctor(string username)
        {
            if(appointmentRepository.FindAll() == null)
            {
                throw new Exception();
            }
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();

            foreach (Appointment a in appointmentRepository.FindAll())
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
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.PatientUsername.Equals(username))
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

    }
}