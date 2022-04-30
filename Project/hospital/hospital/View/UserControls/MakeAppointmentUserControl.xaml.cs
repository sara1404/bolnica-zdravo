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
                ObservableCollection<Appointment> apointments = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, ((Doctor)cmbDoctor.SelectedItem).Username);
                for (int i = 0; i<apointments.Count;i++)
                {
                    string time = (apointments[i]).StartTime.ToString();
                    time = time.Split(' ')[1];
                    string hours = time.Split(':')[0];
                    string minuts = time.Split(':')[1];
                    if (txtTime.Text.Split(':')[0].Equals(hours) && txtTime.Text.Split(':')[1].Equals(minuts))
                    {  
                        //stavi id sobee kad sazanas
                        (apointments[i]).patientUsername = cmbUsername.Text;
                        (apointments[i]).roomId = "-333";
                        //pozovi controller i napravi appointment
                        ac.CreateAppointment(apointments[i]);
                        this.Visibility = Visibility.Collapsed;
                        return;
                    }  
                }
                btnShowRec.Visibility = Visibility.Visible;
                notFree.Text = "Appointment is not free";
            }
        }
        public Appointment RecommendedOne { set; get; }
        public Appointment RecommendedTwo { set; get; }
        private void findFreeForward(ObservableCollection<Appointment> apointments,string hours, string minuts)
        {
            //PROVERI DA LI JE 6 i 30
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;
                _hours = ++_hours;
            }
            else if (_minuts == 0)
            {
                _minuts = 30;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i= 0; i < apointments.Count;i++) {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours)==(_hours % 13) && Int32.Parse(itemMinuts)==_minuts)
                {
                    RecommendedOne=apointments[i];
                    return;
                }
            }
            findFreeForward(apointments,_hours.ToString(), _minuts.ToString());

        }
        private void findFreeBack(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            //PROVERI DA LI JE 7
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;
               
            }
            else if (_minuts == 0)
            {
                _minuts = 30;
                _hours = --_hours;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i = 0; i < apointments.Count; i++)
            {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours) == (_hours % 12) && Int32.Parse(itemMinuts) == _minuts)
                {
                    RecommendedTwo = apointments[i];
                    return;
                }
            }
            findFreeBack(apointments, _hours.ToString(), _minuts.ToString());

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
                    findFreeForward(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    findFreeBack(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(RecommendedOne.doctorUsername) + "\n" + RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(RecommendedTwo.doctorUsername) + "\n" + RecommendedTwo.StartTime;
                }
                else
                {
                    btnShowRec.Visibility = Visibility.Collapsed;
                    notFree.Text = "";
                    btnRecOne.Visibility = Visibility.Visible;
                    btnRecTwo.Visibility = Visibility.Visible;
                    ObservableCollection<Appointment> apointments = ac.GetFreeAppointmentsByDate((DateTime)date.SelectedDate);
                    findRecByTime(apointments, txtTime.Text.Split(':')[0], txtTime.Text.Split(':')[1]);
                    btnRecOne.Content = "Doctor: " + dc.GetByUsername(RecommendedOne.doctorUsername) + "\n" + RecommendedOne.StartTime;
                    btnRecTwo.Content = "Doctor: " + dc.GetByUsername(RecommendedTwo.doctorUsername) + "\n" + RecommendedTwo.StartTime;
                }
            }
        }

        private void findRecByTime(ObservableCollection<Appointment> apointments,string hours,string minuts)
        {
            bool oneRecFilled = false;
            foreach (Appointment appointment in apointments)
            {
                Console.WriteLine(appointment.doctorUsername +"+++"+ appointment.StartTime);
            }
            foreach(Appointment appointment in apointments)
            {
                string time = (appointment).StartTime.ToString();
                time = time.Split(' ')[1];
                string _hours = time.Split(':')[0];
                string _minuts = time.Split(':')[1];
                if(_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    if (oneRecFilled == false)
                    {
                        Console.WriteLine("Napunio prvog");
                        RecommendedOne = appointment;
                        oneRecFilled = true;
                    }
                    else
                    {
                        Console.WriteLine("Napunio drugog");
                        RecommendedTwo = appointment;
                        return;
                    }
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
            ac.CreateAppointment(RecommendedTwo);
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
            ac.CreateAppointment(RecommendedOne);
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
    }
}
