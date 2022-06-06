using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public interface IScheduledRelocationRepository
    {
        void Create(ScheduledRelocation relocation);
        ScheduledRelocation FindById(string id);
        List<ScheduledRelocation> FindAll();
        bool UpdateById(string id, ScheduledRelocation relocation);
        bool DeleteById(string id);
        void LoadRelocationData();
        void WriteRelocationData();

    }
}
