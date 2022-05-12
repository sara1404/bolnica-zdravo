using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.FileHandler
{
    public class MedicineFileHandler
    {
        private readonly string path = @"../../Resources/Data/MedicineData.txt";


        public List<Medicine> Read()
        {
            string serializedMedicines = System.IO.File.ReadAllText(path);
            List<Medicine> medicines = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Medicine>>(serializedMedicines);
            return medicines;
        }

        public void Write(List<Medicine> medicines)
        {
            string serializedMedicines = Newtonsoft.Json.JsonConvert.SerializeObject(medicines);
            System.IO.File.WriteAllText(path, serializedMedicines);
        }
    }
}
