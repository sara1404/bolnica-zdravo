using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
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
using Controller;
using hospital.View.PatientView;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientHomeWindow.xaml
    /// </summary>
    public partial class PatientHomeWindow : Window
    {
        private NotificationController nc;
        private MedicalRecord mr;
        private User current;
        private List<Timer> timers;
        private App app;

        public PatientHomeWindow()
        {
            InitializeComponent();
            Main.Content = new PatientCalendar();

            app = Application.Current as App;
            nc = app.notificationController;
            current = app.userController.CurentLoggedUser;
            mr = app.medicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
            timers = new List<Timer>();

            StartTherapyNotifications();
            StartNotificationsFromFile();
        }

        private void StartNotificationsFromFile()
        {
            foreach (Notification n in nc.FindAll().ToList())
            {
                if (n.Username.Equals(current.Username))
                {
                    StartNotification(n);
                }
            }
        }

        private void StartTherapyNotifications()
        {
            if (mr != null && mr.Therapy != null)
            {
                foreach (Therapy t in mr.Therapy)
                {
                    Notification n = new Notification("", t.TimeStart, t.TimeEnd, t.Interval, "Take " + t.Medicine + "!");
                    StartNotification(n);
                }
            }
        }
        
        public void StartNotification(Notification notification)
        {
            if (notification.StartTime != DateTime.MinValue && notification.EndTime == DateTime.MinValue)
            {
                StartOneTimeNotification(notification);
            }
            else if(notification.StartTime != DateTime.MinValue && notification.EndTime != DateTime.MinValue)
            {
                StartPeriodicalNotification(notification);
            }
            else if (notification.StartTime == DateTime.MinValue && notification.EndTime == DateTime.MinValue)
            {
                // on startup notifications
            }
        }

        private void StartOneTimeNotification(Notification notification)
        {
            if (DateTime.Now <= notification.StartTime)
            {
                TimeSpan timeToGo = notification.StartTime - DateTime.Now;
                Timer preTimer = MakeTimer(timeToGo.TotalMilliseconds, false, true);
                preTimer.Elapsed += (sender, e_) => RunOneTimeNotification(sender, e_, notification);
            }
        }

        public void RunOneTimeNotification(Object source, System.Timers.ElapsedEventArgs e, Notification notification)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation(notification.Text);
            });
        }

        private void StartPeriodicalNotification(Notification notification)
        {
            if (DateTime.Now <= notification.StartTime)
            {
                Timer preTimer = MakeTimer((notification.StartTime - DateTime.Now).TotalMilliseconds, false, true);
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, notification);
            }
            else if (DateTime.Now > notification.StartTime && DateTime.Now <= notification.EndTime)
            {
                TimeSpan timeToGo = IterateTime(notification.StartTime, notification.Interval) - DateTime.Now;
                Timer preTimer = MakeTimer(timeToGo.TotalMilliseconds, false, true);
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, notification);
            }
        }

        private DateTime IterateTime(DateTime startTime, int interval)
        {
            DateTime iterator = startTime;
            while (iterator <= DateTime.Now)
            {
                iterator = iterator.AddMinutes(interval); // ADD HOURS
            }
            return iterator;
        }

        public void RunPeriodically(Object source, System.Timers.ElapsedEventArgs e, Notification notification)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation(notification.Text);
            });
            Timer timer = MakeTimer(1000 * 60 * notification.Interval, true, true); // add *60
            timer.Elapsed += (sender, e_) => NotifyPeriodically(sender, e_, timer, notification);
        }

        public void NotifyPeriodically(Object source, System.Timers.ElapsedEventArgs e, Timer timer, Notification notification)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation(notification.Text);
            });
            if (notification.EndTime <= DateTime.Now)
            {
                timer.Enabled = false;
                timer.Stop();
            }
        }

        private Timer MakeTimer(double interval, bool autoReset, bool enabled)
        {
            Timer timer = new Timer(interval);
            timer.AutoReset = autoReset;
            timer.Enabled = enabled;
            timer.Start();
            timers.Add(timer);
            return timer;
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
        
        private void KillAllTimers()
        {
            foreach (Timer timer in timers)
            {
                timer.Enabled = false;
                timer.Stop();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            KillAllTimers();
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientCalendar();
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientMakeAppointmentFirst();
        }

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientAppointmentsPage();
        }

        private void btnHospitalPoll_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientHospitalPoll();
        }

        private void btnDoctorPoll_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientDoctorPoll();
        }

        private void btnMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientMedicalRecord();
        }

        private void btnCustomNotification_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientCustomNotification();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            app.userController.CurentLoggedUser = null;
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

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientPopularTimes();
        }
    }
}
