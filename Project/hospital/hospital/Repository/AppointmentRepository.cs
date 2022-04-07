using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class AppointmentRepository
    {
        public Appointment FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> FindAll()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void UpdateById(Appointment appointment, String id)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<Appointment> appointment;


        public System.Collections.Generic.List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.Generic.List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Model.Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }


        public void AddAppointment(Model.Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.Generic.List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
                this.appointment.Add(newAppointment);
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                    this.appointment.Remove(oldAppointment);
        }


        public void RemoveAllAppointment()
        {
            if (appointment != null)
                appointment.Clear();
        }
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

    }
}