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
    /// Interaction logic for PatientDelayAppointment.xaml
    /// </summary>
    public partial class PatientDelayAppointment : Window
    {
        private AppointmentController ac;
        private PatientController pc;
        private Appointment selectedAppointment;
        private UserController uc;

        public PatientDelayAppointment(Appointment a)
        {
            InitializeComponent();
            selectedAppointment = a;
            tbxDoctor.Text = selectedAppointment.DoctorUsername;
            oldDate.SelectedDate = selectedAppointment.StartTime;
            App app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            pc = app.patientController;
            newDate.DisplayDateStart = DateTime.Now > selectedAppointment.StartTime.AddDays(-4) ? DateTime.Now : selectedAppointment.StartTime.AddDays(-4);
            newDate.DisplayDateEnd = selectedAppointment.StartTime.AddDays(4);
            DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (newDate.SelectedDate != null)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, selectedAppointment.DoctorUsername,uc.CurentLoggedUser.Username);
            }
        }
        private void AddDelayOrCancelAppointment()
        {
            Patient currentPatient = pc.FindById(uc.CurentLoggedUser.Username);
            currentPatient.DelayOrCancelAppointment.Add(DateTime.Now);
            pc.UpdateByUsername(uc.CurentLoggedUser.Username, currentPatient);
        }
        private bool IsTroll()
        {
            Patient currentPatient = pc.FindById(uc.CurentLoggedUser.Username);
            int delayOrCancelCnt = 0;
            foreach (DateTime time in currentPatient.DelayOrCancelAppointment)
            {
                if (time >= DateTime.Now.AddDays(-30))
                {
                    delayOrCancelCnt++;
                }
            }
            if (delayOrCancelCnt >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BlockPatient()
        {
            Patient currentPatient = pc.FindById(uc.CurentLoggedUser.Username);
            currentPatient.IsBlocked = true;
            User blockedUser = uc.CurentLoggedUser;
            blockedUser.IsBlocked = true;
            uc.UpdateByUsername(uc.CurentLoggedUser.Username, blockedUser);
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedItem != null)
            {
                AddDelayOrCancelAppointment();
                if (IsTroll())
                {
                    BlockPatient();
                }
                ac.UpdateAppointment(selectedAppointment, (Appointment)appointmentTable.SelectedItem);
                Close();
            }
        }
    }
}
