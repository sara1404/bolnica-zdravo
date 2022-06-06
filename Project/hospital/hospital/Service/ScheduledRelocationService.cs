using hospital.Model;
using hospital.Repository;
using Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class ScheduledRelocationService
    {
        private IScheduledRelocationRepository scheduledRelocationRepository;
        private TimeSchedulerService timeSchedulerService;

        public ScheduledRelocationService(IScheduledRelocationRepository scheduledRelocationRepository, TimeSchedulerService timeSchedulerService)
        {
            this.scheduledRelocationRepository = scheduledRelocationRepository;
            this.timeSchedulerService = timeSchedulerService;
        }

        public void Create(ScheduledRelocation relocation)
        {
            if (relocation._FromRoom.id.Equals(relocation._ToRoom.id))
                throw new Exception("Cant relocate to same room");
            scheduledRelocationRepository.Create(relocation);
        }

        public ScheduledRelocation FindById(string id)
        {
            return scheduledRelocationRepository.FindById(id);
        }

        public List<ScheduledRelocation> FindAll()
        {
            return scheduledRelocationRepository.FindAll();
        }

        public bool UpdateById(string id, ScheduledRelocation relocation)
        {
            return scheduledRelocationRepository.UpdateById(id, relocation);
        }

        public bool DeleteById(string id)
        {
            return scheduledRelocationRepository.DeleteById(id);
        }

        public void RelocationTracker()
        {
            Thread.Sleep(3000);
            DateTime now = DateTime.Now;
            List<ScheduledRelocation> relocations = new List<ScheduledRelocation>(FindAll());
            foreach (ScheduledRelocation rel in relocations)
            {
                if (rel._Relocation._End.Date.CompareTo(now.Date) >= 0)
                {
                    ExecuteRelocation(rel);
                }
            }
        }

        private void ExecuteRelocation(ScheduledRelocation rel) {
            Room toRoom = rel._ToRoom;
            string equipmentType = rel._TypeOfEquipment;
            int quantity = rel._Quantity;
            toRoom.AddEquipment(new Equipment(equipmentType, quantity));
            DeleteById(rel._Id);
        }

        public List<TimeInterval> FindRelocationIntervals(int relocationDuration) {
            return timeSchedulerService.FindRelocationIntervals(relocationDuration);
        }

    }
}
