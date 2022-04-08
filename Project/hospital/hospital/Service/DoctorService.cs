using System;
using System.Collections.Generic;
using Model;


namespace Service
{
    public class DoctorService
    {
        private readonly Repository.DoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new Repository.DoctorRepository();
        }
        public List<DateTime> GetFreeTimeSlots()
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetDoctors()
        {
            return doctorRepository.FindAll();
        }

        public List<Doctor> GetGeneralPractitioners()
        {
            throw new NotImplementedException();
        }
    }
}