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

        public AppointmentService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
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
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            foreach (DateTime time in allTimeSlots)
            {
                App app = Application.Current as App;
                PatientController pc = app.patientController;
                retVal.Add(new Appointment(-1, doctorRepository.FindByUsername(username).Username, pc.FindById("peromir").Username, time)); //odavde izbaciti pacijenta
            }
            
            return retVal;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date)
        {
            List<DateTime> startTimes = new List<DateTime>();
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.StartTime.Day == date.Day && a.StartTime.Month == date.Month && a.StartTime.Year == date.Year)
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
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            foreach (DateTime time in allTimeSlots)
            {
                App app = Application.Current as App;
                PatientController pc = app.patientController;
                foreach (Doctor d in doctorRepository.FindAll())
                {
                    retVal.Add(new Appointment(-1, d.Username, pc.FindById("peromir").Username, time));
                }
            }

            return retVal;

        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string username)
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
            foreach (DateTime time in startTimes)
            {
                allTimeSlots.Remove(time);
            }
            foreach (DateTime time in allTimeSlots)
            {
                App app = Application.Current as App;
                PatientController pc = app.patientController;
                DoctorController dc = app.doctorController;
                retVal.Add(new Appointment(-1, username, pc.FindById("peromir").Username, time));
                
            }
            return retVal;
        }
        
        public ObservableCollection<Appointment> GetRecommendedAppointments(DateTime startDate, DateTime endDate, Doctor doctor, bool priority)
        {
            if (priority)   // priority is doctor
            {
                List<DateTime> startTimes = new List<DateTime>();
                ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
                foreach (Appointment a in appointmentRepository.FindAll())
                {
                    if (a.DoctorUsername.Equals(doctor.Username))
                    {
                        startTimes.Add(a.StartTime);
                    }
                }
                //DateTime tmrw = DateTime.Now.AddDays(1);
                //DateTime tomorrow = new DateTime(tmrw.Year, tmrw.Month, tmrw.Day, 7, 0, 0);
                List<DateTime> allTimeSlots = new List<DateTime>();
                for (var dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1)) {
                    DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                    for (int i = 0; i < 24; i++)
                    {
                        allTimeSlots.Add(day.AddMinutes(i * 30));
                    }
                }
                foreach (DateTime time in startTimes)
                {
                    allTimeSlots.Remove(time);
                }
                if(allTimeSlots.Count == 0)
                {
                    DateTime startDatePrev = startDate.CompareTo(DateTime.Today.AddDays(-4)) > 0 ? DateTime.Today.AddDays(1) : startDate.AddDays(-4);
                    DateTime endDatePrev = startDate;
                    for (var dayIt = startDatePrev; dayIt <= endDatePrev; dayIt = dayIt.AddDays(1))
                    {
                        DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                        for (int i = 0; i < 24; i++)
                        {
                            allTimeSlots.Add(day.AddMinutes(i * 30));
                        }
                    }

                    DateTime startDatePost = endDate;
                    DateTime endDatePost = endDate.AddDays(4);
                    for (var dayIt = startDatePost; dayIt <= endDatePost; dayIt = dayIt.AddDays(1))
                    {
                        DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                        for (int i = 0; i < 24; i++)
                        {
                            allTimeSlots.Add(day.AddMinutes(i * 30));
                        }
                    }
                }
                foreach (DateTime time in startTimes)
                {
                    allTimeSlots.Remove(time);
                }

                foreach (DateTime time in allTimeSlots)
                {
                    retVal.Add(new Appointment(-1, doctor.Username, "peromir", time));
                }
                return retVal;
            }
            else
            {
                List<DateTime> startTimes = new List<DateTime>();
                ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
                foreach (Appointment a in appointmentRepository.FindAll())
                {
                    if (a.DoctorUsername.Equals(doctor.Username))
                    {
                        startTimes.Add(a.StartTime);
                    }
                }
                List<DateTime> allTimeSlots = new List<DateTime>();
                for (var dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1))
                {
                    DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                    for (int i = 0; i < 24; i++)
                    {
                        allTimeSlots.Add(day.AddMinutes(i * 30));
                    }
                }
                foreach (DateTime time in startTimes)
                {
                    allTimeSlots.Remove(time);
                }

                if(allTimeSlots.Count == 0)
                {
                    for (var dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1))
                    {
                        DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                        for (int i = 0; i < 24; i++)
                        {
                            allTimeSlots.Add(day.AddMinutes(i * 30));
                        }
                    }
                    foreach (Appointment a in appointmentRepository.FindAll())
                    {
                        startTimes.Add(a.StartTime);
                    }
                    foreach (DateTime time in startTimes)
                    {
                        allTimeSlots.Remove(time);
                    }
                }
                
                foreach (DateTime time in allTimeSlots)
                {
                    retVal.Add(new Appointment(-1, doctor.Username, "peromir", time));
                }
                return retVal;
            }
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
            //newAppointment.operationRoom = oldAppointment.operationRoom;
            appointmentRepository.AddAppointment(newAppointment);
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
                if (a.DoctorUsername.Equals(username)) // change to Id to Username
                {
                    retVal.Add(a);
                }
            }
            return retVal;

        }

        public ObservableCollection<Appointment> GetByPatient(string username)
        {
            List<Appointment> otherPatients = new List<Appointment>();
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
            //return retVal;
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
    }
}