using hospital.FileHandler;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class ScheduledRelocationRepository : IScheduledRelocationRepository
    {
        ScheduledRelocationFileHandler scheduledRelocationFileHandler;
        List<ScheduledRelocation> relocations;
        public ScheduledRelocationRepository() {
            scheduledRelocationFileHandler = new ScheduledRelocationFileHandler();
            relocations = new List<ScheduledRelocation>();
        }

        public void Create(ScheduledRelocation relocation) {
            relocations.Add(relocation);
        }

        public ScheduledRelocation FindById(string id) {
            foreach (ScheduledRelocation rel in relocations) {
                if (rel._Id.Equals(id))
                    return rel;
            }
            return null;
        }

        public List<ScheduledRelocation> FindAll() {
            return relocations;
        }

        public bool UpdateById(string id, ScheduledRelocation relocation) {
            foreach (ScheduledRelocation rel in relocations) {
                if (rel._Id.Equals(id)) {
                    rel._FromRoom = relocation._FromRoom;
                    rel._ToRoom = relocation._ToRoom;
                    rel._TypeOfEquipment = relocation._TypeOfEquipment;
                    rel._Quantity = relocation._Quantity;
                    rel._Relocation = relocation._Relocation;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(string id) {
            ScheduledRelocation relocation = FindById(id);
            return relocations.Remove(relocation);
        }

        public void LoadRelocationData()
        {
            if (scheduledRelocationFileHandler.Read() != null)
            {
                foreach (ScheduledRelocation rel in scheduledRelocationFileHandler.Read())
                {
                    relocations.Add(rel);
                }
            }
        }

        public void WriteRelocationData()
        {
            scheduledRelocationFileHandler.Write(relocations);
        }

    }
}
