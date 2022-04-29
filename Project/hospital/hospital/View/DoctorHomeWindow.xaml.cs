using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for DoctorHomeWindow.xaml
    /// </summary>
    public partial class DoctorHomeWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private AppointmentController ac;
        private DoctorController dc;
        private UserController uc;

        private Doctor loggedInDoctor;
        public DoctorHomeWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            dc = app.doctorController;
            uc = app.userController;

            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
           

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 6000; //1min = 60000ms
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Appointments = ac.GetAppointmentByDoctor(loggedInDoctor.Username);
            foreach (Appointment a in Appointments)
            {
                if (a.StartTime <= DateTime.Now)
                {
                    if(!loggedInDoctor.myPatients.Contains(a.PatientUsername))
                    {
                        dc.addPatientToDoctorsList(a.PatientUsername, loggedInDoctor.Username);
                        //MessageBox.Show("DODAT PACIJENT U DOKTORA");
                    }
                }
            }
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            new DoctorAppointmentsWindow().Show();
        }

        private void MedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMedicalRecordsWindow().Show();
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {
            new DoctorTherapyWindow().Show();
        }
    }
}
