using System;
using System.Collections.Generic;
using Model;
using System.Collections.ObjectModel;
using Repository;

namespace Service
{
    public class DoctorService
    {
        private readonly DoctorRepository doctorRepository;

        public DoctorService(DoctorRepository _repo)
        {
            doctorRepository = _repo;
        }
        public DoctorService() { doctorRepository = new DoctorRepository(); }
        public List<DateTime> GetFreeTimeSlots()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return doctorRepository.FindAll();
        }

        public ObservableCollection<Doctor> GetGeneralPractitioners()
        {
            throw new NotImplementedException();
        }
        public Doctor GetByUsername(string username)
        {
            return doctorRepository.FindByUsername(username);
        }
        public Doctor getByName(string name)
        {
            string firstname = name.Split(' ')[0];
            string lastname = name.Split(' ')[1];
            return doctorRepository.getByName(firstname,lastname);
        }
        public void addPatientToDoctorsList(string patientId, string doctorUsername)
        {
            doctorRepository.addPatientToDoctorsList(patientId, doctorUsername);
        }
    }
}