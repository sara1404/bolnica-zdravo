using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
    public class Appointment : INotifyPropertyChanged
    {
        private int id;
        private string description;
        private DateTime startTime;
        private int duration;

        public Doctor doctor;
        public Patient patient;
        public Appointment(int id, Doctor doctor, Patient patient, DateTime startTime)
        {
            Id = id;
            this.doctor = doctor;
            this.patient = patient;
            StartTime = startTime;
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Description { get => description; set => description = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public int Duration { get => duration; set => duration = value; }
        public string Doctor
        {
            get
            {
                if (doctor == null)
                {
                    return ".";
                }
                else
                {
                    return doctor.Name + " " + doctor.Surname;
                }
            }
        }
        public string Patient
        {
            get
            {
                if (patient == null)
                {
                    return ".";
                }
                else
                {
                    return patient.FirstName + " " + patient.LastName;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public override string ToString()
        {
            return id.ToString();
        }

    }
}