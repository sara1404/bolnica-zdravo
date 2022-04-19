using Controller;
using Model;
using Service;
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
    /// Interaction logic for PatientMakeNewAppointment.xaml
    /// </summary>
    public partial class PatientMakeNewAppointment : Window
    {
        private AppointmentController ac;

        public PatientMakeNewAppointment()
        {
            InitializeComponent();
            date.DisplayDateStart = DateTime.Today;
            //DoctorService ds = new DoctorService();
            App app = Application.Current as App;
            DoctorController dc = app.doctorController;
            cmbDoctors.ItemsSource = dc.GetDoctors();
            ac = app.appointmentController;
            this.DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            if (cmbDoctors.SelectedIndex != -1 && date.SelectedDate == null)
            {
                Doctor d = (Doctor)cmbDoctors.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDoctor(d.Username);
                
            }
            if (cmbDoctors.SelectedIndex == -1 && date.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)date.SelectedDate;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDate(selectedDate);
            }
            if (cmbDoctors.SelectedIndex != -1 && date.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)date.SelectedDate;
                Doctor d = (Doctor)cmbDoctors.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor(selectedDate, d.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedIndex != -1)
            {
                Appointment a = (Appointment)appointmentTable.SelectedItem;
                ac.CreateAppointment(a);
                Close();
            }
        }
    }
}
