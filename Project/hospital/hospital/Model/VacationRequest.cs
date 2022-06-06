using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Status { waiting, approved, rejected }
    public class VacationRequest
    {
        private string doctorId;
        int id;
        private DateTime startDate;
        private DateTime endDate;
        private bool isHighPriority;
        private string note;
        private Status status;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public VacationRequest() { }
        public VacationRequest(DateTime startDate, DateTime endDate, bool isHighPriority, string note, string doctorId, int id)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.isHighPriority = isHighPriority;
            this.note = note;
            this.status = Status.waiting;
            this.doctorId = doctorId;
            this.id = id;
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged("");
            }
        }
        public bool IsHighPriority
        {
            get
            {
                return isHighPriority;
            }
            set
            {
                isHighPriority = value;
                OnPropertyChanged("");
            }
        }
        public string Note
        { 
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("");
            }
        }
        public Status Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("");
            }
        }
        public string DoctorId
        {
            get
            {
                return doctorId;
            }
            set
            {
                doctorId = value;
                OnPropertyChanged("");
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
                OnPropertyChanged("");
            }
        }
        public string VacationStatus
        {
            get
            {
                if (status == Status.approved) return "Approved";
                if (status == Status.rejected) return "Rejected";
                else
                    return "Waiting";
            }
        }

    }
}
