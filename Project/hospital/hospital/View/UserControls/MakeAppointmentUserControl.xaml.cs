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
using Controller;
using Model;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using hospital.Controller;
using hospital.Model;

namespace hospital.View.UserControls
{
    public partial class MakeAppointmentUserControl : UserControl
    {
        private PatientController pc;
        private AppointmentManagementController ac;
        private DoctorController dc;
        private ScheduledBasicRenovationController sbrc;
        private RecommendedAppointmentController rc;
        private AvailableAppointmentController aac;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public MakeAppointmentUserControl()
        {
            InitializeComponent();
            this.DataContext=this;
            date.DisplayDateStart = DateTime.Today;
            App app = Application.Current as App;
            pc = app.patientController;
            ac = app.appointmentController;
            dc = app.doctorController;
            sbrc = app.scheduledBasicRenovationController;
            aac = app.availableAppointmentController;
            rc = app.recommendedAppointmentController;
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
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
                bool canMake = true;
                foreach (ScheduledBasicRenovation renovation in renovationList)
                {
                    if (renovation._Room.id == ((Doctor)cmbDoctor.SelectedItem).OrdinationId && renovation._Interval._Start <= (DateTime)date.SelectedDate && renovation._Interval._End >= (DateTime)date.SelectedDate)
                    {
                        notifier.ShowError("Invalid time because of renovations");
                        canMake = false;
                    }
                }
                if (canMake)
                {
                    DateTime tmp = (DateTime) date.SelectedDate;
                    DateTime tmp1 = (DateTime)txtTime.Value;
                    DateTime newDate = new DateTime(tmp.Year,tmp.Month,tmp.Day, tmp1.Hour, tmp1.Minute,0);
                    if (rc.TryMakeAppointment(cmbUsername.Text, newDate, (Doctor) cmbDoctor.SelectedItem))
                    {
                        this.Visibility = Visibility.Collapsed;
                        ResetFields();
                        notifier.ShowSuccess("Appointment successfully scheduled");
                        return;
                    }
                    btnRecOne.Visibility = Visibility.Collapsed;
                    btnRecTwo.Visibility = Visibility.Collapsed;
                    btnShowRec.Visibility = Visibility.Visible;
                    notFree.Text = "Appointment is not free";
                }
            }
        }
        private void ResetFields()
        {
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            cmbDoctor.Text = "";
            errDate.Text = "";
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
        }

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
            //doctor
            if (cmbDoctor.Text.Equals(""))
            {
                errDoctor.Text = "Choose one option";
                isCorrected[1] = false;
            }
            else
            {
                errDoctor.Text = "";
                isCorrected[1] = true;
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
            //if (txtTime.Text.Equals(""))
            //{
               // errTime.Text = "Must be filled";
               // isCorrected[3] = false;
            //}
            //else
           // {
            //    errTime.Text = "";
            //    isCorrected[3] = true;
           // }
            //radio
            if(radioDoctor.IsChecked == false && radioTime.IsChecked == false)
            {
                errPriority.Text = "Choose one option";
                isCorrected[4] = false;
            }
            else
            {
                errPriority.Text = "";
                isCorrected[4] = true;
            }


            return (isCorrected[0] && isCorrected[1] && isCorrected[2] && isCorrected[3] && isCorrected[4]);
        }

        private void cmbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errUsername.Text = "";
        }

        private void cmbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errDoctor.Text = "";
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (date.Text.Equals(""))
                errDate.Text = "Choose one date";
            else
                errDate.Text = "";
        }

        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {
            errTime.Text = "";
        }

        private void radioDoctor_Checked(object sender, RoutedEventArgs e)
        {
            errPriority.Text = "";
        }

        private void radioTime_Checked(object sender, RoutedEventArgs e)
        {
            errPriority.Text = "";
        }

        private void btnShowRec_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                if (radioDoctor.IsChecked == true)
                {
                    btnShowRec.Visibility = Visibility.Collapsed;
                    notFree.Text = "";
                    if (((DateTime) txtTime.Value).Hour.Equals("6") && ((DateTime)txtTime.Value).Minute.Equals("30"))
                        btnRecOne.Visibility = Visibility.Collapsed;
                    else
                        btnRecOne.Visibility = Visibility.Visible;
                    if (((DateTime)txtTime.Value).Hour.Equals("7") && ((DateTime)txtTime.Value).Minute.Equals("00"))
                        btnRecTwo.Visibility = Visibility.Collapsed;
                    else
                        btnRecTwo.Visibility = Visibility.Visible;
                    ObservableCollection<Appointment> apointments = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, ((Doctor)cmbDoctor.SelectedItem).Username,cmbUsername.Text);
                    rc.FindFreeForward(apointments, (DateTime) txtTime.Value);
                    rc.FindFreeBack(apointments, (DateTime) txtTime.Value);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedOne.DoctorUsername) + "\n" + rc.RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedTwo.DoctorUsername) + "\n" + rc.RecommendedTwo.StartTime;
                }
                else
                {
                    btnShowRec.Visibility = Visibility.Collapsed;
                    notFree.Text = "";
                    btnRecOne.Visibility = Visibility.Visible;
                    btnRecTwo.Visibility = Visibility.Visible;
                    ObservableCollection<Appointment> apointments = aac.GetFreeAppointmentsByDate((DateTime)date.SelectedDate,cmbUsername.Text);
                    rc.FindRecByTime(apointments,(DateTime) txtTime.Value);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedOne.DoctorUsername) + "\n" + rc.RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(rc.RecommendedTwo.DoctorUsername) + "\n" + rc.RecommendedTwo.StartTime;
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;    
        }

        private void btnRecTwo_Click(object sender, RoutedEventArgs e)
        {
            ac.CreateAppointment(rc.RecommendedTwo);
            notifier.ShowSuccess("Appointment successfully scheduled");
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,12,0,0);
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnRecOne_Click(object sender, RoutedEventArgs e)
        {
            ac.CreateAppointment(rc.RecommendedOne);
            notifier.ShowSuccess("Appointment successfully scheduled");
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,12,0,0);
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }
    }
}
