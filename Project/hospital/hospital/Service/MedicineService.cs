using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;

namespace Service
{
    public class MedicineService
    {
        private readonly MedicineRepository medicineRepository;
        public MedicineService(MedicineRepository _repo)
        {
            medicineRepository = _repo;
        }
        public bool Create(Medicine medicine)
        {
            return medicineRepository.Create(medicine);
        }
        public ObservableCollection<Medicine> FindAll()
        {
            return medicineRepository.FindAll();
        }
        public Medicine FindById(string id)
        {
            return medicineRepository.FindById(id);
        }

        public Medicine FindByName(string name) {
            return medicineRepository.FindByName(name);
        }
        public bool UpdateById(string id, Medicine medicine)
        {
            return medicineRepository.UpdateById(id, medicine);
        }
        public bool DeleteById(string id)
        {
            return medicineRepository.DeleteById(id);
        }
    }
}
