using hospital.DTO;
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

        public void BlockPatient(string patientUsername)
        {
            Patient currentPatient = FindById(patientUsername);
            currentPatient.IsBlocked = true;
            User blockedUser = userService.FindByUsername(patientUsername);
            blockedUser.IsBlocked = true;
            userService.UpdateByUsername(patientUsername, blockedUser);
        }

        public bool IsTroll(string patientUsername)
        {
            Patient currentPatient = FindById(patientUsername);
            int delayOrCancelCnt = 0;
            foreach (DateTime time in currentPatient.DelayOrCancelAppointment)
            {
                if (time >= DateTime.Now.AddDays(-30))
                {
                    delayOrCancelCnt++;
                }
            }
            if (delayOrCancelCnt >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddDelayOrCancelAppointment(string patientUsername)
        {
            Patient currentPatient = FindById(patientUsername);
            currentPatient.DelayOrCancelAppointment.Add(DateTime.Now);
            UpdateByUsername(patientUsername, currentPatient);
        }

        public List<TherapyDTO> FindCurrentMonthTherapies(string id)
        {
            return patientService.FindCurrentMonthTherapies(id);
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