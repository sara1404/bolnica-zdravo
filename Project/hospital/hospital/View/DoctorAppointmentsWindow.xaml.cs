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
        public AppointmentController ac;
        public DoctorAppointmentsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            Appointments = ac.GetAppointmentByDoctor("miromir");
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
    }
}
