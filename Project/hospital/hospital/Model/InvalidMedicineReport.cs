using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InvalidMedicineReport
    {
        private int id;
        private string medicineId;
        private string note;

        public InvalidMedicineReport() { }
        public InvalidMedicineReport(string medicineId, string note, int id)
        {
            this.medicineId = medicineId;
            this.note = note;
            this.id = id;
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string MedicineId
        {
            get
            {
                return medicineId;
            }
            set
            {
                medicineId = value;
            }
        }
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
            }
        }
    }
}
