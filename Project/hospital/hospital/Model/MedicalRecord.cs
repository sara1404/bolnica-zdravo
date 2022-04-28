using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class MedicalRecord : INotifyPropertyChanged
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
        public MedicalRecord() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int RecordId { get { return recordId; } set { recordId = value; OnPropertyChanged(""); } }
        public string Note { get { return note; } set { note = value; OnPropertyChanged(""); } }
        public string Alergies { get { return alergies; } set { alergies = value; OnPropertyChanged(""); } }
        public string DoctorUsername { get { return doctorUsername; } set { doctorUsername = value; OnPropertyChanged(""); } }
        public string PatientUsername { get { return patientUsername; } set { patientUsername = value; OnPropertyChanged(""); } }
        public BloodType BloodType { get { return bloodType; } set { bloodType = value; OnPropertyChanged(""); } }

        public List<Therapy> Therapy { get { return therapy; } set { therapy = value; OnPropertyChanged(""); } }
    }
}