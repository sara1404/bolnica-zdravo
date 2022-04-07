using System;

namespace Model
{
    public class Appointment
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
        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public int Duration { get => duration; set => duration = value; }



    }
}