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
        private ScheduledRelocationRepository scheduledRelocationRepository;

        public ScheduledRelocationService(ScheduledRelocationRepository scheduledRelocationRepository)
        {
            this.scheduledRelocationRepository = scheduledRelocationRepository;
        }

        public void Create(ScheduledRelocation relocation)
        {
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

        public void relocationTracker()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(3000);
                    DateTime now = DateTime.Now;
                    List<ScheduledRelocation> relocations = FindAll();
                    foreach (ScheduledRelocation rel in relocations)
                    {
                        if (rel._Relocation._End.Date.CompareTo(now.Date) == 0)
                        {
                            Room fromRoom = rel._FromRoom;
                            Room toRoom = rel._ToRoom;
                            string equipmentType = rel._TypeOfEquipment;
                            int quantity = rel._Quantity;
                            Equipment equipment = new Equipment(equipmentType, quantity);
                            foreach (Equipment eq in fromRoom.equipment)
                            {
                                if (eq.type.Equals(equipmentType))
                                {
                                    eq.quantity -= quantity;
                                    if (eq.quantity == 0)
                                    {
                                        List<Equipment> equipTemp = fromRoom.equipment.ToList();
                                        equipTemp.Remove(eq);
                                        fromRoom.equipment = new ConcurrentBag<Equipment>(equipTemp);

                                    }
                                }
                            }
                            foreach (Equipment eq in toRoom.equipment)
                            {
                                if (eq.type.Equals(equipmentType))
                                {
                                    eq.quantity += quantity;
                                    DeleteById(rel._Id);
                                    return;
                                }
                            }
                            toRoom.equipment.Add(equipment);
                            DeleteById(rel._Id);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

    }
}
