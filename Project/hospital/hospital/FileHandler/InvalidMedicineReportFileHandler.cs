using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class InvalidMedicineReportFileHandler
    {
        private readonly string path = @"../../Resources/Data/InvalidMedicineReportData.txt";

        public List<InvalidMedicineReport> Read()
        {
            try
            {
                string serializedInvalidMedicineReports = System.IO.File.ReadAllText(path);
                List<InvalidMedicineReport> invalidMedicineReports = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvalidMedicineReport>>(serializedInvalidMedicineReports);
                return invalidMedicineReports;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<InvalidMedicineReport> invalidMedicineReports)
        {
            string serializedInvalidMedicineReports = Newtonsoft.Json.JsonConvert.SerializeObject(invalidMedicineReports);
            System.IO.File.WriteAllText(path, serializedInvalidMedicineReports);
        }
    }
}
