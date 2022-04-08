using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Repository
{
    public class AppointmentRepository
    {
        public ObservableCollection<Appointment> appointment;
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

        public AppointmentRepository()
        {
            // call filehandler read here
            PatientRepository pr = new PatientRepository();
            DoctorRepository dr = new DoctorRepository();

            appointment = new ObservableCollection<Appointment>();
            DateTime dt = new DateTime(2022, 4, 9, 15, 0, 0);
            appointment.Add(new Appointment(1, dr.FindByUsername("miromir"), pr.FindById("peromir"), dt));
        }

        public int GetAppointmentNumber()
        {
            return appointment.Count;
        }
        public Appointment FindById(int id)
        {
            foreach(Appointment a in appointment)
            {
                if(a.Id == id)
                {
                    return a;
                }
            }
            return null;
        }

        public ObservableCollection<Appointment> FindAll()
        {
            return appointment;
        }

        public void DeleteById(int id)
        {
            appointment.Remove(FindById(id));
        }

        public void Create(Appointment _appointment)
        {
            appointment.Add(_appointment);
        }

        public void UpdateById(Appointment appointment, string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                {
                    appointment = new ObservableCollection<Appointment>();
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
                appointment = new ObservableCollection<Appointment>();
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