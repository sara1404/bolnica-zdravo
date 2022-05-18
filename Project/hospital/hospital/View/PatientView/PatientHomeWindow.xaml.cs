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

        private List<Timer> timers;

        public PatientHomeWindow()
        {
            InitializeComponent();
            Main.Content = new PatientMainMenu();

            App app = Application.Current as App;
            nc = app.notificationController;
            User current = app.userController.CurentLoggedUser;
            mr = app.medicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
            timers = new List<Timer>();

            StartTherapyNotifications();

            foreach(Notification n in nc.FindAll().ToList())
            {
                // startup notifications
                if (n.Username.Equals(current.Username))
                {
                    Timer preTimer = new Timer(1000);
                    preTimer.AutoReset = false;
                    preTimer.Elapsed += RunOnce;
                    preTimer.Start();
                    nc.Delete(n);
                }

            }
        }
        public void RunOnce(Object source, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Check appointments");
            });
        }
        

        private void StartTherapyNotifications()
        {
            if (mr != null && mr.Therapy != null)
            {
                foreach (Therapy t in mr.Therapy)
                {
                    StartTherapyNotificationTimer(t);
                }
            }
        }
        private void StartTherapyNotificationTimer(Therapy therapy)
        {
            if (DateTime.Now <= therapy.TimeStart)
            {
                TimeSpan timeToGo = therapy.TimeStart - DateTime.Now;
                Timer preTimer = new Timer();
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, therapy);
                preTimer.Start();
                timers.Add(preTimer);
            }
            else if (DateTime.Now > therapy.TimeStart && DateTime.Now <= therapy.TimeEnd)
            {
                DateTime iterator = therapy.TimeStart;
                while (iterator <= DateTime.Now)
                {
                    iterator = iterator.AddMinutes(therapy.Interval); // ADD HOURS
                }
                Timer preTimer = new Timer();
                TimeSpan timeToGo = iterator - DateTime.Now;
                preTimer.Interval = timeToGo.TotalMilliseconds;
                preTimer.AutoReset = false;
                preTimer.Elapsed += (sender, e) => RunPeriodically(sender, e, therapy);
                preTimer.Start();
                timers.Add(preTimer);
            }
        }

        public void RunPeriodically(Object source, System.Timers.ElapsedEventArgs e, Therapy therapy)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Take " + therapy.Medicine + "!");
            });
            Timer timer = new Timer();
            timer.Interval = 1000 * 60 * therapy.Interval; // ADD * 60
            timer.AutoReset = true;
            timer.Elapsed += (sender, e_) => NotifyPeriodically(sender, e_, timer, therapy);
            timer.Enabled = true;
            timer.Start();
            timers.Add(timer);
        }

        public void NotifyPeriodically(Object source, System.Timers.ElapsedEventArgs e, Timer timer, Therapy therapy)
        {
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Take " + therapy.Medicine + "!");
            });
            if (therapy.TimeEnd <= DateTime.Now)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach(Timer timer in timers)
            {
                timer.Stop();
            }
        }
    }
}
