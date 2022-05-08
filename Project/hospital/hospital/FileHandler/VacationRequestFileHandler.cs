using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandler
{
    public class VacationRequestFileHandler
    {
        private readonly string path = @"../../Resources/Data/VacationRequestData.txt";

        public List<VacationRequest> Read()
        {
            try
            {
                string serializedVacationRequests = System.IO.File.ReadAllText(path);
                List<VacationRequest> VacationRequests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VacationRequest>>(serializedVacationRequests);
                return VacationRequests;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<VacationRequest> VacationRequests)
        {
            string serializedVacationRequests = Newtonsoft.Json.JsonConvert.SerializeObject(VacationRequests);
            System.IO.File.WriteAllText(path, serializedVacationRequests);
        }
    }
}
