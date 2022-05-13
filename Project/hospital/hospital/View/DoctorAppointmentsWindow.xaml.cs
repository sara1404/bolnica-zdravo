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
using Controller;
using hospital.View.UserControls;
using Model;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentsWindow.xaml
    /// </summary>
    public partial class DoctorAppointmentsWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private AppointmentController ac;
        private UserController uc;
        private DoctorController dc;

        private Doctor loggedInDoctor;
        public DoctorAppointmentsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            uc = app.userController;
            dc = app.doctorController;
            //Appointments = ac.GetAppointmentByDoctor(uc.CurentLoggedUser.Username);
            Appointments = ac.GetAppointments();
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = DoctorFilter;
        }

        private bool DoctorFilter(object item)
        {
            Appointment appointment = item as Appointment;
            return appointment.DoctorUsername.Equals(loggedInDoctor.Username);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {
                new DoctorEditAppointment().Show();
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMakeNewAppointment().Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {
                Appointment toDelete = (Appointment)Table.SelectedItem;
                ac.DeleteAppointment(toDelete.Id);
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            if(Table.SelectedIndex != -1)
            {
                new DoctorViewInfoWindow().Show();
            }
        }

        private void btnOtherDoctor_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMakeNewAppointmentForOtherDoctors().Show();
        }
    }
}
