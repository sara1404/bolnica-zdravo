using Newtonsoft.Json;
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

        public string doctorUsername;
        public string patientUsername;
        public string roomId;
        
        public Appointment(int id, string doctorUsername, string patientUsername, DateTime startTime)
        {
            Id = id;
            this.doctorUsername = doctorUsername;
            this.patientUsername = patientUsername;
            StartTime = startTime;
        }
        public string PatientUsername
        {
            get
            {
                if (patientUsername == null)
                    return "";
                else
                    return patientUsername;
            }
            set
            {
                patientUsername = value;
            }
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
        public string DoctorUsername
        {
            get
            {
                if (doctorUsername == null)
                    return "";
                else
                    return doctorUsername;
            }
            set
            {
                doctorUsername = value;
            }
        }
        public string RoomId
        {
            get
            {
                if (roomId == null)
                    return "";
                else
                    return roomId;
            }
            set
            {
                roomId = value;
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