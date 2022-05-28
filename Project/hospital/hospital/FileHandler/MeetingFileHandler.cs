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


        public List<Meetings> Read()
        {
            try
            {
                string serializedMeetings = System.IO.File.ReadAllText(path);
                List<Meetings> meetings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Meetings>>(serializedMeetings);
                return meetings;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Meetings> meetings)
        {
            string serializedMeetings = Newtonsoft.Json.JsonConvert.SerializeObject(meetings);
            System.IO.File.WriteAllText(path, serializedMeetings);
        }
    }
}
