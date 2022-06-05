using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace FileHandler
{
    public class MeetingFileHandler
    {
        private readonly string path = @"../../Resources/Data/MeetingData.txt";


        public List<Meeting> Read()
        {
            try
            {
                string serializedMeetings = System.IO.File.ReadAllText(path);
                List<Meeting> meetings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Meeting>>(serializedMeetings);
                return meetings;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Meeting> meetings)
        {
            string serializedMeetings = Newtonsoft.Json.JsonConvert.SerializeObject(meetings);
            System.IO.File.WriteAllText(path, serializedMeetings);
        }
    }
}
