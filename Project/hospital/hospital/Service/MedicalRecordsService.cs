using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class MedicalRecordsService
    {
        private readonly MedicalRecordsRepository medicalRecordsRepository;

        public MedicalRecordsService(MedicalRecordsRepository _repo)
        {
            medicalRecordsRepository = _repo;
        }
        public bool Create(MedicalRecord medicalRecord)
        {
            return medicalRecordsRepository.Create(medicalRecord); 

        }

        public ObservableCollection<MedicalRecord> FindAll()
        {
             return medicalRecordsRepository.FindAll();
        }

        public MedicalRecord FindById(int id)
        {
            return medicalRecordsRepository.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return medicalRecordsRepository.DeleteById(id);
        }

        public bool UpdateById(int id, MedicalRecord medicalRecord)
        {
            return medicalRecordsRepository.UpdateById(id, medicalRecord);
        }
        public bool AddTheraphy(int id, Therapy therapy)
        {
            return medicalRecordsRepository.AddTheraphy(id, therapy);
        }

    }
}