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
    /// Interaction logic for PatientMakeAppointmentFirst.xaml
    /// </summary>
    public partial class PatientMakeAppointmentFirst : Page
    {
        private AppointmentController ac;
        private UserController uc;
        private App app;
        public PatientMakeAppointmentFirst()
        {
            InitializeComponent();
            dateFrom.DisplayDateStart = DateTime.Today;
            dateTo.DisplayDateStart = DateTime.Today;
            app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            DoctorController dc = app.doctorController;
            cbDoctor.ItemsSource = dc.GetDoctors();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            bool priorityDoctor = (bool)rbDoctor.IsChecked;
            bool priorityDate = (bool)rbDate.IsChecked;
            Doctor doctor = (Doctor)cbDoctor.SelectedItem;
            DateTime from, to;
            if(dateFrom.SelectedDate == null)
            {
                from = DateTime.MinValue;
            }
            else
            {
                from = (DateTime)dateFrom.SelectedDate;
            }

            if (dateTo.SelectedDate == null)
            {
                to = DateTime.MinValue;
            }
            else
            {
                to = (DateTime)dateTo.SelectedDate;
            }

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    if (dateFrom.SelectedDate != null && dateTo.SelectedDate != null && cbDoctor.SelectedItem != null && (priorityDoctor || priorityDate))
                    {
                        if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDoctor)
                        {
                            (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentSecond(doctor, from, to, priorityDoctor, priorityDate);
                        }
                        else if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDate)
                        {
                            (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentSecond(doctor, from, to, priorityDoctor, priorityDate);
                        }
                        else if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) >= 0)
                        {
                            lbWarning.Content = "Date from needs to be before date to!";
                        }
                        else
                        {
                            lbWarning.Content = "Choose priority(and doctor)!";
                        }
                    }
                    else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate == null && !priorityDoctor && !priorityDate)
                    {
                        Doctor d = (Doctor)cbDoctor.SelectedItem;
                        (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentSecond(doctor, from, to, priorityDoctor, priorityDate);

                    }
                    else if (cbDoctor.SelectedIndex == -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
                    {
                        DateTime selectedDate = (DateTime)dateFrom.SelectedDate;
                        (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentSecond(doctor, from, to, priorityDoctor, priorityDate);
                    }
                    else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
                    {
                        DateTime selectedDate = (DateTime)dateFrom.SelectedDate;
                        Doctor d = (Doctor)cbDoctor.SelectedItem;
                        (window as PatientHomeWindow).Main.Content = new PatientMakeAppointmentSecond(doctor, from, to, priorityDoctor, priorityDate);
                    }
                    lbWarning.Content = "Wrong selection of parameters!";
                }
            }
        } 
    }
}
