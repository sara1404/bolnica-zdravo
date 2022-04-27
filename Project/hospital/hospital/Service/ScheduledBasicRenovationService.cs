using hospital.Model;
using hospital.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class ScheduledBasicRenovationService
    {
        private ScheduledBasicRenovationRepository scheduledRenovationRepository;
        private TimeSchedulerService timeSchedulerService;

        public ScheduledBasicRenovationService(ScheduledBasicRenovationRepository scheduledRenovationRepository, TimeSchedulerService timeSchedulerService) {
            this.scheduledRenovationRepository = scheduledRenovationRepository;
            this.timeSchedulerService = timeSchedulerService;
        }

        public void Create(ScheduledBasicRenovation renovation) {
            scheduledRenovationRepository.Create(renovation);
        }

        public ScheduledBasicRenovation FindById(string id) {
            return scheduledRenovationRepository.FindById(id);
        }

        public List<ScheduledBasicRenovation> FindAll() {
            return scheduledRenovationRepository.FindAll();
        }

        public bool UpdateById(string id, ScheduledBasicRenovation renovation) {
            return scheduledRenovationRepository.UpdateById(id, renovation);
        }

        public bool DeleteById(string id) {
            return scheduledRenovationRepository.DeleteById(id);
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration) {
            return timeSchedulerService.FindFreeTimeIntervals(room, renovationDuration);
        }

    }
}
