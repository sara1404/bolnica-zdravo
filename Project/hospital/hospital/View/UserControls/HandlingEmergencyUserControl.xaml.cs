using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model;
using Controller;
using ToastNotifications;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for HandlingEmergencyUserControl.xaml
    /// </summary>
    public partial class HandlingEmergencyUserControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Specialization> Specializations { get; set; }
        private PatientController _patientController;
        private AppointmentController _appointmentController;
        private EmergencyController _emergencyController;

        private Notifier Notifier { get; set; }
        public HandlingEmergencyUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _patientController = app.patientController;
            _emergencyController = app.emergencyController;
            _appointmentController = app.appointmentController;
            Patients = _patientController.FindAll();
            Specializations = new ObservableCollection<Specialization>(Enum.GetValues(typeof(Specialization)).Cast<Specialization>().ToList());
            Notifier = GetNotifier();
        }


        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            addGuestuserControl.Visibility = Visibility.Visible;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                try
                {
                    _emergencyController.tryMakeEmergencyAppointment(cmbPatient.Text, GetSpecialization(cmbSpecialization.Text),(bool)cbOperation.IsChecked);
                    Notifier.ShowSuccess("Successfully scheduled an emergency.");
                }catch(Exception ex)
                {
                    err.Text = ex.Message;
                    Console.WriteLine(ex.Message);
                    btnShowRec.Visibility = Visibility.Visible;
                }
            }
        }

        private Specialization GetSpecialization(string specialization)
        {
            switch (specialization)
            {
                case "General":
                    return Specialization.General;
                    break;
                case "Cardiologist":
                    return Specialization.Cardiologist;
                    break;
                case "Dermatologist":
                    return Specialization.Dermatologist;
                    break;
                case "Endocrinologist":
                    return Specialization.Endocrinologist;
                    break;
                case "Neurologist":
                    return Specialization.Neurologist;
                    break;
                default :
                    return Specialization.Oncologist;
            }
        }
        private bool isValidate()
        {
            bool[] isCorrected = new bool[2];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }
            
            if (cmbPatient.Text.Equals(""))
            {
                errPatient.Text = "Choose one option";
                isCorrected[0] = false;
            }
            else
            {
                errPatient.Text = "";
                isCorrected[0] = true;
            }
            
            if (cmbSpecialization.Text.Equals(""))
            {
                errSpecialization.Text = "Choose one option";
                isCorrected[1] = false;
            }
            else
            {
                errSpecialization.Text = "";
                isCorrected[1] = true;
            }
            return (isCorrected[0] && isCorrected[1]);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void cmbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errSpecialization.Text = "";
            btnShowRec.Visibility = Visibility.Collapsed;
            err.Text = "";
        }

        private void cmbPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errPatient.Text = "";
            btnShowRec.Visibility = Visibility.Collapsed;
            err.Text = "";
        }

        public static List<Appointment> OldAppointments { get; set; }
        public static List<Appointment> NewAppointments { get; set; }
        private void btnShowRec_Click(object sender, RoutedEventArgs e)
        {
            
            suggestedDelayUserControl.Visibility = Visibility.Visible;

            OldAppointments = _emergencyController.FindAppointmentsForCancelation();
            PewviewButton(OldAppointments);
            PreviewAppointment(OldAppointments, true);
            NewAppointments = _emergencyController.FindSuggestedAppointments();
            PreviewAppointment(NewAppointments, false);
        }
        private void PewviewButton(List<Appointment> appointments)
        {
            if (appointments.Count == 1)
                suggestedDelayUserControl.btnSuggestedOne.Visibility = Visibility.Visible;
            else if (appointments.Count == 2)
            {
                suggestedDelayUserControl.btnSuggestedOne.Visibility = Visibility.Visible;
                suggestedDelayUserControl.btnSuggestedTwo.Visibility = Visibility.Visible;
            }
            else if (appointments.Count == 3)
            {   
                suggestedDelayUserControl.btnSuggestedOne.Visibility = Visibility.Visible;
                suggestedDelayUserControl.btnSuggestedTwo.Visibility = Visibility.Visible;
                suggestedDelayUserControl.btnSuggestedThree.Visibility = Visibility.Visible;
            }
            
        }
        private void PreviewAppointment(List<Appointment> appointments,bool isOld)
        {
            if(isOld)
                for (int i = 1; i < appointments.Count+1; i++)
                {
                    SetOldValueOnButton("old" + i, appointments);
                }
            else
                for (int i = 1; i < appointments.Count + 1; i++)
                {
                    SetOldValueOnButton("new" + i, appointments);
                }
        }

        private void SetOldValueOnButton(string button, List<Appointment> appointments)
        {
            if (button == "old1")
                suggestedDelayUserControl.old1.Text = "Patient: " + appointments[0].PatientUsername + "\n" + "Doctor: " + appointments[0].DoctorUsername + "\n" + appointments[0].StartTime;
            else if (button == "old2")
                suggestedDelayUserControl.old2.Text = "Patient: " + appointments[1].PatientUsername + "\n" + "Doctor: " + appointments[1].DoctorUsername + "\n" + appointments[1].StartTime;
            else if (button == "old3")
                suggestedDelayUserControl.old3.Text = "Patient: " + appointments[2].PatientUsername + "\n" + "Doctor: " + appointments[2].DoctorUsername + "\n" + appointments[2].StartTime;
            else if (button == "new1")
                suggestedDelayUserControl.new1.Text = "Patient: " + appointments[0].PatientUsername + "\n" + "Doctor: " + appointments[0].DoctorUsername + "\n" + appointments[0].StartTime;
            else if (button == "new2")
                suggestedDelayUserControl.new2.Text = "Patient: " + appointments[1].PatientUsername + "\n" + "Doctor: " + appointments[1].DoctorUsername + "\n" + appointments[1].StartTime;
            else if (button == "new3")
                suggestedDelayUserControl.new3.Text = "Patient: " + appointments[2].PatientUsername + "\n" + "Doctor: " + appointments[2].DoctorUsername + "\n" + appointments[2].StartTime;
        }


        

        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
