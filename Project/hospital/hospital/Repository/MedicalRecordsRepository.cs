using Model;
using System;
using System.Collections.Generic;

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

        public MedicalRecord FindById(string id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateById(string id, MedicalRecord medicalRecord)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<MedicalRecord> medicalRecord;


        public System.Collections.Generic.List<MedicalRecord> MedicalRecord
        {
            get
            {
                if (medicalRecord == null)
                {
                    medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
                }

                return medicalRecord;
            }
            set
            {
                RemoveAllMedicalRecord();
                if (value != null)
                {
                    foreach (Model.MedicalRecord oMedicalRecord in value)
                    {
                        AddMedicalRecord(oMedicalRecord);
                    }
                }
            }
        }


        public void AddMedicalRecord(Model.MedicalRecord newMedicalRecord)
        {
            if (newMedicalRecord == null)
            {
                return;
            }

            if (medicalRecord == null)
            {
                medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
            }

            if (!medicalRecord.Contains(newMedicalRecord))
            {
                medicalRecord.Add(newMedicalRecord);
            }
        }


        public void RemoveMedicalRecord(Model.MedicalRecord oldMedicalRecord)
        {
            if (oldMedicalRecord == null)
            {
                return;
            }

            if (medicalRecord != null)
            {
                if (medicalRecord.Contains(oldMedicalRecord))
                {
                    medicalRecord.Remove(oldMedicalRecord);
                }
            }
        }


        public void RemoveAllMedicalRecord()
        {
            if (medicalRecord != null)
            {
                medicalRecord.Clear();
            }
        }
        public FileHandler.MedicalRecordFileHandler medicalRecordFileHandler;

    }
}