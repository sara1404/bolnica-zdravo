using System.ComponentModel;

namespace Model
{
    public class Patient : User , INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string id;
        private string dateOfBirth;
        private string phoneNumber;
        private bool isGuest;

        private readonly MedicalRecord medicalRecord;

        public Appointment[] appointment;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Patient() { }
        public Patient(string _username,string _password, string _email,string fName, string lName, string patientId,string phone): base(_username, _password, _email,hospital.Model.Role.Patient)
        {
            FirstName = fName;
            LastName = lName;
            id = patientId;
            phoneNumber= phone;
            isGuest = false;
        }

        public Patient(string name)
        {
            FirstName=name;
        }

        public Patient(string firstName,string surname,string username)
        {
            //constructor for guest account
            FirstName = firstName;
            LastName = surname;
            Username = username;
            isGuest = true;
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

        public string Username { get => base.Username; set => base.Username = value; }
        public string Email { get => base.Email; set => base.Email = value; }

        public bool Blocked { get => base.IsBlocked; set => base.IsBlocked = value; }

        public override string ToString()
        {
            return base.Username;
        }
    }
}