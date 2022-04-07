using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DoctorRepository
    {
        public List<Doctor> FindAll()
        {
            throw new NotImplementedException();
        }

        public Doctor FindByUsername(String username)
        {
            throw new NotImplementedException();
        }

        public FileHandler.DoctorFileHandler doctorFileHandler;
        public System.Collections.Generic.List<Doctor> doctor;


        public System.Collections.Generic.List<Doctor> Doctor
        {
            get
            {
                if (doctor == null)
                    doctor = new System.Collections.Generic.List<Doctor>();
                return doctor;
            }
            set
            {
                RemoveAllDoctor();
                if (value != null)
                {
                    foreach (Model.Doctor oDoctor in value)
                        AddDoctor(oDoctor);
                }
            }
        }


        public void AddDoctor(Model.Doctor newDoctor)
        {
            if (newDoctor == null)
                return;
            if (this.doctor == null)
                this.doctor = new System.Collections.Generic.List<Doctor>();
            if (!this.doctor.Contains(newDoctor))
                this.doctor.Add(newDoctor);
        }


        public void RemoveDoctor(Model.Doctor oldDoctor)
        {
            if (oldDoctor == null)
                return;
            if (this.doctor != null)
                if (this.doctor.Contains(oldDoctor))
                    this.doctor.Remove(oldDoctor);
        }


        public void RemoveAllDoctor()
        {
            if (doctor != null)
                doctor.Clear();
        }

    }
}