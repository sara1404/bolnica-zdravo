using hospital;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Repository
{
    public class PatientRepository
    {
        public FileHandler.PatientFileHandler patientFileHandler;
        public ObservableCollection<Patient> patient;
        public PatientRepository()
        {
            patientFileHandler = new FileHandler.PatientFileHandler();

            List<Patient> deserializedList = patientFileHandler.Read();
            if (deserializedList != null)
            {
                patient = new ObservableCollection<Patient>(patientFileHandler.Read());
            }
            else
            {
                patient = new ObservableCollection<Patient>();
                
            }
        }
        public bool Create(Patient patient)
        {
            this.patient.Add(patient);
            patientFileHandler.Write(this.patient.ToList());
            return true;
        }

        public ObservableCollection<Patient> FindAll()
        {
            return patient;
        }

        public Patient FindById(string username)
        {
            foreach (Patient p in patient)
            {
                if (p.Username.Equals(username))
                {
                    return p;
                }
            }
            return null;
        }

        public bool DeleteById(string id)
        {
            bool reVal= patient.Remove(FindById(id));
            patientFileHandler.Write(this.patient.ToList());
            return reVal;
        }

        public bool UpdateById(string id, Patient patient)
        {
            Patient oldPatient=FindById(id);
            oldPatient.Username = patient.Username;
            oldPatient.FirstName = patient.FirstName;
            oldPatient.LastName = patient.LastName; 
            oldPatient.Email = patient.Email;
            oldPatient.Id = patient.Id; 
            oldPatient.DateOfBirth = patient.DateOfBirth;
            oldPatient.PhoneNumber = patient.PhoneNumber;
            oldPatient.IsBlocked = patient.IsBlocked;
            oldPatient.Gender = patient.Gender;
            patientFileHandler.Write(this.patient.ToList());
            return true;
        }


        public ObservableCollection<Patient> Patient
        {
            get
            {
                if (patient == null)
                {
                    patient = new ObservableCollection<Patient>();
                }

                return patient;
            }
            set
            {
                RemoveAllPatient();
                if (value != null)
                {
                    foreach (Model.Patient oPatient in value)
                    {
                        AddPatient(oPatient);
                    }
                }
            }
        }


        public void AddPatient(Model.Patient newPatient)
        {
            if (newPatient == null)
            {
                return;
            }

            if (patient == null)
            {
                patient = new ObservableCollection<Patient>();
            }

            if (!patient.Contains(newPatient))
            {
                patient.Add(newPatient);
            }
        }


        public void RemovePatient(Model.Patient oldPatient)
        {
            if (oldPatient == null)
            {
                return;
            }

            if (patient != null)
            {
                if (patient.Contains(oldPatient))
                {
                    patient.Remove(oldPatient);
                }
            }
        }


        public void RemoveAllPatient()
        {
            if (patient != null)
            {
                patient.Clear();
            }
        }

    }
}