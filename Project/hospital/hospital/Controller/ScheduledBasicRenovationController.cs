using hospital.Model;
using hospital.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class ScheduledBasicRenovationController
    {
        private ScheduledBasicRenovationService renovationService;

        public ScheduledBasicRenovationController(ScheduledBasicRenovationService renovationService) {
            this.renovationService = renovationService;
        }

        public void Create(ScheduledBasicRenovation renovation) {
            renovationService.Create(renovation);
        }

        public ScheduledBasicRenovation FindById(string id) {
            return renovationService.FindById(id);
        }

        public List<ScheduledBasicRenovation> FindAll() {
            return renovationService.FindAll();
        }

        public bool UpdateById(string id, ScheduledBasicRenovation renovation) {
            return renovationService.UpdateById(id, renovation);
        }

        public bool DeleteById(string id) {
            return renovationService.DeleteById(id);
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration) {
            return renovationService.FindFreeTimeIntervals(room, renovationDuration);
        }
    }
}
