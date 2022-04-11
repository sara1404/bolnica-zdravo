using Controller;
using Model;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointment.xaml
    /// </summary>
    public partial class DoctorEditAppointment : Window
    {
        private AppointmentController ac;
        private Appointment selectedAppointment;
        public DoctorEditAppointment()
        {
            InitializeComponent();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorAppointmentsWindow))
                {
                    selectedAppointment = (window as DoctorAppointmentsWindow).Table.SelectedItem as Appointment;
                    tbPatient.Text = selectedAppointment.patient.ToString(); //moze i a.PatientName()
                    date.SelectedDate = selectedAppointment.StartTime;
                    tbDescription.Text = selectedAppointment.Description;
                }
            }
            App app = Application.Current as App;
            ac = app.appointmentController;
            this.DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (date.SelectedDate != null)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, selectedAppointment.doctor.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedItem != null)
            {
                Appointment updatedAppointment = (Appointment)appointmentTable.SelectedItem;
                updatedAppointment.Description = tbDescription.Text;
                ac.UpdateAppointment(selectedAppointment, updatedAppointment);
                this.Close();
            }
        }
    }
}
