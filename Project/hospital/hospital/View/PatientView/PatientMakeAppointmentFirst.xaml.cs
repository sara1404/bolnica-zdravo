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
    /// Interaction logic for PatientMakeAppointmentFirst.xaml
    /// </summary>
    public partial class PatientMakeAppointmentFirst : Page
    {
        private AppointmentController ac;
        private UserController uc;
        public PatientMakeAppointmentFirst()
        {
            InitializeComponent();
            dateFrom.DisplayDateStart = DateTime.Today;
            dateTo.DisplayDateStart = DateTime.Today;
            App app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            DoctorController dc = app.doctorController;
            cbDoctor.ItemsSource = dc.GetDoctors();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientMainMenu();
                }
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            bool priorityDoctor = (bool)rbDoctor.IsChecked;
            bool priorityDate = (bool)rbDate.IsChecked;
            if (dateFrom.SelectedDate != null && dateTo.SelectedDate != null && cbDoctor.SelectedItem != null && (priorityDoctor || priorityDate))
            {
                if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDoctor)
                {
                    appointmentTable.ItemsSource = ac.GetRecommendedByDoctor((DateTime)dateFrom.SelectedDate, (DateTime)dateTo.SelectedDate, (Doctor)cbDoctor.SelectedItem);
                }
                else if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDate)
                {
                    appointmentTable.ItemsSource = ac.GetRecommendedByDate((DateTime)dateFrom.SelectedDate, (DateTime)dateTo.SelectedDate, (Doctor)cbDoctor.SelectedItem);
                }
            }
            else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate == null && !priorityDoctor && !priorityDate)
            {
                Doctor d = (Doctor)cbDoctor.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDoctor(d.Username);

            }
            else if (cbDoctor.SelectedIndex == -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
            {
                DateTime selectedDate = (DateTime)dateFrom.SelectedDate;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDate(selectedDate,uc.CurentLoggedUser.Username);
            }
            else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
            {
                DateTime selectedDate = (DateTime)dateFrom.SelectedDate;
                Doctor d = (Doctor)cbDoctor.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor(selectedDate, d.Username,uc.CurentLoggedUser.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                ac.CreateAppointment((Appointment)appointmentTable.SelectedItem);
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PatientHomeWindow))
                    {
                        (window as PatientHomeWindow).Main.Content = new PatientMainMenu();
                    }
                }
            }
        }
    }
}
