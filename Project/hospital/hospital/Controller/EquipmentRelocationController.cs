using hospital.Model;
using hospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class EquipmentRelocationController
    {
        private readonly EquipmentRelocationService relocationService;

        public EquipmentRelocationController(EquipmentRelocationService relocationService) {
            this.relocationService = relocationService;
        }

        public void Create(EquipmentRelocation relocation) {
            relocationService.Create(relocation);
        }

        public EquipmentRelocation FindById(string id) {
            return relocationService.FindById(id);
        }

        public List<EquipmentRelocation> FindAll() {
            return relocationService.FindAll();
        }

        public bool UpdateById(string id, EquipmentRelocation relocation) {
            return relocationService.UpdateById(id, relocation);
        }

        public bool DeleteById(string id) {
            return relocationService.DeleteById(id);
        }
    }
}
