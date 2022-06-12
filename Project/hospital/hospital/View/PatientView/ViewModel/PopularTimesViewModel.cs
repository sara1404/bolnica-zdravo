using hospital.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hospital.View.PatientView
{
    
    public class PopularTimesViewModel
    {
        public List<ChartDataDTO> Data { get; set; }

        public PopularTimesViewModel()
        {
            App app = Application.Current as App;
            Data = app.appointmentController.GetPopularTimes();
        }
    }
}
