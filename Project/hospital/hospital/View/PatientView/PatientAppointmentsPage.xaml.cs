using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsPage.xaml
    /// </summary>
    public partial class PatientAppointmentsPage : Page
    {
        private AppointmentController ac;
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientAppointmentsPage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ac = app.appointmentController;
            this.DataContext = this;
            User current = app.userController.CurentLoggedUser;
            Appointments = ac.GetAppointments();

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            Appointment appointment = item as Appointment;
            App app = Application.Current as App;
            User current = app.userController.CurentLoggedUser;
            return appointment.PatientUsername.Equals(current.Username);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientMainMenu();
                }
            }
        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
            if (selectedAppointment != null)
            {
                if (ac.CanBeDelayed(selectedAppointment))
                {
                    //PatientDelayAppointment p = new PatientDelayAppointment();
                    //NavigationService.Navigate(p, selectedAppointment);
                    new PatientDelayAppointment(selectedAppointment).Show();
                }
                else
                {
                    MessageBox.Show("You can't delay an appointment now!");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                ac.DeleteAppointment(Convert.ToInt32(appointmentTable.SelectedItem.ToString()));
            }
        }
    }
}
