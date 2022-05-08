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
using ToastNotifications.Messages;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for SuggestedDelayUserControl.xaml
    /// </summary>
    public partial class SuggestedDelayUserControl : UserControl
    {
        private readonly AppointmentController _appointmentController;
        private List<Appointment> oldAppointments;
        private List<Appointment> newAppointments;
        public Notifier _notifier;
        public SuggestedDelayUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _notifier = app.Notifier;
            _appointmentController = app.appointmentController;
        }
        private void btnSuggestedOne_Click(object sender, RoutedEventArgs e)
        {
            _appointmentController.MoveAppointemntAndMakeNotification(oldAppointments[0], newAppointments[0]);
            _notifier.ShowSuccess("Successfully scheduled an emergency.");
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSuggestedThree_Click(object sender, RoutedEventArgs e)
        {
            _appointmentController.MoveAppointemntAndMakeNotification(oldAppointments[2], newAppointments[2]);
            _notifier.ShowSuccess("Successfully scheduled an emergency.");
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSuggestedTwo_Click(object sender, RoutedEventArgs e)
        {
            _appointmentController.MoveAppointemntAndMakeNotification(oldAppointments[1], newAppointments[1]);
            _notifier.ShowSuccess("Successfully scheduled an emergency.");
            this.Visibility = Visibility.Collapsed;
        }
    }
}
