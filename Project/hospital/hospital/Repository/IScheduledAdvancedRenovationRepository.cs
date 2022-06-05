using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public interface IScheduledAdvancedRenovationRepository
    {
        void Create(ScheduledAdvancedRenovation renovation);
        ScheduledAdvancedRenovation FindById(string id);
        List<ScheduledAdvancedRenovation> FindAll();
        bool UpdateById(string id, ScheduledAdvancedRenovation renovation);
        bool DeleteById(string id);
        List<ScheduledAdvancedRenovation> FindForSpecifiedRoom(Room room);
        void LoadRenovationData();
        void WriteRenovationData();
    }
}
