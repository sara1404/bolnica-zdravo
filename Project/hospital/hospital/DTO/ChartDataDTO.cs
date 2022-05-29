using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.DTO
{
    public class ChartDataDTO
    {
        public DateTime Time { get; set; }
        public int NumberOfAppointments { get; set; }

        public ChartDataDTO(DateTime time)
        {
            Time = time;
            NumberOfAppointments = 1;
        }
    }
}
