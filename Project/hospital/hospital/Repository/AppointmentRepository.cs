using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Repository
{
    public class AppointmentRepository
    {
        public ObservableCollection<Appointment> appointments;
        public FileHandler.AppointmentsFileHandler appointmentsFileHandler;

        public AppointmentRepository()
        {
            // call filehandler read here
            PatientRepository pr = new PatientRepository();
            DoctorRepository dr = new DoctorRepository();

            appointments = new ObservableCollection<Appointment>();
            DateTime dt = new DateTime(2022, 4, 9, 15, 0, 0);
            appointments.Add(new Appointment(1, dr.FindByUsername("miromir"), pr.FindById("peromir"), dt));
        }

        public int GetAppointmentNumber()
        {
            return appointments.Count;
        }
        public Appointment FindById(int id)
        {
            foreach(Appointment a in appointments)
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
            return appointments;
        }

        public void DeleteById(int id)
        {
            appointments.Remove(FindById(id));
        }

        public void Create(Appointment _appointment)
        {
            appointments.Add(_appointment);
        }

        public void UpdateById(Appointment appointment, string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Appointment> Appointment
        {
            get
            {
                if (appointments == null)
                {
                    appointments = new ObservableCollection<Appointment>();
                }

                return appointments;
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

            if (appointments == null)
            {
                appointments = new ObservableCollection<Appointment>();
            }

            if (!appointments.Contains(newAppointment))
            {
                appointments.Add(newAppointment);
            }
        }


        public void RemoveAppointment(Model.Appointment oldAppointment)
        {
            if (oldAppointment == null)
            {
                return;
            }

            if (appointments != null)
            {
                if (appointments.Contains(oldAppointment))
                {
                    appointments.Remove(oldAppointment);
                }
            }
        }


        public void RemoveAllAppointment()
        {
            if (appointments != null)
            {
                appointments.Clear();
            }
        }

    }
}