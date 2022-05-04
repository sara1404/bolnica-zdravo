using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.FileHandler
{
    public class ScheduledRelocationFileHandler
    {
        private readonly string path = @"../../Resources/Data/ScheduledRelocationData.txt";


        public List<ScheduledRelocation> Read()
        {
            string serializedScheduledRelocations = System.IO.File.ReadAllText(path);
            List<ScheduledRelocation> scheduledRelocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScheduledRelocation>>(serializedScheduledRelocations);
            return scheduledRelocations;
        }

        public void Write(List<ScheduledRelocation> scheduledRelocations)
        {
            string serializedScheduledRelocations = Newtonsoft.Json.JsonConvert.SerializeObject(scheduledRelocations);
            System.IO.File.WriteAllText(path, serializedScheduledRelocations);
        }
    }
}
