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
    /// Interaction logic for DoctorViewInfoWindow.xaml
    /// </summary>
    public partial class DoctorViewInfoWindow : Window
    {
        private PatientController pc;
        private Patient selectedPatient;
        private Appointment selectedAppointment;
        public DoctorViewInfoWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pc = app.patientController;
            this.DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorAppointmentsWindow))
                {
                    selectedAppointment = (window as DoctorAppointmentsWindow).Table.SelectedItem as Appointment;
                    selectedPatient = pc.FindById(selectedAppointment.patientUsername);
                    lbFirstName.Content = selectedPatient.FirstName;
                    lbLastName.Content = selectedPatient.LastName;
                    lbUsername.Content = selectedPatient.Username;
                    lbDateOfBirth.Content = selectedPatient.DateOfBirth;
                    lbPhoneNumber.Content = selectedPatient.PhoneNumber;
                }
            }
        }
    }
}
