using Controller;
using hospital.Controller;
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
    /// Interaction logic for PatientDelayAppointmentPage.xaml
    /// </summary>
    public partial class PatientDelayAppointmentPage : Page
    {
        private AppointmentController ac;
        private PatientController pc;
        private Appointment selectedAppointment;
        private UserController uc;
        private AvailableAppointmentController aac;
        public PatientDelayAppointmentPage(Appointment a)
        {
            InitializeComponent();
            selectedAppointment = a;
            tbxDoctor.Text = selectedAppointment.DoctorUsername;
            oldDate.SelectedDate = selectedAppointment.StartTime;
            App app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            pc = app.patientController;
            aac = app.availableAppointmentController;

            newDate.DisplayDateStart = DateTime.Now > selectedAppointment.StartTime.AddDays(-4) ? DateTime.Now : selectedAppointment.StartTime.AddDays(-4);
            newDate.DisplayDateEnd = selectedAppointment.StartTime.AddDays(4);
            DataContext = this;
        }

        public void LogoutUser()
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Close();
                }
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (newDate.SelectedDate != null)
            {
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, selectedAppointment.DoctorUsername, uc.CurentLoggedUser.Username);
            }
        }
        private void GoToAppointmentsPage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientAppointmentsPage();
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedItem != null)
            {
                pc.AddDelayOrCancelAppointment(uc.CurentLoggedUser.Username);
                if (pc.IsTroll(uc.CurentLoggedUser.Username))
                {
                    pc.BlockPatient(uc.CurentLoggedUser.Username);
                    LogoutUser();
                }
                ac.UpdateAppointment(selectedAppointment, (Appointment)appointmentTable.SelectedItem);
                GoToAppointmentsPage();
            }
        }
    }
}
