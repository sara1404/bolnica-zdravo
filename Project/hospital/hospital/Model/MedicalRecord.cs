using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        /*private  string note;
        private  string alergies;
        private  string doctorUsername;

        //private  List<Therapy> therapy;
        private  BloodType bloodType;
        private  string patientUsername;
        private  int recordId;*/

        public MedicalRecord(string patientUsername, string _allergens, string _choosen, BloodType bt, string note)
        {
            this.PatientUsername = patientUsername;
            this.Note = note;
            this.Alergies = _allergens;
            this.DoctorUsername = _choosen;
            this.BloodType = bt;
        }
        public MedicalRecord(string patientsUsername,int id)
        {
            this.PatientUsername=patientsUsername;
            this.RecordId=id;
        }

        //public string Firstname { get=>patient.FirstName; set=>patient.FirstName=value; }
        //public string Lastname { get => patient.LastName; set => patient.LastName = value; }
        //public Doctor ChoosenDoctor { get; set; }
        //public string Username { get => patient.Username; set => patient.Username=value; }
        //public string NameDoctor { get => choosenDoctor.ToString();}
        //private Patient _Patient { get; set; }
        public int RecordId { get; set; }
        public string Note { get; set; }
        public string Alergies { get; set; }
        public string DoctorUsername { get; set; }
        public string PatientUsername { get; set; }
        public BloodType BloodType { get; set; }

        public List<Therapy> Therapy { get; set; }
    }
}