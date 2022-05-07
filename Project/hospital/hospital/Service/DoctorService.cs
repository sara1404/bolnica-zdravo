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

        public ObservableCollection<Doctor> GetDoctors()
        {
            return doctorRepository.FindAll();
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
        public void addOrdinationToDoctor(string doctorUsername, string ordinationId)
        {
            doctorRepository.addOrdinationToDoctor(doctorUsername, ordinationId);
        }

        public List<Doctor> GetDoctorsBySpecialization(Specialization requiredSpecialization)
        {
            List<Doctor> requiredDoctors = new List<Doctor>();

            foreach(Doctor doctor in doctorRepository.FindAll())
            {
                if(doctor.Specialization == requiredSpecialization)
                {
                    requiredDoctors.Add(doctor);
                }
            }
            return requiredDoctors;
        }
    }
}