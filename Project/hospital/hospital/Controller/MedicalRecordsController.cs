using Model;
using System;
using Service;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Controller
{
    public class MedicalRecordsController
    {
        private readonly MedicalRecordsService medicalRecordsService;
        public int RecordId { get; set; }
        public MedicalRecordsController(MedicalRecordsService _service) { medicalRecordsService = _service; }
        public bool Create(MedicalRecord medicalRecord)
        {
            return medicalRecordsService.Create(medicalRecord);
        }

        public ObservableCollection<MedicalRecord> FindAll()
        {
            return medicalRecordsService.FindAll();
        }

        public MedicalRecord FindById(int id)
        {
            return medicalRecordsService.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return medicalRecordsService.DeleteById(id);
        }

        public bool UpdateById(int id, MedicalRecord medicalRecord)
        {
            return medicalRecordsService.UpdateById(id, medicalRecord);
        }
        public bool AddTheraphy(int id, Therapy therapy)
        {
            return medicalRecordsService.AddTheraphy(id, therapy);
        }
        public bool CheckAllergies(int recordId, Medicine medicine)
        {
            MedicalRecord record = FindById(recordId);
            List<string> allergies = new List<string>();
            if (record.Alergies != null)
                allergies = record.Alergies.Split(',').ToList<string>();
            foreach (string ingridient in medicine.Ingridients)
            {
                if (allergies.Contains(ingridient) || allergies.Contains(medicine.Name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}