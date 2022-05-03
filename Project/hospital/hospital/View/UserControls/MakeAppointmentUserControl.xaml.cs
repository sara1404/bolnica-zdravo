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

namespace hospital.View.UserControls
{
    public partial class MakeAppointmentUserControl : UserControl
    {
        private PatientController pc;
        private AppointmentController ac;
        private DoctorController dc;
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
            Patients = pc.FindAll();
            Doctors = dc.GetDoctors();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                if (ac.tryMakeAppointment(txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1],cmbUsername.Text, ((Doctor)cmbDoctor.SelectedItem).OrdinationId, (DateTime)date.SelectedDate, (Doctor)cmbDoctor.SelectedItem))
                {
                    this.Visibility = Visibility.Collapsed;
                    return;
                }
                btnRecOne.Visibility = Visibility.Collapsed;
                btnRecTwo.Visibility = Visibility.Collapsed;
                btnShowRec.Visibility = Visibility.Visible;
                notFree.Text = "Appointment is not free";
            }
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
                    if (txtTime.Text.Split(':')[0].Equals("6") && txtTime.Text.Split(':')[1].Equals("30"))
                        btnRecOne.Visibility = Visibility.Collapsed;
                    else
                        btnRecOne.Visibility = Visibility.Visible;
                    if (txtTime.Text.Split(':')[0].Equals("7") &&txtTime.Text.Split(':')[1].Equals("00"))
                        btnRecTwo.Visibility = Visibility.Collapsed;
                    else
                        btnRecTwo.Visibility = Visibility.Visible;
                    ObservableCollection<Appointment> apointments = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, ((Doctor)cmbDoctor.SelectedItem).Username);
                    ac.findFreeForward(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    ac.findFreeBack(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(ac.RecommendedOne.doctorUsername) + "\n" + ac.RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(ac.RecommendedTwo.doctorUsername) + "\n" + ac.RecommendedTwo.StartTime;
                }
                else
                {
                    btnShowRec.Visibility = Visibility.Collapsed;
                    notFree.Text = "";
                    btnRecOne.Visibility = Visibility.Visible;
                    btnRecTwo.Visibility = Visibility.Visible;
                    ObservableCollection<Appointment> apointments = ac.GetFreeAppointmentsByDate((DateTime)date.SelectedDate);
                    ac.findRecByTime(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(ac.RecommendedOne.doctorUsername) + "\n" + ac.RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(ac.RecommendedTwo.doctorUsername) + "\n" + ac.RecommendedTwo.StartTime;
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text =  "";
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;    
        }

        private void btnRecTwo_Click(object sender, RoutedEventArgs e)
        {
            ac.CreateAppointment(ac.RecommendedTwo);
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnRecOne_Click(object sender, RoutedEventArgs e)
        {
            ac.CreateAppointment(ac.RecommendedOne);
            this.Visibility = Visibility.Collapsed;
            cmbDoctor.Text = "";
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            radioDoctor.IsChecked = false;
            radioTime.IsChecked = false;
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnDelay_Click(object sender, RoutedEventArgs e)
        {
            delayAppointmentUserControl.Visibility = Visibility.Visible;
        }
    }
}
