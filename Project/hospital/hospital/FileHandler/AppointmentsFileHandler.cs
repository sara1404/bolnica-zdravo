using Model;
using System;
using System.Collections.Generic;

namespace FileHandler
{
    public class AppointmentsFileHandler
    {
        private readonly string path = @"../../Resources/Data/AppointmentData.txt";

        public List<Appointment> Read()
        {
            try
            {
                string serializedAppointments = System.IO.File.ReadAllText(path);
                List<Appointment> appointments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Appointment>>(serializedAppointments);
                return appointments;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Appointment> appointments)
        {
            string serializedAppointments = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            System.IO.File.WriteAllText(path, serializedAppointments);
        }

    }
}