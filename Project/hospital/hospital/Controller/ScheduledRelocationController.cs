using hospital.Model;
using hospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class ScheduledRelocationController
    {
        private ScheduledRelocationService scheduledRelocationService;

        public ScheduledRelocationController(ScheduledRelocationService scheduledRelocationService) {
            this.scheduledRelocationService = scheduledRelocationService;
        }

        public void Create(ScheduledRelocation relocation) {
            scheduledRelocationService.Create(relocation);
        }

        public ScheduledRelocation FindById(string id) {
            return scheduledRelocationService.FindById(id);
        }

        public List<ScheduledRelocation> FindAll() {
            return scheduledRelocationService.FindAll();
        }

        public bool UpdateById(string id, ScheduledRelocation relocation) {
            return scheduledRelocationService.UpdateById(id, relocation);
        }

        public bool DeleteById(string id) {
            return scheduledRelocationService.DeleteById(id);
        }


    }
}
