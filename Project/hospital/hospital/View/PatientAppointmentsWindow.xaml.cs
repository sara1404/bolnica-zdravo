using System.Windows;
using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Service;
using System;
using hospital.Model;
using System.ComponentModel;
using System.Windows.Data;

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
            DataContext = this;
            User current = app.userController.CurentLoggedUser;
            Appointments = ac.GetAppointments();

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = UserFilter;

            MedicalRecord mr = app.mediicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
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
            Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
            if (selectedAppointment != null)
            {
                if(ac.CanBeDelayed(selectedAppointment))
                {
                    new PatientDelayAppointment().Show();
                }
                else
                {
                    MessageBox.Show("You can't delay an appointment now!");
                }
            }
        }

        private void btnRecommended_Click(object sender, RoutedEventArgs e)
        {
            new PatientRecommendedAppointments().Show();
        }
        private bool UserFilter(object item)
        {
            Appointment appointment = item as Appointment;
            App app = Application.Current as App;
            User current = app.userController.CurentLoggedUser;
            return appointment.PatientUsername.Equals(current.Username);
        }
    }
}
