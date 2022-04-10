using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
           // patient = new ObservableCollection<Patient>();
           // patient.Add(new Patient("peromir","perica","123","Pera", "Peric", "21312312","052152151"));
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