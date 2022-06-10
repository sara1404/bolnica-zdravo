using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDelayAppointmentPage.xaml
    /// </summary>
    public partial class PatientDelayAppointmentPage : Page
    {
        private AppointmentManagementController ac;
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
                Dispatcher.Invoke(() =>
                {
                    notifier.ShowInformation("Appointment successfully delayed!");
                });
                GoToAppointmentsPage();
            }
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
