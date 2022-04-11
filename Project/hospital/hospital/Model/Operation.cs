using System;

namespace Model
{
    public class Operation : Appointment
    {
        private Room operationRoom;
        public Operation() : base(0, null, null, System.DateTime.Now)
        {

        }

        public Operation(int id, Doctor doctor, Patient patient, DateTime startTime, Room room)
        {
            base.Id = id;
            base.doctor = doctor;
            base.patient = patient;
            base.StartTime = startTime;
            this.operationRoom = room;
        }

        public Room OperationRoom
        {
            get { return operationRoom; }
            set { operationRoom = value; }
        }
    }
}