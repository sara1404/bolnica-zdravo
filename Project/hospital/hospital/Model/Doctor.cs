namespace Model
{
    public class Doctor : User
    {
        private string name;
        private string surname;

        public Appointment[] appointment;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

        public Doctor(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public override string ToString()
        {
            return name + " " + surname;
        }

    }
}