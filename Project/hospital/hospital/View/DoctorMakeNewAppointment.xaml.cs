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
using hospital.Controller;
using hospital.Model;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for DoctorMakeNewAppointment.xaml
    /// </summary>
    public partial class DoctorMakeNewAppointment : Window
    {
        private DoctorController dc;
        private AppointmentController ac;
        private PatientController pc;
        private RoomController rc;
        private UserController uc;
        private ScheduledBasicRenovationController sbrc;

        public Doctor loggedInDoctor;
        public Patient selectedPatient;
        public DoctorMakeNewAppointment()
        {
            InitializeComponent();
            App app = Application.Current as App;
            dc = app.doctorController;
            ac = app.appointmentController;
            pc = app.patientController;
            rc = app.roomController;
            uc = app.userController;
            sbrc = app.scheduledBasicRenovationController;
            
            cmbPatients.ItemsSource = pc.FindAll();
            cmbOpRoom.ItemsSource = rc.FindAll();//dodati ovde proveru da izlistava samo "operation" sale
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            if (loggedInDoctor.Specialization == Specialization.general)
                cbOperation.IsEnabled = false;
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
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor(selectedDate, loggedInDoctor.Username, cmbPatients.Text);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1 && selectedPatient != null)
            {
                Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
                selectedAppointment.PatientUsername = selectedPatient.Username;
                selectedAppointment.Description = tbDescription.Text;
                if (cbOperation.IsChecked == true && cmbOpRoom.SelectedIndex != -1 && loggedInDoctor.Specialization != Specialization.general)
                    selectedAppointment.RoomId = ((Room)cmbOpRoom.SelectedItem).id;
                else
                    selectedAppointment.RoomId = loggedInDoctor.OrdinationId;

                List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
                bool canMake = true;
                foreach(ScheduledBasicRenovation renovation in renovationList)
                {
                    if(renovation._Room.id == selectedAppointment.RoomId && renovation._Interval._Start < selectedAppointment.StartTime && renovation._Interval._End > selectedAppointment.StartTime)
                    {
                        MessageBox.Show("Invalid time because of renovations");
                        canMake = false;
                    }
                }
                if (canMake)
                {
                    ac.CreateAppointment(selectedAppointment);
                    this.Close();
                }
            }
        }

        private void cbOperation_Checked(object sender, RoutedEventArgs e)
        {
            if(cbOperation.IsChecked == true)
            {
                cmbOpRoom.IsEnabled = true;
            }

        }

        private void cbOperation_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbOperation.IsChecked == false)
            { 
                cmbOpRoom.IsEnabled = false;
            }
        }
    }
}
