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


        public PatientService() { patientRepository = new PatientRepository(); }
        public bool Create(Patient patient)
        {
            throw new NotImplementedException();
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