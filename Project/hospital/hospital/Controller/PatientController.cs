using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService patientService;


        public PatientController()
        {
            patientService = new PatientService();
        }
        public bool Create(Model.Patient patient)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Patient> FindAll()
        {
           return patientService.FindAll();
        }

        public Patient FindById(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(string id)
        {
            return patientService.DeleteById(id);
        }

        public bool UpdateById(string id, Model.Patient patient)
        {
            throw new NotImplementedException();
        }

    }
}