using Model;
using System.Collections.ObjectModel;

namespace Model
{
    public class Doctor : User
    {
        private string name;
        private string surname;
        private Specialization specialization;
        private string ordinationId;

        public Appointment[] appointment;

        public ObservableCollection<string> myPatients = new ObservableCollection<string>();

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

        public string OrdinationId { get => ordinationId; set => ordinationId = value; }

        public Specialization Specialization { get => specialization; set => specialization = value; }

        public Doctor(string name, string surname, Specialization specialization = Specialization.General)
        {
            Name = name;
            Surname = surname;
            this.specialization = specialization;
        }
        public Doctor() { }
        public override string ToString()
        {
            return name + " " + surname;
        }

    }
}