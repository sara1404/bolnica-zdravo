namespace Model
{
    public class Patient : User
    {
        private string firstName;
        private string lastName;
        private string id;
        private string dateOfBirth;
        private string phoneNumber;
        private bool isGuest;

        private readonly MedicalRecord medicalRecord;

        public Appointment[] appointment;

        public Patient(string fName, string lName, string patientId)
        {
            FirstName = fName;
            LastName = lName;
            id = patientId;
        }

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool IsGuest { get => isGuest; set => isGuest = value; }

        public override string ToString()
        {
            return firstName + " " + lastName;
        }
    }
}