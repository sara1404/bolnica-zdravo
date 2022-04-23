using hospital.Model;
using hospital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class EquipmentRelocationService
    {
        private readonly EquipmentRelocationRepository relocationRepository;

        public EquipmentRelocationService(EquipmentRelocationRepository relocationRepository) {
            this.relocationRepository = relocationRepository;
        }

        public void Create(EquipmentRelocation relocation) {
            relocationRepository.Create(relocation);
        }

        public EquipmentRelocation FindById(string id) {
            return relocationRepository.FindById(id);
        }

        public List<EquipmentRelocation> FindAll() {
            return relocationRepository.FindAll();
        }

        public bool UpdateById(string id, EquipmentRelocation relocation) {
            return relocationRepository.UpdateById(id, relocation); ;
        }

        public bool DeleteById(string id) {
            return relocationRepository.DeleteById(id);
        }


    }
}
