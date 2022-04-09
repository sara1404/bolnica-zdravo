using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
    public class PatientRepository
    {
        public FileHandler.PatientFileHandler patientFileHandler;
        public ObservableCollection<Patient> patient;
        public PatientRepository()
        {
            patient = new ObservableCollection<Patient>();
            patient.Add(new Patient("peromir","perica","123","Pera", "Peric", "peromir","052152151"));
        }
        public bool Create(Patient patient)
        {
            Console.WriteLine(patient.FirstName);
            this.patient.Add(patient);
            return true;
        }

        public ObservableCollection<Patient> FindAll()
        {
            return patient;
        }

        public Patient FindById(string id)
        {
            foreach (Patient p in patient)
            {
                if (p.Id.Equals(id))
                {
                    return p;
                }
            }
            return null;
        }

        public bool DeleteById(string id)
        {
            foreach (Patient p in patient)
                Console.WriteLine(p.Username);

            Console.WriteLine("AAAA");
            return patient.Remove(FindById(id));
        }

        public bool UpdateById(string id, Patient patient)
        {
            DeleteById(id);
            Create(patient);
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