using hospital.FileHandler;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class EquipmentRelocationRepository
    {
        public FileHandler.EquipmentRelocationFileHandler equipmentRelocationFileHandler;
        List<EquipmentRelocation> relocations;

        public EquipmentRelocationRepository() {
            equipmentRelocationFileHandler = new EquipmentRelocationFileHandler();
            relocations = new List<EquipmentRelocation>();
        }

        public void Create(Model.EquipmentRelocation equipmentRelocation) {
            relocations.Add(equipmentRelocation);
        }

        public EquipmentRelocation FindById(string id) {
            foreach(EquipmentRelocation relocation in relocations){
                if (relocation._Id.Equals(id))
                    return relocation;
            }
            return null;
        }

        public List<EquipmentRelocation> FindAll() {
            return relocations;
        }

        public bool UpdateById(string id, EquipmentRelocation relocation) {
            foreach (EquipmentRelocation r in relocations) {
                if (r._Id.Equals(id)) {
                    r._Start = relocation._Start;
                    r._End = relocation._End;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(string id) {
            EquipmentRelocation relocation = FindById(id);
            return relocations.Remove(relocation);
        }

        public void LoadRelocationData()
        {
            if (equipmentRelocationFileHandler.Read() != null)
            {
                foreach (EquipmentRelocation relocation in equipmentRelocationFileHandler.Read())
                {
                    relocations.Add(relocation);
                }
            }
        }

        public void WriteRelocationData()
        {
            equipmentRelocationFileHandler.Write(relocations.ToList());
        }

    }
}
