using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentsPage.xaml
    /// </summary>
    public partial class PatientAppointmentsPage : Page
    {
        private AppointmentController ac;
        private PatientController pc;
        private UserController uc;
        private App app;
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }
        public PatientAppointmentsPage()
        {
            InitializeComponent();
            app = Application.Current as App;
            ac = app.appointmentController;
            pc = app.patientController;
            uc = app.userController;
            this.DataContext = this;
            User current = app.userController.CurentLoggedUser;
            Appointments = ac.GetAppointments();

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            Appointment appointment = item as Appointment;
            User current = app.userController.CurentLoggedUser;
            return appointment.PatientUsername.Equals(current.Username);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
            if (selectedAppointment != null)
            {
                if (ac.CanBeDelayed(selectedAppointment))
                {
                    new PatientDelayAppointment(selectedAppointment).Show();
                }
                else
                {
                    MessageBox.Show("You can't delay an appointment now!");
                }
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
            foreach(DateTime time in currentPatient.DelayOrCancelAppointment)
            {
                if (time >= DateTime.Now.AddDays(-30))
                {
                    delayOrCancelCnt++;
                }
            }
            if(delayOrCancelCnt >= 5)
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
            //pc.UpdateByUsername(uc.CurentLoggedUser.Username, currentPatient);
            User blockedUser = uc.CurentLoggedUser;
            blockedUser.IsBlocked = true;
            uc.UpdateByUsername(uc.CurentLoggedUser.Username, blockedUser);
        }
        public void LogoutUser()
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Close();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                AddDelayOrCancelAppointment();
                if (IsTroll())
                {
                    BlockPatient();
                    LogoutUser();
                }
                ac.DeleteAppointment(Convert.ToInt32(appointmentTable.SelectedItem.ToString()));
            }
        }
    }
}
