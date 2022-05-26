using Controller;
using Model;
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

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMakeAppointmentSecond.xaml
    /// </summary>
    public partial class PatientMakeAppointmentSecond : Page
    {
        private AppointmentController ac;
        private UserController uc;
        private App app;

        public PatientMakeAppointmentSecond(Doctor doctor, DateTime dateFrom, DateTime dateTo, bool priorityDoctor, bool priorityDate)
        {
            InitializeComponent();
            app = Application.Current as App;
            ac = app.appointmentController;
            uc = app.userController;

            if (dateFrom != DateTime.MinValue && dateTo != DateTime.MinValue && doctor != null && (priorityDoctor || priorityDate))
            {
                if (dateFrom.CompareTo(dateTo) < 0 && priorityDoctor)
                {
                    appointmentTable.ItemsSource = ac.GetRecommendedByDoctor(dateFrom, dateTo, doctor);
                }
                else if (dateFrom.CompareTo(dateTo) < 0 && priorityDate)
                {
                    appointmentTable.ItemsSource = ac.GetRecommendedByDate(dateFrom, dateTo, doctor);
                }
            }
            else if (doctor != null && dateFrom == DateTime.MinValue && !priorityDoctor && !priorityDate)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDoctor(doctor.Username);

            }
            else if (doctor == null && dateFrom != DateTime.MinValue && !priorityDoctor && !priorityDate)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDate(dateFrom, uc.CurentLoggedUser.Username);
            }
            else if (doctor != null && dateFrom != DateTime.MinValue && !priorityDoctor && !priorityDate)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor(dateFrom, doctor.Username, uc.CurentLoggedUser.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                ac.CreateAppointment((Appointment)appointmentTable.SelectedItem);
            }
        }

        private void btnBackToAppointments_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentFirst();
                }
            }
        }
    }
}
