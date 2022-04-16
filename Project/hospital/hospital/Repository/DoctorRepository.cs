using FileHandler;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Repository
{
    public class DoctorRepository
    {
        public DoctorFileHandler doctorFileHandler;
        public ObservableCollection<Doctor> doctors;

        public DoctorRepository()
        {
            doctors = new ObservableCollection<Doctor>();
            Doctor d1 = new Doctor("Mitar", "Miric");
            d1.Username = "miromir";
            d1.Specialization = Specialization.general;
            doctors.Add(d1);
            Doctor d2 = new Doctor("Jovan", "Jovanovic");
            d2.Username = "jovanov";
            d2.Specialization = Specialization.general;
            doctors.Add(d2);
        }
        public ObservableCollection<Doctor> FindAll()
        {
            return doctors;
        }
        public Doctor getByName(string firsname,string lastname)
        {
            foreach(Doctor doctor in doctors)
            {
                if(doctor.Name.Equals(firsname) && doctor.Surname.Equals(lastname))
                    return doctor;
            }
            return null;
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

        public ObservableCollection<Doctor> Doctor
        {
            get
            {
                if (doctors == null)
                {
                    doctors = new ObservableCollection<Doctor>();
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
                doctors = new ObservableCollection<Doctor>();
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