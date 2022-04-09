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
        public Patient EditPatient { get; set; }

        public PatientController(PatientService _sevice)
        {
            patientService = _sevice;
        }
        public bool Create(Patient patient)
        {
           return patientService.Create(patient);
        }

        public ObservableCollection<Patient> FindAll()
        {
           return patientService.FindAll();
        }

        public Patient FindById(string id)
        {
            return patientService.FindById(id);
        }

        public bool DeleteById(string id)
        {
            return patientService.DeleteById(id);
        }

        public bool UpdateById(string id, Model.Patient patient)
        {
            return patientService.UpdateById(id, patient);
        }

    }
}