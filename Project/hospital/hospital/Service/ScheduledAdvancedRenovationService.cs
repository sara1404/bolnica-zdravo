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
                    FinishRenovation(renovation);
                else if (renovation._Interval._Start.Date.CompareTo(now.Date) >= 0)
                    StartRenovation(renovation);
            }
        }

        private void FinishRenovation(ScheduledAdvancedRenovation renovation) {
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                if (renovation.flag.Equals("split"))
                    SplitRoom(renovation);
                else if (renovation.flag.Equals("merge"))
                    MergeRooms(renovation);
                DeleteById(renovation._Id);
            });
        }

        private void StartRenovation(ScheduledAdvancedRenovation renovation) {
            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
            {
                MoveEquipmentToWarehouse(renovation);
            });
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
                MoveEquipmentToWarehouseFromRoom(renovation, renovation._Room);
            else if (renovation.flag.Equals("merge"))
                MoveEquipmentToWarehouseFromRooms(renovation);
        }

        private void MoveEquipmentToWarehouseFromRoom(ScheduledAdvancedRenovation renovation, Room room) {
            foreach (Equipment eq in room.equipment)
            {
                roomService.FindRoomByPurpose("warehouse").AddEquipment(eq);
            }
            room.equipment.Clear();
        }

        private void MoveEquipmentToWarehouseFromRooms(ScheduledAdvancedRenovation renovation) {
            MoveEquipmentToWarehouseFromRoom(renovation, renovation.rooms[0]);
            MoveEquipmentToWarehouseFromRoom(renovation, renovation.rooms[1]);
        }


      
    }
}
