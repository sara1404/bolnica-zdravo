using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospital.FileHandler;
using Model;

namespace Repository
{
    public class MedicineRepository
    {
        public ObservableCollection<Medicine> medicineList;
        private MedicineFileHandler medicineFileHandler;

        public MedicineRepository()
        {
            medicineList = new ObservableCollection<Medicine>();
            medicineFileHandler = new MedicineFileHandler();
        }
        public bool Create(Medicine medicine)
        {
            medicine.Status = "approved";
            medicineList.Add(medicine);
            medicineFileHandler.Write(medicineList.ToList());
            return true;
        }
        public ObservableCollection<Medicine> FindAll()
        {
            return medicineList;
        }
        public Medicine FindById(string id)
        {
            foreach (Medicine medicine in medicineList)
            {
                if (medicine.Id == id)
                {
                    return medicine;
                }
            }
            return null;
        }

        public Medicine FindByName(string name) {
            foreach (Medicine medicine in medicineList)
            {
                if (medicine.Name == name)
                {
                    return medicine;
                }
            }
            return null;
        }
        public bool UpdateById(string id, Medicine newMedicine)
        {
            Medicine oldMedicine = FindById(id);
            oldMedicine.Name = newMedicine.Name;
            oldMedicine.quantity = newMedicine.quantity;
            oldMedicine.Alternatives = newMedicine.Alternatives;
            oldMedicine.Ingridients = newMedicine.Ingridients;
            oldMedicine.Status = "approved";
            medicineFileHandler.Write(medicineList.ToList());
            return true;
        }

        public bool DeleteById(string id)
        {
            medicineList.Remove(FindById(id));
            return true;
        }
        public void LoadMedicineData()
        {
            if (medicineFileHandler.Read() != null)
            {
                foreach (Medicine medicine in medicineFileHandler.Read())
                {
                    medicineList.Add(medicine);
                }
            }
        }

        public void WriteMedicineData()
        {
            medicineFileHandler.Write(medicineList.ToList());
        }
    }
}
