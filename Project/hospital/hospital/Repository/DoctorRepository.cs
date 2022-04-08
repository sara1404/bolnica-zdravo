using FileHandler;
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DoctorRepository
    {
        public DoctorFileHandler doctorFileHandler;
        public List<Doctor> doctor;

        public DoctorRepository()
        {
            doctor = new List<Doctor>();
            Doctor d = new Model.Doctor("Mitar", "Miric");
            d.Username = "miromir";
            doctor.Add(d);
        }
        public List<Doctor> FindAll()
        {
            return doctor;
        }

        public Doctor FindByUsername(string username)
        {
            foreach (Doctor d in doctor)
            {
                if (d.Username.Equals(username))
                {
                    return d;
                }
            }
            return null;
        }

        public System.Collections.Generic.List<Doctor> Doctor
        {
            get
            {
                if (doctor == null)
                {
                    doctor = new System.Collections.Generic.List<Doctor>();
                }

                return doctor;
            }
            set
            {
                RemoveAllDoctor();
                if (value != null)
                {
                    foreach (Model.Doctor oDoctor in value)
                    {
                        AddDoctor(oDoctor);
                    }
                }
            }
        }


        public void AddDoctor(Model.Doctor newDoctor)
        {
            if (newDoctor == null)
            {
                return;
            }

            if (doctor == null)
            {
                doctor = new System.Collections.Generic.List<Doctor>();
            }

            if (!doctor.Contains(newDoctor))
            {
                doctor.Add(newDoctor);
            }
        }


        public void RemoveDoctor(Model.Doctor oldDoctor)
        {
            if (oldDoctor == null)
            {
                return;
            }

            if (doctor != null)
            {
                if (doctor.Contains(oldDoctor))
                {
                    doctor.Remove(oldDoctor);
                }
            }
        }


        public void RemoveAllDoctor()
        {
            if (doctor != null)
            {
                doctor.Clear();
            }
        }

    }
}