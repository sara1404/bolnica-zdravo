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

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for DoctorMakeNewAppointment.xaml
    /// </summary>
    public partial class DoctorMakeNewAppointment : Window
    {
        public DoctorController dc;
        public AppointmentController ac;
        public PatientController pc;

        public Doctor loggedInDoctor;
        public Patient selectedPatient;
        public DoctorMakeNewAppointment()
        {
            InitializeComponent();
            App app = Application.Current as App;
            dc = app.doctorController;
            ac = app.appointmentController;
            pc = app.patientController;
            cmbPatients.ItemsSource = pc.FindAll();
            loggedInDoctor = dc.GetDoctors().First<Doctor>(); //za sad zakucamo
            this.DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatients.SelectedIndex != -1 && date.SelectedDate == null)
            {
                this.DataContext = this;
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDoctor(loggedInDoctor.Username);
            }
            if (cmbPatients.SelectedIndex != -1 && date.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)date.SelectedDate;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor(selectedDate, loggedInDoctor.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1 && selectedPatient != null)
            {
                Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
                selectedAppointment.Patient = selectedPatient;
                selectedAppointment.Description = tbDescription.Text;
                ac.CreateAppointment(selectedAppointment);
                this.Close();
            }
        }
    }
}
