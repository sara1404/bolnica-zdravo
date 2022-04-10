using Model;
using System;
using System.Collections.Generic;

namespace FileHandler
{
    public class PatientFileHandler
    {
        private readonly string path = @"../../Resources/Data/patientData.txt";

        public List<Patient> Read()
        {
            try
            {
                string serializedPatients = System.IO.File.ReadAllText(path);
                List<Patient> patients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Patient>>(serializedPatients);
                return patients;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Patient> patients)
        {
            string serializedPatients = Newtonsoft.Json.JsonConvert.SerializeObject(patients);
            System.IO.File.WriteAllText(path, serializedPatients);
        }

    }
}