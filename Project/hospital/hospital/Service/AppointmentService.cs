using Model;
using System;

namespace Service
{
    public class AppointmentService
    {
        private readonly Repository.AppointmentRepository appointmentRepository;

        public Appointment Read(int id)
        {
            throw new NotImplementedException();
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

        public Appointment GetByPatient(string username)
        {
            throw new NotImplementedException();
        }

    }
}