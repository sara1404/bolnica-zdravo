using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;

namespace Controller
{
    public class MedicineController
    {
        private readonly MedicineService medicineService;

        public MedicineController(MedicineService _service)
        {
            medicineService = _service;
        }

        public bool Create(Medicine medicine)
        {
            return medicineService.Create(medicine);
        }

        public ObservableCollection<Medicine> FindAll()
        {
            return medicineService.FindAll();
        }

        public Medicine FindById(string id)
        {
            return medicineService.FindById(id);
        }

        public Medicine FindByName(string name)
        {
            return medicineService.FindByName(name);
        }

        public bool UpdateById(string id, Medicine medicine)
        {
            return medicineService.UpdateById(id, medicine);
        }

        public bool DeleteById(string id)
        {
            return medicineService.DeleteById(id);
        }

        public List<Medicine> FindRejectedMedicines()
        {
            return medicineService.FindRejectedMedicines();
        }

        public List<Medicine> FindApprovedMedicines()
        {
            return medicineService.FindApprovedMedicines();
        }
    }
}