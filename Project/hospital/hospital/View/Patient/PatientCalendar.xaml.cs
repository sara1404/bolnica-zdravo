using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.Pdf;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientCalendar.xaml
    /// </summary>
    public partial class PatientCalendar : Page
    {
        public PatientCalendar()
        {
            InitializeComponent();
            App app = Application.Current as App;
            AppointmentController ac = app.appointmentController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach(Appointment a in ac.GetAppointmentByPatient(app.userController.CurentLoggedUser.Username))
            {
                ScheduleAppointment sa = new ScheduleAppointment();
                sa.StartTime = a.StartTime;
                sa.EndTime = a.StartTime.AddMinutes(30);
                sa.IsAllDay = false;
                sac.Add(sa);
            }
            appointmentCalendar.ItemsSource = sac;
            
        }
    }
}
