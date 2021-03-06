using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorHomeWindow.xaml
    /// </summary>
    public partial class DoctorHomeWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private AppointmentManagementController ac;
        private DoctorController dc;
        private UserController uc;
        private NotificationController nc;

        private Doctor loggedInDoctor;
        public DoctorHomeWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            nc = app.notificationController;
            ac = app.appointmentController;
            dc = app.doctorController;
            uc = app.userController;

            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
           

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 6000; //1min = 60000ms
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;



            //-------sekretar salje notifikacije tebi cim se ulogujes - hvala sekretaru
            foreach (Notification n in nc.FindAll().ToList())
            {
                if (n.Username.Equals(uc.CurentLoggedUser.Username))
                {
                    Timer preTimer;
                    preTimer = new Timer(1000);
                    preTimer.AutoReset = false;
                    preTimer.Elapsed += (sender, e_) => RunOneTimeNotification(sender, e_, n);
                    preTimer.Start();
                    nc.Delete(n);
                }
            }
        }
        public void RunOneTimeNotification(Object source, System.Timers.ElapsedEventArgs e, Notification notification)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation(notification.Text);
            });
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
        //-----------
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Appointments = ac.GetAppointmentByDoctor(loggedInDoctor.Username);
            foreach (Appointment a in Appointments)
            {
                if (a.StartTime <= DateTime.Now)
                {
                    if(!loggedInDoctor.myPatients.Contains(a.PatientUsername))
                    {
                        dc.addPatientToDoctorsList(a.PatientUsername, loggedInDoctor.Username);
                        //MessageBox.Show("DODAT PACIJENT U DOKTORA");
                    }
                }
            }
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            new DoctorAppointmentsWindow().Show();
        }

        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMedicalRecordsWindow().Show();
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {
            new DoctorTherapyWindow().Show();
        }

        private void Vacation_Click(object sender, RoutedEventArgs e)
        {
            new DoctorVacationWindow().Show();
        }

        private void Medicine_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMedicineWindow().Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
