using hospital.Model;
using hospital.Repository;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class ScheduledAdvancedRenovationService
    {
        private ScheduledAdvancedRenovationRepository scheduledRenovationRepository;
        private TimeSchedulerService timeSchedulerService;
        private RoomService roomService;

        public ScheduledAdvancedRenovationService(ScheduledAdvancedRenovationRepository scheduledRenovationRepository, TimeSchedulerService timeSchedulerService, RoomService roomService)
        {
            this.scheduledRenovationRepository = scheduledRenovationRepository;
            this.timeSchedulerService = timeSchedulerService;
            this.roomService = roomService;
        }

        public void Create(ScheduledAdvancedRenovation renovation)
        {
            scheduledRenovationRepository.Create(renovation);
        }

        public ScheduledAdvancedRenovation FindById(string id)
        {
            return scheduledRenovationRepository.FindById(id);
        }

        public List<ScheduledAdvancedRenovation> FindAll()
        {
            return scheduledRenovationRepository.FindAll();
        }

        public bool UpdateById(string id, ScheduledAdvancedRenovation renovation)
        {
            return scheduledRenovationRepository.UpdateById(id, renovation);
        }

        public bool DeleteById(string id)
        {
            return scheduledRenovationRepository.DeleteById(id);
        }

        public List<TimeInterval> FindIntervalsForMergingRooms(List<Room> rooms, int renovationDuration)
        {
            return timeSchedulerService.FindTimeIntervalsForMergingRooms(rooms, renovationDuration);
        }

        public List<TimeInterval> FindIntervalsForSplitingRoom(Room room, int renovationDuration)
        {
            return timeSchedulerService.FindTimeIntervalsForSplitingRoom(room, renovationDuration);
        }

        public void RenovationTracker()
        {


            DateTime now = DateTime.Now;
            List<ScheduledAdvancedRenovation> renovations = new List<ScheduledAdvancedRenovation>(FindAll());
            foreach (ScheduledAdvancedRenovation renovation in renovations)
            {
                if (renovation._Interval._End.Date.CompareTo(now.Date) <= 0)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        if (renovation.flag.Equals("split"))
                            SplitRoom(renovation);
                        else if (renovation.flag.Equals("merge"))
                            MergeRooms(renovation);
                        DeleteById(renovation._Id);
                    });
                }
                else if (renovation._Interval._Start.Date.CompareTo(now.Date) >= 0)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        MoveEquipmentToWarehouse(renovation);
                    });
                }
            }
        }

        private void SplitRoom(ScheduledAdvancedRenovation renovation)
        {
            roomService.DeleteById(renovation._Room.id);
            roomService.Create(renovation.rooms[0]);
            roomService.Create(renovation.rooms[1]);
        }

        private void MergeRooms(ScheduledAdvancedRenovation renovation)
        {
            roomService.DeleteById(renovation.rooms[0].id);
            roomService.DeleteById(renovation.rooms[1].id);
            roomService.Create(renovation._Room);
        }

        private void MoveEquipmentToWarehouse(ScheduledAdvancedRenovation renovation)
        {
            if (renovation.flag.Equals("split"))
                MoveEquipmentToWarehouseWhenSplit(renovation);
            else if (renovation.flag.Equals("merge"))
                MoveEquipmentToWarehouseWhenMerge(renovation);
        }

        private void MoveEquipmentToWarehouseWhenSplit(ScheduledAdvancedRenovation renovation) {
            foreach (Equipment eq in renovation._Room.equipment)
            {
                roomService.FindRoomByPurpose("warehouse").AddEquipment(eq);
            }
            renovation._Room.equipment.Clear();
        }

        private void MoveEquipmentToWarehouseWhenMerge(ScheduledAdvancedRenovation renovation) {
            foreach (Equipment eq in renovation.rooms[0].equipment)
            {
                roomService.FindRoomByPurpose("warehouse").AddEquipment(eq);
            }
            foreach (Equipment eq in renovation.rooms[1].equipment)
            {
                roomService.FindRoomByPurpose("warehouse").AddEquipment(eq);
            }
            renovation.rooms[0].equipment.Clear();
            renovation.rooms[1].equipment.Clear();
        }


      
    }
}
