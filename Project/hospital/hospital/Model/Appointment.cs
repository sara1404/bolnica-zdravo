using System;

namespace Model
{
    public class Appointment
    {
        private int id;
        private String description;
        private System.DateTime startTime;
        private int duration;

        public Doctor doctor;
        public Patient patient;

    }
}