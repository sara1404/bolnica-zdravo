using Controller;
using hospital.View.PatientView;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsPage.xaml
    /// </summary>
    public partial class PatientAppointmentsPage : Page
    {
        private AppointmentManagementController ac;
        private PatientController pc;
        private UserController uc;
        private App app;
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientAppointmentsPage()
        {
            InitializeComponent();
            app = Application.Current as App;
            ac = app.appointmentController;
            pc = app.patientController;
            uc = app.userController;
            DataContext = this;
            User current = app.userController.CurentLoggedUser;
            Appointments = ac.GetAppointments();

            btnLeaveNote.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnDelay.IsEnabled = false;

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            Appointment appointment = item as Appointment;
            User current = app.userController.CurentLoggedUser;
            return appointment.PatientUsername.Equals(current.Username);
        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
            if (selectedAppointment != null)
            {
                if (ac.CanBeDelayed(selectedAppointment))
                {
                    GoToDelayPage(selectedAppointment);
                }
                else
                {
                    MessageBox.Show("You can't delay an appointment now!");
                }
            }
        }

        private void GoToDelayPage(Appointment appointment)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientDelayAppointmentPage(appointment);
                }
            }
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                pc.AddDelayOrCancelAppointment(uc.CurentLoggedUser.Username);
                if (pc.IsTroll(uc.CurentLoggedUser.Username))
                {
                    pc.BlockPatient(uc.CurentLoggedUser.Username);
                    LogoutUser();
                }
                ac.DeleteAppointment((appointmentTable.SelectedItem as Appointment).Id);
                Dispatcher.Invoke(() =>
                {
                    notifier.ShowInformation("Appointment succcessfully canceled!");
                });
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

        private void GoToLeaveNotePage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientAppointmentNote(appointmentTable.SelectedItem as Appointment);
                }
            }
        }

        private void btnLeaveNote_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedItem != null && (appointmentTable.SelectedItem as Appointment).StartTime <= DateTime.Now)
            {
                GoToLeaveNotePage();
            }
        }

        private void appointmentTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(appointmentTable.SelectedItem != null)
            {
                Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
                btnDelay.IsEnabled = ac.CanBeDelayed(selectedAppointment);
                btnLeaveNote.IsEnabled = selectedAppointment.StartTime <= DateTime.Now;
                btnCancel.IsEnabled = selectedAppointment.StartTime >= DateTime.Now;
            }
        }
    }
}
