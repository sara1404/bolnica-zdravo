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
    public class ScheduledAdvancedRenovationController
    {
        private ScheduledAdvancedRenovationService renovationService;

        public ScheduledAdvancedRenovationController(ScheduledAdvancedRenovationService renovationService)
        {
            this.renovationService = renovationService;
        }

        public void Create(ScheduledAdvancedRenovation renovation)
        {
            renovationService.Create(renovation);
        }

        public ScheduledAdvancedRenovation FindById(string id)
        {
            return renovationService.FindById(id);
        }

        public List<ScheduledAdvancedRenovation> FindAll()
        {
            return renovationService.FindAll();
        }

        public bool UpdateById(string id, ScheduledAdvancedRenovation renovation)
        {
            return renovationService.UpdateById(id, renovation);
        }

        public bool DeleteById(string id)
        {
            return renovationService.DeleteById(id);
        }

        public List<TimeInterval> FindIntervalsForMergingRooms(List<Room> rooms, int renovationDuration) {
            return renovationService.FindIntervalsForMergingRooms(rooms, renovationDuration);
        }

        public List<TimeInterval> FindIntervalsForSplitingRoom(Room room, int renovationDuration) {
            return renovationService.FindIntervalsForSplitingRoom(room, renovationDuration);
        }

        public void RenovationTracker() {
            renovationService.RenovationTracker();
        }
    }
}
