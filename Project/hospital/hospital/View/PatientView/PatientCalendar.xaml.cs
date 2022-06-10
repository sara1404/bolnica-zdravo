using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Controls;

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
            AppointmentManagementController ac = app.appointmentController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach (Appointment a in ac.GetAppointmentByPatient(app.userController.CurentLoggedUser.Username))
            {
                ScheduleAppointment sa = new ScheduleAppointment();
                sa.StartTime = a.StartTime;
                sa.EndTime = a.StartTime.AddMinutes(30);
                sa.IsAllDay = false;
                sa.Subject = a.DoctorUsername;
                sac.Add(sa);
            }
            appointmentCalendar.ItemsSource = sac;
            
        }
    }
}
