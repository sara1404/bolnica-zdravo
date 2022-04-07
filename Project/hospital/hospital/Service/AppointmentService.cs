using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class AppointmentService
    {
        private readonly Repository.AppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new Repository.AppointmentRepository();
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
                if (a.patient.Id.Equals(username))
                {
                    retVal.Add(a);
                }
            }
            return retVal;
        }

    }
}