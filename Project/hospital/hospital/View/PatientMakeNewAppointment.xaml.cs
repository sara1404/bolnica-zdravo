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
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientMakeNewAppointment()
        {
            InitializeComponent();
            DoctorService ds = new DoctorService();
            cmbDoctors.ItemsSource = ds.GetDoctors();
            this.DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDoctors.SelectedIndex != -1)
            {
                this.DataContext = this;
                AppointmentController ac = new AppointmentController();
                Doctor d = (Doctor)cmbDoctors.SelectedItem;
                //Appointments = new ObservableCollection<Appointment>(ac.GetFreeAppointmentsByDoctor(d.Username));
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDoctor(d.Username);
            }

        }
    }
}
