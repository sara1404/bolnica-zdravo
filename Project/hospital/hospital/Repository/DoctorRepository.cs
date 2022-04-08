using FileHandler;
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DoctorRepository
    {
        public DoctorFileHandler doctorFileHandler;
        public List<Doctor> doctors;

        public DoctorRepository()
        {
            doctors = new List<Doctor>();
            Doctor d = new Doctor("Mitar", "Miric");
            d.Username = "miromir";
            doctors.Add(d);
        }
        public List<Doctor> FindAll()
        {
            return doctors;
        }

        public Doctor FindByUsername(string username)
        {
            foreach (Doctor d in doctors)
            {
                if (d.Username.Equals(username))
                {
                    return d;
                }
            }
            return null;
        }

        public List<Doctor> Doctor
        {
            get
            {
                if (doctors == null)
                {
                    doctors = new List<Doctor>();
                }

                return doctors;
            }
            set
            {
                RemoveAllDoctor();
                if (value != null)
                {
                    foreach (Doctor oDoctor in value)
                    {
                        AddDoctor(oDoctor);
                    }
                }
            }
        }


        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
            {
                return;
            }

            if (doctors == null)
            {
                doctors = new List<Doctor>();
            }

            if (!doctors.Contains(newDoctor))
            {
                doctors.Add(newDoctor);
            }
        }


        public void RemoveDoctor(Doctor oldDoctor)
        {
            if (oldDoctor == null)
            {
                return;
            }

            if (doctors != null)
            {
                if (doctors.Contains(oldDoctor))
                {
                    doctors.Remove(oldDoctor);
                }
            }
        }


        public void RemoveAllDoctor()
        {
            if (doctors != null)
            {
                doctors.Clear();
            }
        }

    }
}