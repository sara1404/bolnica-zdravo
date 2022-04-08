using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class AppointmentRepository
    {
        public List<Appointment> appointment;
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

        public AppointmentRepository()
        {
            // call filehandler read here
            PatientRepository pr = new PatientRepository();
            DoctorRepository dr = new DoctorRepository();

            appointment = new List<Appointment>();
            DateTime dt = new DateTime(2022, 4, 9, 15, 0, 0);
            appointment.Add(new Appointment(1, dr.FindByUsername("miromir"), pr.FindById("peromir"), dt));
        }
        public Appointment FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> FindAll()
        {
            return appointment;
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public void UpdateById(Appointment appointment, string id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                {
                    appointment = new List<Appointment>();
                }

                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Model.Appointment oAppointment in value)
                    {
                        AddAppointment(oAppointment);
                    }
                }
            }
        }


        public void AddAppointment(Model.Appointment newAppointment)
        {
            if (newAppointment == null)
            {
                return;
            }

            if (appointment == null)
            {
                appointment = new List<Appointment>();
            }

            if (!appointment.Contains(newAppointment))
            {
                appointment.Add(newAppointment);
            }
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
            {
                return;
            }

            if (appointment != null)
            {
                if (appointment.Contains(oldAppointment))
                {
                    appointment.Remove(oldAppointment);
                }
            }
        }


        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                appointment.Clear();
            }
        }

    }
}