using System.Windows;
using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Service;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsWindow.xaml
    /// </summary>
    public partial class PatientAppointmentsWindow : Window
    {
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientAppointmentsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentController ac = new AppointmentController();
            Appointments = new ObservableCollection<Appointment>(ac.GetAppointmentByPatient("peromir"));
            //Appointments = new ObservableCollection<Appointment>(ac.GetFreeAppointmentsByDoctor("miromir"));
        }

        private void btnNewAppointment_Click(object sender, RoutedEventArgs e)
        {
            new PatientMakeNewAppointment().Show();
        }
    }
}
