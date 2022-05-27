using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
    public class Appointment : INotifyPropertyChanged
    {
        private int _id;
        private string _description;
        private DateTime _startTime;
        private int _duration;
        private string _doctorUsername;
        private string _patientUsername;
        private string _roomId;
        private string _doctorNote;
        private string _patientNote;
        
        public Appointment(int id, string doctorUsername, string patientUsername, DateTime startTime)
        {
            _id = id;
            _doctorUsername = doctorUsername;
            _patientUsername = patientUsername;
            _startTime = startTime;
        }
        public string Description { get => _description; set => _description = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public string DoctorNote { get => _doctorNote; set => _doctorNote = value; }
        public string PatientNote { get => _patientNote; set => _patientNote = value; }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string PatientUsername
        {
            get
            {
                if (_patientUsername == null)
                {
                    return "";
                }
                else
                {
                    return _patientUsername;
                }
            }
            set
            {
                _patientUsername = value;
                OnPropertyChanged();
            }
        }
        public string DoctorUsername
        {
            get
            {
                if (_doctorUsername == null)
                {
                    return "";
                }
                else
                {
                    return _doctorUsername;
                }
            }
            set
            {
                _doctorUsername = value;
                OnPropertyChanged();
            }
        }
        public string RoomId
        {
            get
            {
                if (_roomId == null)
                {
                    return "";
                }
                else
                {
                    return _roomId;
                }
            }
            set
            {
                _roomId = value;
                OnPropertyChanged();
            }
        }
        public string AppointmentDisplayProperty
        {
            get
            {
                return _id.ToString() + " " + _doctorUsername + " " + _startTime.ToString();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}