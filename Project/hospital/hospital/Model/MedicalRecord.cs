namespace Model
{
    public class MedicalRecord
    {
        private  string note;
        private  string alergies;
        private  Doctor choosenDoctor;

        private  Therapy[] therapy;
        private  BloodType bloodType;
        private  Patient patient;
        private  int recordId;

        public MedicalRecord(Patient patient, string _allergens, Doctor _choosen, BloodType bt, string note)
        {
            this.patient = patient;
            this.note = note;
            this.alergies = _allergens;
            this.choosenDoctor = _choosen;
            this.bloodType = bt;
        }

        public string Firstname { get=>patient.FirstName; set=>patient.FirstName=value; }
        public string Lastname { get => patient.LastName; set => patient.LastName = value; }
        public int RecordId { get=>recordId; set=>recordId=value; }
        public string Username { get => patient.Username; set => patient.Username=value; }

        public string Note { get=>note; set=>note=value; }
        public string Alergies { get=>alergies; set=>alergies=value; }

        public Doctor ChoosenDoctor { get; set; }
        public string NameDoctor { get => choosenDoctor.ToString();}
        public BloodType BloodType { get=> bloodType; set=> bloodType=value; }
        private Patient _Patient { get; set; }
    }
}