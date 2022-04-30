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
        private readonly UserService userService;
        public Patient EditPatient { get; set; }

        public PatientController(PatientService _sevice,UserService uc)
        {
            patientService = _sevice;
            userService = uc;
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

        public bool UpdateByUsername(string username, Patient patient)
        {
            isValidate(username,patient);
            return patientService.UpdateById(username, patient);
        }

        private void isValidate(Patient patient)
        {
            if (patient.FirstName.Trim().Equals(""))
            {
                throw new Exception("Input first name");
            }

            if (patient.LastName.Trim().Equals(""))
            {
                throw new Exception("Input surname");
            }

            foreach (User p in userService.FindAll())
            {
                if (p.Username.Equals(patient.Username))
                {
                    throw new Exception("Username already exists !");
                }
            }
        }
        private void isValidate(string oldUsername,Patient patient)
        {
            if(patient.FirstName.Trim().Equals(""))
            {
                throw new Exception("Input first name");
            }

            if (patient.LastName.Trim().Equals(""))
            {
                throw new Exception("Input surname");
            }

           foreach(User p in userService.FindAll())
            {

                    if (p.Username.Equals(oldUsername))
                        return;
                if (p.Username.Equals(patient.Username))
                {
                    throw new Exception("Username already exists !");
                }
                
            }
        }
    }
}