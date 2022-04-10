using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
           isValidate(patient);
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

        private void isValidate(Patient patient)
        {
            if(patient.FirstName.Trim().Equals(""))
            {
                throw new Exception("Input first name");
            }
            else if (patient.FirstName.Trim().Any(char.IsDigit))
            {
                throw new Exception("number in fist name");
            }

            if (patient.LastName.Trim().Equals(""))
            {
                throw new Exception("Input surname");
            }
            else if (patient.LastName.Trim().Any(char.IsDigit))
            {
                throw new Exception("number in surname");
            }
        }
    }
}