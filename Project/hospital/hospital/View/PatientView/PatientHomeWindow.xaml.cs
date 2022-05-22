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

        public PatientHomeWindow()
        {
            InitializeComponent();
            Main.Content = new PatientMainMenu();

            App app = Application.Current as App;
            nc = app.notificationController;
            current = app.userController.CurentLoggedUser;
            mr = app.medicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
            timers = new List<Timer>();

            StartTherapyNotifications();
            StartNotificationsFromFile();

            // check appointments notification
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
                // one time notification
                StartOneTimeNotification(notification);
            }
            else if(notification.StartTime != DateTime.MinValue && notification.EndTime != DateTime.MinValue)
            {
                // periodical notification
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
                Timer preTimer = new Timer();
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += (sender, e_) => RunOneTimeNotification(sender, e_, notification);
                preTimer.Start();
                timers.Add(preTimer);
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
                TimeSpan timeToGo = notification.StartTime - DateTime.Now;
                Timer preTimer = new Timer();
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, notification);
                preTimer.Start();
                timers.Add(preTimer);
            }
            else if (DateTime.Now > notification.StartTime && DateTime.Now <= notification.EndTime)
            {
                DateTime iterator = notification.StartTime;
                while (iterator <= DateTime.Now)
                {
                    iterator = iterator.AddMinutes(notification.Interval); // ADD HOURS
                }
                Timer preTimer = new Timer();
                TimeSpan timeToGo = iterator - DateTime.Now;
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, notification);
                preTimer.Start();
                timers.Add(preTimer);
            }
        }

        public void RunPeriodically(Object source, System.Timers.ElapsedEventArgs e, Notification notification)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation(notification.Text);
            });
            Timer timer = new Timer();
            timer.Interval = 1000 * 60 * notification.Interval; // ADD * 60
            timer.AutoReset = true;
            timer.Elapsed += (sender, e_) => NotifyPeriodically(sender, e_, timer, notification);
            timer.Enabled = true;
            timer.Start();
            timers.Add(timer);
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
    }
}
