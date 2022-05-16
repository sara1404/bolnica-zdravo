using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class InvalidMedicineReportController
    {
        private readonly InvalidMedicineReportService invalidMedicineReportService;
        public InvalidMedicineReportController(InvalidMedicineReportService _service)
        {
            invalidMedicineReportService = _service;
        }
        public bool Create(InvalidMedicineReport invalidMedicineReport)
        {
            return invalidMedicineReportService.Create(invalidMedicineReport);
        }
        public ObservableCollection<InvalidMedicineReport> FindAll()
        {
            return invalidMedicineReportService.FindAll();
        }
        public InvalidMedicineReport FindById(int id)
        {
            return invalidMedicineReportService.FindById(id);
        }
        public InvalidMedicineReport FindByMedicineId(string medicineId)
        {
            return invalidMedicineReportService.FindByMedicineId(medicineId);
        }
        public bool DeleteById(int id)
        {
            return invalidMedicineReportService.DeleteById(id);
        }
    }
}
