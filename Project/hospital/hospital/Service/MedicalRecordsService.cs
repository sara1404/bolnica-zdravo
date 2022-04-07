using System;
using Model;
using Repository;
using System.Collections.Generic;

namespace Service
{
   public class MedicalRecordsService
   {
      private MedicalRecordsRepository medicalRecordsRepository;
      
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
   
   }
}