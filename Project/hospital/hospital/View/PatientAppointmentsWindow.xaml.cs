using System.Windows;
using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Service;
using System;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsWindow.xaml
    /// </summary>
    public partial class PatientAppointmentsWindow : Window
    {
        private AppointmentController ac;
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientAppointmentsWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ac = app.appointmentController;
            this.DataContext = this;
            Appointments = ac.GetAppointmentByPatient("peromir");

        }

        private void btnNewAppointment_Click(object sender, RoutedEventArgs e)
        {
            new PatientMakeNewAppointment().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                ac.DeleteAppointment(Convert.ToInt32(appointmentTable.SelectedItem.ToString()));
            }
        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedIndex != -1)
            {
                new PatientDelayAppointment().Show();
            }
        }
    }
}
