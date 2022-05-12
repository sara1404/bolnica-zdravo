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
            //medicineList.Add(new Medicine("1", "xyzal 5mg"));
            //medicineList.Add(new Medicine("2", "paracetamol 500mg"));
            //medicineList.Add(new Medicine("3", "defrinol forte"));
            //medicineList.Add(new Medicine("4", "pressing 10mg"));
            //medicineList.Add(new Medicine("5", "febricet 500mg"));
        }
        public bool Create(Medicine medicine)
        {
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
