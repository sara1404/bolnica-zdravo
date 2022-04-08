using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private readonly Repository.AppointmentRepository appointmentRepository;
        private readonly Repository.DoctorRepository doctorRepository;

        public AppointmentService()
        {
            appointmentRepository = new Repository.AppointmentRepository();
            doctorRepository = new Repository.DoctorRepository();
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
        public List<Appointment> GetFreeAppointmentsByDoctor(string username)
        {
            List<DateTime> startTimes = new List<DateTime>();
            List<Appointment> retVal = new List<Appointment>();
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
            throw new NotImplementedException();
        }

        public void Create(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Appointment GetByDoctor(string username)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetByPatient(string username)
        {
            List<Appointment> retVal = new List<Appointment>();

            foreach (Appointment a in appointmentRepository.FindAll())
            {
                if (a.patient.Id.Equals(username)) // change to Id to Username
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

    }
}