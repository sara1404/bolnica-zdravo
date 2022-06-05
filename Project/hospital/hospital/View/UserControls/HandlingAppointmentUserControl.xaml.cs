using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model;
using Controller;


namespace hospital.View.UserControls
{
    public partial class HandlingAppointmentUserControl : UserControl
    {
        public static ObservableCollection<Appointment> Appointments {get; set; }
        private ObservableCollection<Appointment> comingAppointmnet = new ObservableCollection<Appointment>();
        private PatientController pc;
        private AppointmentManagementController ac;
        private DoctorController dc;
        private AvailableAppointmentController aac;
        public HandlingAppointmentUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            foreach(Appointment appointment in ac.GetAppointments())
                if(DateTime.Now < appointment.StartTime)
                {
                    comingAppointmnet.Add(appointment);
                }
            Appointments = comingAppointmnet;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            makeAppointmentUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(dateGridHandlingAppointment.SelectedItem != null)
                editAppointmentUsercontrol.Visibility = Visibility.Visible;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(dateGridHandlingAppointment.SelectedItem != null)
            {
                ac.DeleteAppointment(((Appointment)dateGridHandlingAppointment.SelectedItem).Id);
                Appointments.Remove(((Appointment)dateGridHandlingAppointment.SelectedItem));
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filteredList = Appointments.Where(x => x.PatientUsername.ToLower().Contains(tbx.Text.ToLower()) || x.DoctorUsername.ToLower().Contains(tbx.Text.ToLower()) || x.StartTime.ToString().Contains(tbx.Text.ToLower()) || x.Duration.ToString().Contains(tbx.Text.ToLower()) || x.RoomId.ToLower().Contains(tbx.Text.ToLower())).ToList();
                dateGridHandlingAppointment.ItemsSource = null;
                dateGridHandlingAppointment.ItemsSource = filteredList;
            }
            else
            {
                dateGridHandlingAppointment.ItemsSource = Appointments;
            } 
        }
    }
}
