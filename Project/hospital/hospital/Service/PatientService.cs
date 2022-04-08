using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class PatientService
    {
        private readonly PatientRepository patientRepository;


        public PatientService(PatientRepository _repo) { patientRepository = _repo; }
        public bool Create(Patient patient)
        {
            return patientRepository.Create(patient);
        }

        public ObservableCollection<Patient> FindAll()
        {
            return patientRepository.FindAll();
        }

        public Patient FindById(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(string id)
        {
            return patientRepository.DeleteById(id);
        }

        public bool UpdateById(string id, Patient patient)
        {
            throw new NotImplementedException();
        }

    }
}