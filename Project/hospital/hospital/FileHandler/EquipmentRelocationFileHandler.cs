using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.FileHandler
{
    public class EquipmentRelocationFileHandler
    {
        private readonly string path = @"../../Resources/Data/EquipmentRelocation.txt";


        public List<EquipmentRelocation> Read()
        {
            string serializedRelocation = System.IO.File.ReadAllText(path);
            List<EquipmentRelocation> relocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EquipmentRelocation>>(serializedRelocation);
            return relocations;
        }

        public void Write(List<EquipmentRelocation> relocations)
        {
            string serializedRelocation = Newtonsoft.Json.JsonConvert.SerializeObject(relocations);
            System.IO.File.WriteAllText(path, serializedRelocation);
        }
    }
}
