using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FileHandler;
using Model;
namespace Repository
{
    public class MedicalRecordsRepository
    {
        public ObservableCollection<MedicalRecord> medicalRecords;
        public MedicalRecordFileHandler medicalRecordFileHandler;

        public MedicalRecordsRepository()
        {
            medicalRecords = new ObservableCollection<MedicalRecord>();
            PatientRepository pr = new PatientRepository();
            DoctorRepository dp = new DoctorRepository();
            medicalRecords.Add(new MedicalRecord(pr.FindById("perica"),"1232141","None",dp.FindByUsername("miromir"),BloodType.oPositive,"None"));
        }
        public bool Create(MedicalRecord medicalRecord)
        {
            medicalRecords.Add(medicalRecord);
            return true;
        }

        public ObservableCollection<MedicalRecord> FindAll()
        {
            return medicalRecords;
        }

        public MedicalRecord FindById(string id)
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

        public bool DeleteById(string id)
        {
            return medicalRecords.Remove(FindById(id));
        }

        public bool UpdateById(string id, MedicalRecord medicalRecord)
        {
            medicalRecords.Remove(FindById(id));
            medicalRecords.Add(medicalRecord);
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