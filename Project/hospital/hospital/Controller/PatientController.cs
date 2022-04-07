using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService patientService;

        public bool Create(Model.Patient patient)
        {
            throw new NotImplementedException();
        }

        public List<Patient> FindAll()
        {
            throw new NotImplementedException();
        }

        public Patient FindById(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(string id, Model.Patient patient)
        {
            throw new NotImplementedException();
        }

    }
}