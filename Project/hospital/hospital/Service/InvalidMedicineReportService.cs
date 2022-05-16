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
    public class InvalidMedicineReportService
    {
        private readonly InvalidMedicineReportRepository invalidMedicineReportRepository;
        public InvalidMedicineReportService(InvalidMedicineReportRepository _repo)
        {
            invalidMedicineReportRepository = _repo;
        }
        public bool Create(InvalidMedicineReport invalidMedicineReport)
        {
            return invalidMedicineReportRepository.Create(invalidMedicineReport);
        }
        public ObservableCollection<InvalidMedicineReport> FindAll()
        {
            return invalidMedicineReportRepository.FindAll();
        }
        public InvalidMedicineReport FindById(int id)
        {
            return invalidMedicineReportRepository.FindById(id);
        }
        public InvalidMedicineReport FindByMedicineId(string medicineId)
        {
            return invalidMedicineReportRepository.FindByMedicineId(medicineId);
        }
        public bool DeleteById(int id)
        {
            return invalidMedicineReportRepository.DeleteById(id);
        }
    }
}
