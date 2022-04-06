using System;

namespace Repository
{
   public class MedicalRecordsRepository
   {
      public bool Create(MedicalRecord medicalRecord)
      {
         throw new NotImplementedException();
      }
      
      public List<MedicalRecord> FindAll()
      {
         throw new NotImplementedException();
      }
      
      public MedicalRecord FindById(String id)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteById(String id)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateById(String id, MedicalRecord medicalRecord)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.Generic.List<MedicalRecord> medicalRecord;
      
      public System.Collections.Generic.List<MedicalRecord> MedicalRecord
      {
         get
         {
            if (medicalRecord == null)
               medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
            return medicalRecord;
         }
         set
         {
            RemoveAllMedicalRecord();
            if (value != null)
            {
               foreach (Model.MedicalRecord oMedicalRecord in value)
                  AddMedicalRecord(oMedicalRecord);
            }
         }
      }
      
      
      public void AddMedicalRecord(Model.MedicalRecord newMedicalRecord)
      {
         if (newMedicalRecord == null)
            return;
         if (this.medicalRecord == null)
            this.medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
         if (!this.medicalRecord.Contains(newMedicalRecord))
            this.medicalRecord.Add(newMedicalRecord);
      }
      
      
      public void RemoveMedicalRecord(Model.MedicalRecord oldMedicalRecord)
      {
         if (oldMedicalRecord == null)
            return;
         if (this.medicalRecord != null)
            if (this.medicalRecord.Contains(oldMedicalRecord))
               this.medicalRecord.Remove(oldMedicalRecord);
      }
      
      
      public void RemoveAllMedicalRecord()
      {
         if (medicalRecord != null)
            medicalRecord.Clear();
      }
      public FileHandler.MedicalRecordFileHandler medicalRecordFileHandler;
   
   }
}