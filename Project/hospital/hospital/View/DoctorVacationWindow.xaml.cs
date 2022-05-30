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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorVacationWindow.xaml
    /// </summary>
    public partial class DoctorVacationWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        public ObservableCollection<VacationRequest> Requests { get; set; }
        private CalendarDateRange invalidDates;
        private Doctor loggedInDoctor;
        private VacationRequestController vc;
        private UserController uc;
        private AppointmentController ac;
        private DoctorController dc;
        
        public DoctorVacationWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            vc = app.vacationRequestController;
            uc = app.userController;
            ac = app.appointmentController;
            dc = app.doctorController;
            Appointments = ac.GetAppointmentByDoctor(uc.CurentLoggedUser.Username);
            invalidDates = new CalendarDateRange(new DateTime(2010, 1, 1), DateTime.Today.AddDays(2));
            dpStartDate.BlackoutDates.Add(invalidDates);
            dpEndDate.BlackoutDates.Add(invalidDates);
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            Requests = vc.FindAll();

            ICollectionView vacationView = CollectionViewSource.GetDefaultView(vc.FindAll());
            vacationView.Filter = VacationFilter;
        }

        private bool VacationFilter(object item)
        {
            VacationRequest request = item as VacationRequest;
            return request.DoctorId.Equals(loggedInDoctor.Username);
        }

        private bool checkOverlapWithAppointments()
        {
            foreach (Appointment appointment in Appointments)
            {
                if (appointment.StartTime > dpStartDate.SelectedDate && appointment.StartTime < dpEndDate.SelectedDate)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkOverlapWithOtherDoctorWithSameSpecialization()
        {
            foreach (VacationRequest vacationRequest in vc.FindAll())
            {
                if (loggedInDoctor.Specialization == dc.GetByUsername(vacationRequest.DoctorId).Specialization)
                {
                    if (dpStartDate.SelectedDate > vacationRequest.StartDate && dpStartDate.SelectedDate < vacationRequest.EndDate
                        && dpEndDate.SelectedDate > vacationRequest.StartDate && dpEndDate.SelectedDate < vacationRequest.EndDate)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void generateRequest()
        {
            VacationRequest newRequest = new VacationRequest((DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate, (bool)cbHighPriority.IsChecked, tbNote.Text, uc.CurentLoggedUser.Username, -1);
            vc.Create(newRequest);
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool isOveralappingWithAppointments = checkOverlapWithAppointments();
            bool isOverlappingWithOtherDoctorWithSameSpecialization = checkOverlapWithOtherDoctorWithSameSpecialization();
            
            if(isOveralappingWithAppointments == false && cbHighPriority.IsChecked == true && dpStartDate.SelectedDate < dpEndDate.SelectedDate)
            {
                generateRequest();
                MessageBox.Show("High priority vacation request sent!");
            }
            else if(isOverlappingWithOtherDoctorWithSameSpecialization == false && isOveralappingWithAppointments == false && dpEndDate.SelectedDate != null && dpStartDate.SelectedDate != null && dpStartDate.SelectedDate < dpEndDate.SelectedDate)
            {
                generateRequest();
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

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if(tableRequests.SelectedIndex != 1)
                new DoctorVacationViewWindow().Show();
        }
    }
}
