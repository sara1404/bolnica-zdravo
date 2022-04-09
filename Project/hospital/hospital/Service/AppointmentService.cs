using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository;

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
            Doctor d;
            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.doctor.Username.Equals(username))
                {
                    startTimes.Add(a.StartTime);
                    d = a.doctor;
                }
            }
            DateTime tmrw = DateTime.Now.AddDays(1);
            // hospital starts working tomorrow at 7 and ends work at 19
            DateTime tomorrow = new DateTime(tmrw.Year, tmrw.Month, tmrw.Day, 7, 0, 0);
            for (int i = 0; i < 24; i++)
            {
                foreach (DateTime time in startTimes)
                {
                    if (tomorrow.AddMinutes(i * 30).CompareTo(time) != 0)
                    {
                        DateTime startTime = tomorrow.AddMinutes(i * 30);
                        retVal.Add(new Appointment(-1, doctorRepository.FindByUsername(username), null, startTime));
                    }
                }
            }
            return retVal;
        }

        public void Delete(int id)
        {
            appointmentRepository.DeleteById(id);
        }

        public void Create(Appointment appointment)
        {
            appointment.Id = appointmentRepository.GetAppointmentNumber() + 1;
            appointmentRepository.Create(appointment);
        }

        public void Update(Appointment appointment)
        {
            throw new NotImplementedException();
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
                if (a.doctor.Username == username) // change to Id to Username
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
                if (!a.patient.Id.Equals(username)) // change to Id to Username
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

    }
}