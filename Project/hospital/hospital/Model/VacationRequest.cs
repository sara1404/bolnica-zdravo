using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Status { waiting, approved }
    public class VacationRequest
    {
        private string doctorId;
        int id;
        private DateTime startDate;
        private DateTime endDate;
        private bool isHighPriority;
        private string note;
        private Status status;

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

    }
}
