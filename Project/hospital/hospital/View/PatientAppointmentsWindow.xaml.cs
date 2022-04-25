using System.Windows;
using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Service;
using System;
using hospital.Model;

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
            User current = app.userController.CurentLoggedUser;
            Appointments = ac.GetAppointmentByPatient(current.Username);
            MedicalRecord mr = app.patientController.FindById(current.Username).MedicalRecord;
            if(mr != null && mr.Therapy != null)
            {
                foreach (Therapy t in mr.Therapy)
                {
                    new Notification(t.TimeStart, t.TimeEnd, t.Interval, "Take " + t.Medicine.Name);
                }
            }
            

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

        private void btnRecommended_Click(object sender, RoutedEventArgs e)
        {
            new PatientRecommendedAppointments().Show();
        }
    }
}
