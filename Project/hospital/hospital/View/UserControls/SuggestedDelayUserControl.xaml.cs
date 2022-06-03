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
using Controller;
using Model;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for SuggestedDelayUserControl.xaml
    /// </summary>
    public partial class SuggestedDelayUserControl : UserControl
    {
        private readonly AppointmentManagementController _appointmentController;
        private readonly EmergencyController _emergencyController;
        private List<Appointment> oldAppointments;
        private List<Appointment> newAppointments;
        public Notifier Notifier;
        public SuggestedDelayUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _emergencyController = app.emergencyController;
            Notifier = GetNotifier();
            _appointmentController = app.appointmentController;
        }
        private void btnSuggestedOne_Click(object sender, RoutedEventArgs e)
        {
            InitializeAppointments();
            _emergencyController.MoveAppointemntAndMakeNotification(oldAppointments[0], newAppointments[0]);
            Notifier.ShowSuccess("Successfully scheduled an emergency.");
            hiddenAllButton();
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSuggestedThree_Click(object sender, RoutedEventArgs e)
        {
            InitializeAppointments();
            _emergencyController.MoveAppointemntAndMakeNotification(oldAppointments[2], newAppointments[2]);
            Notifier.ShowSuccess("Successfully scheduled an emergency.");
            hiddenAllButton();
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSuggestedTwo_Click(object sender, RoutedEventArgs e)
        {
            InitializeAppointments();
            _emergencyController.MoveAppointemntAndMakeNotification(oldAppointments[1], newAppointments[1]);
            Notifier.ShowSuccess("Successfully scheduled an emergency.");
            hiddenAllButton();
            this.Visibility = Visibility.Collapsed;
        }
        private void InitializeAppointments()
        {
            oldAppointments = HandlingEmergencyUserControl.OldAppointments;
            newAppointments = HandlingEmergencyUserControl.NewAppointments;
        }
        private void hiddenAllButton()
        {
            btnSuggestedOne.Visibility = Visibility.Collapsed;
            btnSuggestedTwo.Visibility = Visibility.Collapsed;
            btnSuggestedThree.Visibility = Visibility.Collapsed;

        }
        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
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
}
