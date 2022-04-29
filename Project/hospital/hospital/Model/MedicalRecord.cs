using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class MedicalRecord
    {
        private  string note;
        private  string alergies;
        private  string doctorUsername;

        private  List<Therapy> therapy;
        private  BloodType bloodType;
        private  string patientUsername;
        private  int recordId;

        public MedicalRecord(string patientUsername, string _allergens, string _choosen, BloodType bt, string note)
        {
            this.patientUsername = patientUsername;
            this.note = note;
            this.alergies = _allergens;
            this.doctorUsername = _choosen;
            this.bloodType = bt;
            this.therapy = new List<Therapy>();
        }
        public MedicalRecord(string patientsUsername,int id)
        {
            this.PatientUsername=patientsUsername;
            this.RecordId=id;
        }

        public int RecordId { get; set; }
        public string Note { get; set; }
        public string Alergies { get; set; }
        public string DoctorUsername { get; set; }
        public string PatientUsername { get; set; }
        public BloodType BloodType { get; set; }

        public List<Therapy> Therapy { 
            get 
            {
                if (therapy == null) 
                {
                    therapy = new List<Therapy>();
                    return therapy;
                }
                else
                {
                    return therapy;
                }
            } 
            set
            {
                therapy = value;
            }
        }
    }
}