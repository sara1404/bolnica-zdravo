using hospital.FileHandler;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class ScheduledBasicRenovationRepository
    {
        private List<ScheduledBasicRenovation> renovations;
        private ScheduledBasicRenovationFIleHandler renovationFileHandler;

        public ScheduledBasicRenovationRepository() {
            renovations = new List<ScheduledBasicRenovation>();
            renovationFileHandler = new ScheduledBasicRenovationFIleHandler();
        }

        public void Create(ScheduledBasicRenovation renovation) {
            renovations.Add(renovation);
            renovationFileHandler.Write(renovations);
        }

        public ScheduledBasicRenovation FindById(string id) {
            foreach (ScheduledBasicRenovation renovation in renovations) {
                if (renovation._Id.Equals(id))
                    return renovation;
            }
            return null;
        }

        public List<ScheduledBasicRenovation> FindAll() {
            return renovations;
        }

        public bool UpdateById(string id, ScheduledBasicRenovation renovation) {
            foreach (ScheduledBasicRenovation ren in renovations)
            {
                if (ren._Id.Equals(id)) {
                    ren._Room = renovation._Room;
                    ren._Interval = renovation._Interval;
                    ren._Description = renovation._Description;
                    renovationFileHandler.Write(renovations);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(string id) {
            ScheduledBasicRenovation renovation = FindById(id);
            renovationFileHandler.Write(renovations);
            return renovations.Remove(renovation);
        }

        public void LoadRenovationData()
        {
            if (renovationFileHandler.Read() != null)
            {
                foreach (ScheduledBasicRenovation ren in renovationFileHandler.Read())
                {
                    renovations.Add(ren);
                }
            }
        }

        public void WriteRenovationData()
        {
            renovationFileHandler.Write(renovations);
        }
    }
}
