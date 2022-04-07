using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class PatientRepository
    {
        public FileHandler.PatientFileHandler patientFileHandler;
        public List<Patient> patient;
        public PatientRepository()
        {
            patient = new List<Patient>();
            patient.Add(new Patient("Pera", "Peric", "peromir"));
        }
        public bool Create(Patient patient)
        {
            throw new NotImplementedException();
        }

        public List<Patient> FindAll()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool UpdateById(string id, Patient patient)
        {
            throw new NotImplementedException();
        }


        public List<Patient> Patient
        {
            get
            {
                if (patient == null)
                {
                    patient = new System.Collections.Generic.List<Patient>();
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
                patient = new System.Collections.Generic.List<Patient>();
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