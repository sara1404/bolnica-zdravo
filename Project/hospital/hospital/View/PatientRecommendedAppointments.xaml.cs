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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientRecommendedAppointments.xaml
    /// </summary>
    public partial class PatientRecommendedAppointments : Window
    {
        private AppointmentController ac;
        public PatientRecommendedAppointments()
        {
            InitializeComponent();
            dateStart.DisplayDateStart = DateTime.Today;
            dateEnd.DisplayDateStart = DateTime.Today;
            App app = Application.Current as App;
            ac = app.appointmentController;
            DoctorController dc = app.doctorController;
            cbxDoctor.ItemsSource = dc.GetDoctors();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if(dateStart.SelectedDate != null && dateEnd.SelectedDate != null && cbxDoctor.SelectedItem != null)
            {
                if(dateStart.SelectedDate.Value.CompareTo(dateEnd.SelectedDate.Value) < 0 && ((bool)rbDoctor.IsChecked || (bool)rbDate.IsChecked))
                {
                    appointmentTable.ItemsSource = ac.GetRecommendedAppointments((DateTime)dateStart.SelectedDate, (DateTime)dateEnd.SelectedDate, (Doctor)cbxDoctor.SelectedItem, (bool)rbDoctor.IsChecked);
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                Appointment a = (Appointment)appointmentTable.SelectedItem;
                ac.CreateAppointment(a);
                Close();
            }
        }
    }
}
