using Model;

namespace Model
{
    public class Doctor : User
    {
        private string name;
        private string surname;
        private Specialization specialization;

        public Appointment[] appointment;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

        public Specialization Specialization { get => specialization; set => specialization = value; }

        public Doctor(string name, string surname, Specialization specialization = Specialization.general)
        {
            Name = name;
            Surname = surname;
            this.specialization = specialization;
        }
        public override string ToString()
        {
            return name + " " + surname;
        }

    }
}