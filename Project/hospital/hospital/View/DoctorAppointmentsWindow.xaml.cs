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
            ac = new AppointmentController();
            Appointments = ac.GetAppointmentByDoctor("miromir");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {

                ac.DeleteAppointment(Convert.ToInt32(Table.SelectedItem.ToString()));
            }
        }
    }
}
