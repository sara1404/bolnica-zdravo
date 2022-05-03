using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository;
using hospital;
using System.Windows;
using Controller;

namespace Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepository appointmentRepository;
        private readonly DoctorRepository doctorRepository;
        private readonly UserController userController;
        private readonly NotificationRepository notificationRepository;

        public AppointmentService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository, UserController userController, NotificationRepository notificationRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.userController = userController;
            this.notificationRepository = notificationRepository;
        }

        public Appointment Read(int id)
        {
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.Id == id)
                {
                    return a;
                }
            }
            return null;
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
                if (a.patientUsername.Equals("peromir"))
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
            // add logged in patients appointments
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.patientUsername.Equals(patientUsername))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            // remove those appointments from available timeslots
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }

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
                if (a.patientUsername.Equals(patientUsername))
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
                DoctorController dc = app.doctorController;
                retVal.Add(new Appointment(-1, username, patientUsername, time));
                
            }
            return retVal;
        }
        
        /*
         -change var to DateTime
         -remove userController from constructor
         */
        public ObservableCollection<Appointment> GetRecommendedByDoctor(DateTime startDate, DateTime endDate, Doctor doctor)
        {
            List<DateTime> startTimes = new List<DateTime>();
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            // take all of the appointments of chosen doctor
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.DoctorUsername.Equals(doctor.Username))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            // take all possible appointments
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (DateTime dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1))
            {
                DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                for (int i = 0; i < 24; i++)
                {
                    allTimeSlots.Add(day.AddMinutes(i * 30));
                }
            }
            // remove that doctors appointments from all possible appointments
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            if (allTimeSlots.Count == 0)
            {
                // if that doctor is too busy than expand the timeframe
                DateTime startDatePrev = startDate.CompareTo(DateTime.Today.AddDays(-4)) > 0 ? DateTime.Today.AddDays(1) : startDate.AddDays(-4);
                DateTime endDatePrev = startDate;
                for (DateTime dayIt = startDatePrev; dayIt <= endDatePrev; dayIt = dayIt.AddDays(1))
                {
                    DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                    for (int i = 0; i < 24; i++)
                    {
                        allTimeSlots.Add(day.AddMinutes(i * 30));
                    }
                }

                DateTime startDatePost = endDate;
                DateTime endDatePost = endDate.AddDays(4);
                for (DateTime dayIt = startDatePost; dayIt <= endDatePost; dayIt = dayIt.AddDays(1))
                {
                    DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                    for (int i = 0; i < 24; i++)
                    {
                        allTimeSlots.Add(day.AddMinutes(i * 30));
                    }
                }
            } 
            // add patients appointments to the start times
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.patientUsername.Equals(userController.CurentLoggedUser.Username))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            // remove all taken timeslots
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }

            // offer the patient all found appointments
            foreach (DateTime time in allTimeSlots)
            {
                retVal.Add(new Appointment(-1, doctor.Username, userController.CurentLoggedUser.Username, time));
            }
            return retVal;
        }

        public ObservableCollection<Appointment> GetRecommendedByDate(DateTime startDate, DateTime endDate, Doctor doctor)
        {
            List<DateTime> startTimes = new List<DateTime>();
            List<Appointment> retVal = new List<Appointment>();
            // take all of the patients appointments
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.patientUsername.Equals(userController.CurentLoggedUser.Username))
                {
                    startTimes.Add(a.StartTime);
                }
            }
            // add timeslots
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (DateTime dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1))
            {
                DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                for (int i = 0; i < 24; i++)
                {
                    allTimeSlots.Add(day.AddMinutes(i * 30));
                }
            }

            bool foundDefaultDoctorsAppointment = false;
            // remove the patients appointments from all timeslots
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }

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
                        if (d.Username.Equals(doctor.Username))
                        {
                            foundDefaultDoctorsAppointment = true;
                        }
                        retVal.Add(new Appointment(-1, d.Username, userController.CurentLoggedUser.Username, time));
                    }
                }
            }
            // if the doctor that we asked for has available appointments than we will remove all other doctors
            if (foundDefaultDoctorsAppointment)
            {
                retVal.RemoveAll(s => !s.DoctorUsername.Equals(doctor.Username));
            }

            ObservableCollection<Appointment> retValObserve = new ObservableCollection<Appointment>(retVal);
            return retValObserve;
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
            newAppointment.roomId = oldAppointment.roomId;
            appointmentRepository.AddAppointment(newAppointment);
        }

        public ObservableCollection<Appointment> GetAll()
        {
            return appointmentRepository.FindAll();
        }

        // do not delete or change this method because it is used in GetRecommendedAppointments
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
            /*List<Appointment> otherPatients = new List<Appointment>();
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (!a.PatientUsername.Equals(username))
                {
                    // finding all other patients
                    otherPatients.Add(a);
                }
            }
            foreach (Appointment a in otherPatients)
            {
                // deleting those other patients
                appointmentRepository.DeleteById(a.Id);
            }
            //return retVal;*/
            return appointmentRepository.FindAll();
        }

        public ObservableCollection<Appointment> GetByDoctorSpecialization(Specialization specialization)
        {
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (doctorRepository.FindByUsername(a.doctorUsername).Specialization == specialization)
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }



        //secretary function for appointment--------------
        public bool tryMakeAppointment(string _hours, string _minuts, string patientUsername, string roomId,DateTime date,Doctor doctor)
        {
            ObservableCollection<Appointment> apointments = GetFreeAppointmentsByDateAndDoctor(date, doctor.Username,patientUsername);
            for (int i = 0; i < apointments.Count; i++)
            {
                string time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                string hours = time.Split(':')[0];
                string minuts = time.Split(':')[1];
                if (_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    //stavi id sobee kad sazanas
                    (apointments[i]).patientUsername = patientUsername;
                    (apointments[i]).roomId = roomId;
                    //pozovi controller i napravi appointment
                    Create(apointments[i]);
                    // this.Visibility = Visibility.Collapsed;
                    return true;
                }
            }
            return false;
        }
        public Appointment RecommendedOne { set; get; }
        public Appointment RecommendedTwo { set; get; }
        public void findFreeForward(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;
                _hours = ++_hours;
            }
            else if (_minuts == 0)
            {
                _minuts = 30;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i = 0; i < apointments.Count; i++)
            {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours) == (_hours % 13) && Int32.Parse(itemMinuts) == _minuts)
                {
                    RecommendedOne = apointments[i];
                    return;
                }
            }
            findFreeForward(apointments, _hours.ToString(), _minuts.ToString());
        }
        public void findFreeBack(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;

            }
            else if (_minuts == 0)
            {
                _minuts = 30;
                _hours = --_hours;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i = 0; i < apointments.Count; i++)
            {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours) == (_hours % 12) && Int32.Parse(itemMinuts) == _minuts)
                {
                    RecommendedTwo = apointments[i];
                    return;
                }
            }
            findFreeBack(apointments, _hours.ToString(), _minuts.ToString());

        }

        public void findRecByTime(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            bool oneRecFilled = false;
            foreach (Appointment item in apointments)
            {
                Console.WriteLine(item.doctorUsername +" " + item.StartTime);
            }
            foreach (Appointment appointment in apointments)
            {
                string time = (appointment).StartTime.ToString();
                time = time.Split(' ')[1];
                string _hours = time.Split(':')[0];
                string _minuts = time.Split(':')[1];
                if (_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    if (oneRecFilled == false)
                    {
                        RecommendedOne = appointment;
                        oneRecFilled = true;
                    }
                    else
                    {
                        RecommendedTwo = appointment;
                        return;
                    }
                }
            }
            //ako nije uspeo naci tacno taj nek nadje neke najblize
            findFreeBack(apointments,hours,minuts);
            if(!oneRecFilled)
                findFreeForward(apointments,hours,minuts);
        }

        public bool tryChangeAppointment(Appointment oldAppointment,DateTime newDate,string newTime)
        {
            //svi SLOBODNi pregledi tog dana za tog doktora
            ObservableCollection<Appointment> appointments = GetFreeAppointmentsByDateAndDoctor(newDate,oldAppointment.doctorUsername,oldAppointment.patientUsername);
            //zeljeno vreme
            string newHours = newTime.Split(':')[0];
            string newMinuts =newTime.Split(':')[1];
            for (int i = 0; i < appointments.Count; i++)
            {
                string time = (appointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                string hours = time.Split(':')[0];
                string minuts = time.Split(':')[1];
                if (newHours.Equals(hours) && newMinuts.Equals(minuts))
                {
                    Appointment newAppointmet = appointments[i];
                    newAppointmet.patientUsername = oldAppointment.patientUsername;
                    Update(oldAppointment, newAppointmet);
                    notificationRepository.Create(new Notification(oldAppointment.PatientUsername));
                    notificationRepository.Create(new Notification(oldAppointment.DoctorUsername));
                    return true;
                }
            }
            return false;
        }
        //------------------------------------------------
    }
}