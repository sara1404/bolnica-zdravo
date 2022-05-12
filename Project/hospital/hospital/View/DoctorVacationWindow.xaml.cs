using Controller;
using Model;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorVacationWindow.xaml
    /// </summary>
    public partial class DoctorVacationWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private CalendarDateRange invalidDates;
        private VacationRequestController vc;
        private UserController uc;
        private AppointmentController ac;
        
        public DoctorVacationWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            vc = app.vacationRequestController;
            uc = app.userController;
            ac = app.appointmentController;
            Appointments = ac.GetAppointmentByDoctor(uc.CurentLoggedUser.Username);
            invalidDates = new CalendarDateRange(new DateTime(2010, 1, 1), DateTime.Today.AddDays(2));
            dpStartDate.BlackoutDates.Add(invalidDates);
            dpEndDate.BlackoutDates.Add(invalidDates);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool canReserve = true;
            foreach(Appointment appointment in Appointments)
            {
                if(appointment.StartTime > dpStartDate.SelectedDate && appointment.StartTime < dpEndDate.SelectedDate)
                {
                    canReserve = false;
                }
            }
            if(canReserve == true && cbHighPriority.IsChecked == true && dpStartDate.SelectedDate < dpEndDate.SelectedDate)
            {
                VacationRequest newRequest = new VacationRequest((DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate, (bool)cbHighPriority.IsChecked, tbNote.Text, uc.CurentLoggedUser.Username, -1);
                vc.Create(newRequest);
                MessageBox.Show("High priority vacation request sent!");
            }
            else if(canReserve == true && dpEndDate.SelectedDate != null && dpStartDate.SelectedDate != null && dpStartDate.SelectedDate < dpEndDate.SelectedDate)
            {
                VacationRequest newRequest = new VacationRequest((DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate, (bool)cbHighPriority.IsChecked, tbNote.Text, uc.CurentLoggedUser.Username, -1);
                vc.Create(newRequest);
                MessageBox.Show("Vacation request sent!");
            }
        }

        private void cbHighPriority_Checked(object sender, RoutedEventArgs e)
        {
            dpStartDate.BlackoutDates.Remove(invalidDates);
            dpEndDate.BlackoutDates.Remove(invalidDates);

            invalidDates.End = DateTime.Today;
            dpStartDate.BlackoutDates.Add(invalidDates);
            dpEndDate.BlackoutDates.Add(invalidDates);
        }

        private void cbHighPriority_Unchecked(object sender, RoutedEventArgs e)
        {
            dpStartDate.BlackoutDates.Remove(invalidDates);
            dpEndDate.BlackoutDates.Remove(invalidDates);
            //ovo ispod je da ne bi bacio gresku ako ostane selektovan nevalidan datum
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;

            invalidDates.End = DateTime.Today.AddDays(2);
            dpStartDate.BlackoutDates.Add(invalidDates);
            dpEndDate.BlackoutDates.Add(invalidDates);
        }
    }
}
