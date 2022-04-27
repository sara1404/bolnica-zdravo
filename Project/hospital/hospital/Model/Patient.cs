using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class Patient : User , INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string id;
        private string dateOfBirth;
        private string gender;
        private string phoneNumber;
        private bool isGuest;
        private string email;

        private int recordID;

        //public Appointment[] appointment;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Patient() { }
        public Patient(string _username, string password,string fName, string lName,string email, string patientId,string phone,string date,string gender,bool blocked)
        {
            Username = _username;
            Password = password;
            FirstName = fName;
            LastName = lName;
            Email = email;
            id = patientId;
            phoneNumber= phone;
            dateOfBirth= date;
            isGuest = false;
            Gender = gender;
            IsBlocked = blocked;
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
        public string Gender { get; set; }
        public string Email { get; set; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool IsGuest { get => isGuest; set => isGuest = value; }
        public string Username { get => base.Username; set => base.Username = value; }
        public override string ToString()
        {
            return base.Username;
        }
        public int RecordId { get => recordID; set => recordID = value; }


    }
}