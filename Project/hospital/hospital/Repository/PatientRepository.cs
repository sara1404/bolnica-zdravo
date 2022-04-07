using System;
using Model;
using System.Collections.Generic;

namespace Repository
{
   public class PatientRepository
   {
      public bool Create(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public List<Patient> FindAll()
      {
         throw new NotImplementedException();
      }
      
      public Patient FindById(String id)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteById(String id)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateById(String id, Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public FileHandler.PatientFileHandler patientFileHandler;
      public System.Collections.Generic.List<Patient> patient;
      
      
      public System.Collections.Generic.List<Patient> Patient
      {
         get
         {
            if (patient == null)
               patient = new System.Collections.Generic.List<Patient>();
            return patient;
         }
         set
         {
            RemoveAllPatient();
            if (value != null)
            {
               foreach (Model.Patient oPatient in value)
                  AddPatient(oPatient);
            }
         }
      }
      
      
      public void AddPatient(Model.Patient newPatient)
      {
         if (newPatient == null)
            return;
         if (this.patient == null)
            this.patient = new System.Collections.Generic.List<Patient>();
         if (!this.patient.Contains(newPatient))
            this.patient.Add(newPatient);
      }
      
      
      public void RemovePatient(Model.Patient oldPatient)
      {
         if (oldPatient == null)
            return;
         if (this.patient != null)
            if (this.patient.Contains(oldPatient))
               this.patient.Remove(oldPatient);
      }
      
      
      public void RemoveAllPatient()
      {
         if (patient != null)
            patient.Clear();
      }
   
   }
}