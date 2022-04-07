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

    }
}