using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileHandler;
using Model;
using System.Linq;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public ObservableCollection<MedicalRecord> medicalRecords;
        public MedicalRecordFileHandler medicalRecordFileHandler;

        public MedicalRecordsRepository()
        {
            PatientRepository pr = new PatientRepository();
            DoctorRepository dp = new DoctorRepository();

            medicalRecords = new ObservableCollection<MedicalRecord>();
            medicalRecords.Add(new MedicalRecord("peromir", 333));
            medicalRecords.Add(new MedicalRecord("Ratko", 335));

/*             List<MedicalRecord> deserializedList = medicalRecordFileHandler.Read();
             if (deserializedList != null)
             {
                 medicalRecords = new ObservableCollection<MedicalRecord>(medicalRecordFileHandler.Read());
             }
             else
             {
                 medicalRecords = new ObservableCollection<MedicalRecord>();
             }*/
        }
        public bool Create(MedicalRecord medicalRecord)
        {
            medicalRecords.Add(medicalRecord);
            //medicalRecordFileHandler.Write(this.medicalRecords.ToList());
            return true;
        }

        public ObservableCollection<MedicalRecord> FindAll()
        {
            return medicalRecords;
        }

        public MedicalRecord FindById(int id)
        {
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord.RecordId.Equals(id))
                {
                    return medicalRecord;
                }
            }
            return null;
        }

        public bool DeleteById(int id)
        {
            bool retVal= medicalRecords.Remove(FindById(id));
            //medicalRecordFileHandler.Write(this.medicalRecords.ToList());
            return retVal;
        }

        public bool UpdateById(int id, MedicalRecord medicalRecord)
        {
            MedicalRecord med = FindById(id);
            med.DoctorUsername = medicalRecord.DoctorUsername;
            med.BloodType = medicalRecord.BloodType;
            med.Alergies = medicalRecord.Alergies;
            med.Note = medicalRecord.Note;
            //medicalRecordFileHandler.Write(this.medicalRecords.ToList());
            return true;
        }

        public bool AddTheraphy(int id, Therapy therapy)
        {
            FindById(id).Therapy.Add(therapy);
            return true;
        }


        public ObservableCollection<MedicalRecord> MedicalRecord
        {
            get
            {
                if (medicalRecords == null)
                {
                    medicalRecords = new ObservableCollection<MedicalRecord>();
                }

                return medicalRecords;
            }
            set
            {
                RemoveAllMedicalRecord();
                if (value != null)
                {
                    foreach (MedicalRecord oMedicalRecord in value)
                    {
                        AddMedicalRecord(oMedicalRecord);
                    }
                }
            }
        }


        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            if (newMedicalRecord == null)
            {
                return;
            }

            if (medicalRecords == null)
            {
                medicalRecords = new ObservableCollection<MedicalRecord>();
            }

            if (!medicalRecords.Contains(newMedicalRecord))
            {
                medicalRecords.Add(newMedicalRecord);
            }
        }


        public void RemoveMedicalRecord(MedicalRecord oldMedicalRecord)
        {
            if (oldMedicalRecord == null)
            {
                return;
            }

            if (medicalRecords != null)
            {
                if (medicalRecords.Contains(oldMedicalRecord))
                {
                    medicalRecords.Remove(oldMedicalRecord);
                }
            }
        }


        public void RemoveAllMedicalRecord()
        {
            if (medicalRecords != null)
            {
                medicalRecords.Clear();
            }
        }
        


    }
}