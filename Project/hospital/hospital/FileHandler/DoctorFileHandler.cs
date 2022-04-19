using Model;
using System;
using System.Collections.Generic;

namespace FileHandler
{
    public class DoctorFileHandler
    {
        private readonly string path = @"../../Resources/Data/DoctorData.txt";

        public List<Doctor> Read()
        {
            try
            {
                string serializedDoctors = System.IO.File.ReadAllText(path);
                List<Doctor> doctors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Doctor>>(serializedDoctors);
                return doctors;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Doctor> doctors)
        {
            string serializedDoctors = Newtonsoft.Json.JsonConvert.SerializeObject(doctors);
            System.IO.File.WriteAllText(path, serializedDoctors);
        }

    }
}