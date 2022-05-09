using FileHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class InvalidMedicineReportRepository
    {
        public ObservableCollection<InvalidMedicineReport> invalidMedicineReports;
        public InvalidMedicineReportFileHandler invalidMedicineReportFileHandler;
        public InvalidMedicineReportRepository()
        {
            
            invalidMedicineReportFileHandler = new InvalidMedicineReportFileHandler();
            List<InvalidMedicineReport> deserializedList = invalidMedicineReportFileHandler.Read();
            if (deserializedList != null)
            {
                invalidMedicineReports = new ObservableCollection<InvalidMedicineReport>(invalidMedicineReportFileHandler.Read());
            }
            else
            {
                invalidMedicineReports = new ObservableCollection<InvalidMedicineReport>();
            }
        }
        public bool Create(InvalidMedicineReport invalidMedicineReport)
        {
            invalidMedicineReport.Id = generateId();
            invalidMedicineReports.Add(invalidMedicineReport);
            invalidMedicineReportFileHandler.Write(this.invalidMedicineReports.ToList());
            return true;
        }
        private int generateId()
        {
            int maxId = 0;
            foreach (InvalidMedicineReport report in FindAll())
            {
                if (report.Id > maxId)
                    maxId = report.Id;
            }
            return maxId + 1;
        }
        public ObservableCollection<InvalidMedicineReport> FindAll()
        {
            return invalidMedicineReports;
        }
        public InvalidMedicineReport FindById(int id)
        {
            foreach (InvalidMedicineReport report in invalidMedicineReports)
            {
                if (report.Id == id)
                {
                    return report;
                }
            }
            return null;
        }
        public InvalidMedicineReport FindByMedicineId(string medicineId)
        {
            foreach (InvalidMedicineReport report in invalidMedicineReports)
            {
                if (report.MedicineId == medicineId)
                {
                    return report;
                }
            }
            return null;
        }
        public bool DeleteById(int id)
        {
            invalidMedicineReports.Remove(FindById(id));
            invalidMedicineReportFileHandler.Write(this.invalidMedicineReports.ToList());
            return true;
        }

    }
}
