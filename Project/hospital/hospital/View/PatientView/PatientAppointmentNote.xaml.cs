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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientAppointmentNote.xaml
    /// </summary>
    public partial class PatientAppointmentNote : Page
    {
        private Appointment oldAppointment;
        private App app;
        private AppointmentManagementController ac;
        public PatientAppointmentNote(Appointment appointment)
        {
            InitializeComponent();
            app = Application.Current as App;
            ac = app.appointmentController;
            tbDoctorsNote.Text = appointment.DoctorNote;
            tbPatientsNote.Text = appointment.PatientNote;
            oldAppointment = appointment;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Appointment newAppointment = oldAppointment;
            newAppointment.PatientNote = tbPatientsNote.Text;
            ac.UpdateAppointment(oldAppointment, newAppointment);
        }

    }
}
