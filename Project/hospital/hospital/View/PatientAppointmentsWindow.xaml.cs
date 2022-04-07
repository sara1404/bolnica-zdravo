using System.Windows;
using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            
        }
    }
}
