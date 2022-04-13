using Controller;
using Model;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientDelayAppointment.xaml
    /// </summary>
    public partial class PatientDelayAppointment : Window
    {
        private AppointmentController ac;
        private Appointment a;

        public PatientDelayAppointment()
        {
            InitializeComponent();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientAppointmentsWindow))
                {
                    a = (window as PatientAppointmentsWindow).appointmentTable.SelectedItem as Appointment;
                    tbxDoctor.Text = a.DoctorUsername;
                    oldDate.SelectedDate = a.StartTime;
                }
            }
            App app = Application.Current as App;
            ac = app.appointmentController;
            this.DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (newDate.SelectedDate != null)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, a.DoctorUsername);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedItem != null)
            {
                ac.UpdateAppointment(a, (Appointment)appointmentTable.SelectedItem);
                this.Close();
            }
        }
    }
}
