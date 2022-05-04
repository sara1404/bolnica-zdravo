using Model;
using System;
using Service;
using System.Collections.ObjectModel;

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
    }
}