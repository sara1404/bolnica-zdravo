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
        Timer preTimer;
        private NotificationController nc;
        public PatientHomeWindow()
        {
            InitializeComponent();
            Main.Content = new PatientMainMenu();

            // loading all notifications
            App app = Application.Current as App;
            nc = app.notificationController;
            User current = app.userController.CurentLoggedUser;
            MedicalRecord mr = app.mediicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
            if (mr != null && mr.Therapy != null)
            {
                foreach (Therapy t in mr.Therapy)
                {
                    new Notification(t.TimeStart, t.TimeEnd, t.Interval, "Take " + t.Medicine.Name);
                }
            }

            foreach(Notification n in nc.FindAll().ToList())
            {
                if (n.Username.Equals(current.Username))
                {
                    preTimer = new Timer(1000);
                    preTimer.AutoReset = false;
                    preTimer.Elapsed += RunOnce;
                    preTimer.Start();
                    nc.Delete(n);
                }

            }
            

        }
        public void RunOnce(Object source, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Appointment has been moved. Look new appointment.");
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
    }
}
