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
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using hospital.Model;
using hospital.Controller;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for DelayAppointmentUserControl.xaml
    /// </summary>
    public partial class DelayAppointmentUserControl : UserControl
    {
        private PatientController pc;
        private AppointmentController ac;
        private DoctorController dc;
        private NotificationController nc;
        private ScheduledBasicRenovationController sbrc;
        private RecommendedAppointmentController rc;
        private AvailableAppointmentController aac;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public DelayAppointmentUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            date.DisplayDateStart = DateTime.Today;
            newDate.DisplayDateStart = DateTime.Today;
            App app = Application.Current as App;
            pc = app.patientController;
            nc = app.notificationController;
            sbrc = app.scheduledBasicRenovationController;
            ac = app.appointmentController;
            dc = app.doctorController;
            rc = app.recommendedAppointmentController;
            aac = app.availableAppointmentController;
            Patients = pc.FindAll();
            Doctors = dc.GetDoctors();
        }


        Notifier notifier = new Notifier(cfg =>
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



        private bool isValidate()
        {
            bool[] isCorrected = new bool[5];

            for (int i = 0; i < 4; i++)
            {
                isCorrected[i] = true;
            }
            //username
            if (cmbUsername.Text.Equals(""))
            {
                errUsername.Text = "Choose one option";
                isCorrected[0] = false;
            }
            else
            {
                errUsername.Text = "";
                isCorrected[0] = true;
            }
            //date
            if (date.Text.Equals(""))
            {
                errDate.Text = "Choose one date";
                isCorrected[2] = false;
            }
            else
            {
                errDate.Text = "";
                isCorrected[2] = true;
            }
            //time
            if (txtTime.Text.Equals(""))
            {
                errTime.Text = "Must be filled";
                isCorrected[3] = false;
            }
            else
            {
                errTime.Text = "";
                isCorrected[3] = true;
            }
            //newdate
            if (newDate.Text.Equals(""))
            {
                errNewDate.Text = "Choose one date";
                isCorrected[1] = false;
            }
            else
            {
                errNewDate.Text = "";
                isCorrected[1] = true;
            }
            //newtime
            if (txtNewTime.Text.Equals(""))
            {
                errNewTime.Text = "Must be filled";
                isCorrected[4] = false;
            }
            else
            {
                errNewTime.Text = "";
                isCorrected[4] = true;
            }


            return (isCorrected[0] && isCorrected[1] && isCorrected[2] && isCorrected[3] && isCorrected[4]);
        }

        private void cmbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errUsername.Text = "";
        }


        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == date)
                if (date.Text.Equals(""))
                    errDate.Text = "Choose one date";
                else
                    errDate.Text = "";
            if (e.Source == newDate)
                if (newDate.Text.Equals(""))
                    errNewDate.Text = "Choose one date";
                else
                    errNewDate.Text = "";
        }

        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source == txtTime)
                errTime.Text = "";
            if (e.Source == txtNewTime)
                errNewTime.Text = "";

        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnRecTwo_Click(object sender, RoutedEventArgs e)
        {
            rc.tryChangeAppointment(CurrentAppointment, (DateTime)newDate.SelectedDate, rc.RecommendedTwo.StartTime.ToString().Split(' ')[1]);
            this.Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            newDate.Text = "";
            txtNewTime.Text = "";
            errNewDate.Text = "";
            errNewTime.Text = "";
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnRecOne_Click(object sender, RoutedEventArgs e)
        {
            rc.tryChangeAppointment(CurrentAppointment, (DateTime)newDate.SelectedDate, rc.RecommendedOne.StartTime.ToString().Split(' ')[1]);
            this.Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            newDate.Text = "";
            txtNewTime.Text = "";
            errNewDate.Text = "";
            errNewTime.Text = "";
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnShowRec_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                btnShowRec.Visibility = Visibility.Collapsed;
                notFree.Text = "";
                if (txtNewTime.Text.Split(':')[0].Equals("6") && txtNewTime.Text.Split(':')[1].Equals("30"))
                    btnRecOne.Visibility = Visibility.Collapsed;
                else
                    btnRecOne.Visibility = Visibility.Visible;
                if (txtNewTime.Text.Split(':')[0].Equals("7") && txtNewTime.Text.Split(':')[1].Equals("00"))
                    btnRecTwo.Visibility = Visibility.Collapsed;
                else
                    btnRecTwo.Visibility = Visibility.Visible;
                ObservableCollection<Appointment> apointments = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, CurrentAppointment.DoctorUsername,cmbUsername.Text);
                rc.findFreeForward(apointments, txtNewTime.Text.Split(':')[0], txtNewTime.Text.Split(':')[1]);
                rc.findFreeBack(apointments, txtNewTime.Text.Split(':')[0], txtNewTime.Text.Split(':')[1]);
                btnRecOne.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedOne.DoctorUsername) + "\n" + rc.RecommendedOne.StartTime;
                btnRecTwo.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedTwo.DoctorUsername) + "\n" + rc.RecommendedTwo.StartTime;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        public Appointment CurrentAppointment {get;set;}
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                Appointment oldAppointment;
                bool sucess = false;
                ObservableCollection<Appointment> appointments= ac.GetAppointmentByPatient(cmbUsername.Text);
                string hours = txtTime.Text.Split(':')[0];
                string minuts = txtTime.Text.Split(':')[1];
                string dates = date.ToString().Split(' ')[0];
                bool exists = false;

                //da li je uopste uneta promena nekakva
                if (date.Text.Equals(newDate.Text) && txtNewTime.Text.Trim().Equals(txtTime.Text.Trim()))
                {
                    notFree.Text = "No change occured";
                }


                foreach (Appointment appointment in appointments)
                {
                        string hoursFromAppointment = appointment.StartTime.ToString().Split(' ')[1].Split(':')[0];
                        string minutsFromAppointment = appointment.StartTime.ToString().Split(' ')[1].Split(':')[1];
                        string dateFromAppointment = appointment.StartTime.ToString().Split(' ')[0];
                        if (hoursFromAppointment.Equals(hours) && minutsFromAppointment.Equals(minuts) && dates.Equals(dateFromAppointment))
                        {
                            exists = true;
                            CurrentAppointment = appointment;

                            List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
                            bool canMake = true;
                            foreach (ScheduledBasicRenovation renovation in renovationList)
                            {
                                if (renovation._Room.id == dc.GetByUsername(appointment.DoctorUsername).OrdinationId && renovation._Interval._Start <= (DateTime)newDate.SelectedDate && renovation._Interval._End >= (DateTime)newDate.SelectedDate)
                                {
                                    notifier.ShowError("Invalid time because of renovations");
                                    canMake = false;
                                }
                            }

                            if (canMake)
                            {
                                sucess = rc.tryChangeAppointment(appointment, (DateTime)newDate.SelectedDate, txtNewTime.Text);
                                if (sucess)
                                {
                                    notifier.ShowSuccess("Appointment has been moved successfully.");
                                    this.Visibility = Visibility.Collapsed;
                                    return;
                                }
                                else
                                {
                                    btnShowRec.Visibility = Visibility.Visible;
                                    notFree.Text = "Appointment is not free";
                                    break;
                                }
                            }
                        }
                }
                if (!exists)
                {
                    notFree.Text = "Appointment not exists";
                    return;
                }
            }
        }
    }
}
