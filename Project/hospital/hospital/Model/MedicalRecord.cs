namespace Model
{
    public class MedicalRecord
    {
        private readonly string note;
        private readonly string alergies;
        private readonly Doctor choosenDoctor;

        private readonly Therapy[] therapy;
        private readonly BloodType bloodType;
        private readonly Patient patient;

        public string Note { get; set; }
        public string Alergies { get; set; }

        public Doctor ChoosenDoctor { get; set; }
        public BloodType _BloodType { get; set; }
        private Patient _Patient { get; set; }
    }
}