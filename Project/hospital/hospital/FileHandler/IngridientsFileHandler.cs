using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.FileHandler
{
    public class IngridientsFileHandler
    {
        private readonly string path = @"../../Resources/Data/IngridientsData.txt";


        public List<string> Read()
        {
            string serializedIngridients = System.IO.File.ReadAllText(path);
            List<string> ingridients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(serializedIngridients);
            return ingridients;
        }

        public void Write(List<string> ingridients)
        {
            string serializedIngridients = Newtonsoft.Json.JsonConvert.SerializeObject(ingridients);
            System.IO.File.WriteAllText(path, serializedIngridients);
        }
    }
}
