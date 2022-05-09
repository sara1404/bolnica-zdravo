using hospital.FileHandler;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class ScheduledAdvancedRenovationRepository
    {
        private List<ScheduledAdvancedRenovation> renovations;
        private ScheduledAdvancedRenovationFileHandler renovationFileHandler;

        public ScheduledAdvancedRenovationRepository()
        {
            renovations = new List<ScheduledAdvancedRenovation>();
            renovationFileHandler = new ScheduledAdvancedRenovationFileHandler();
        }

        public void Create(ScheduledAdvancedRenovation renovation)
        {
            renovations.Add(renovation);
            renovationFileHandler.Write(this.renovations.ToList());
        }

        public ScheduledAdvancedRenovation FindById(string id)
        {
            foreach (ScheduledAdvancedRenovation renovation in renovations)
            {
                if (renovation._Id.Equals(id))
                    return renovation;
            }
            return null;
        }

        public List<ScheduledAdvancedRenovation> FindAll()
        {
            return renovations;
        }

        public bool UpdateById(string id, ScheduledAdvancedRenovation renovation)
        {
            foreach (ScheduledAdvancedRenovation ren in renovations)
            {
                if (ren._Id.Equals(id))
                {
                    ren._Room = renovation._Room;
                    ren._Interval = renovation._Interval;
                    ren._Description = renovation._Description;
                    renovationFileHandler.Write(this.renovations.ToList());
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(string id)
        {
            ScheduledAdvancedRenovation renovation = FindById(id);
            renovations.Remove(renovation);
            renovationFileHandler.Write(this.renovations.ToList());
            return true;
        }

        public void LoadRenovationData()
        {
            if (renovationFileHandler.Read() != null)
            {
                foreach (ScheduledAdvancedRenovation ren in renovationFileHandler.Read())
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
