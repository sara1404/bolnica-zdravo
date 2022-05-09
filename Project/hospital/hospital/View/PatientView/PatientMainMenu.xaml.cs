using Controller;
using hospital.View.PatientView;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMainMenu.xaml
    /// </summary>
    public partial class PatientMainMenu : Page
    {
        private Frame Main;
        private UserController uc;
        public PatientMainMenu()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    Main = (window as PatientHomeWindow).Main;
                }
            }
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientMakeAppointmentFirst();
        }

        private void btnAllAppointments_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientAppointmentsPage();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Close();
                }
            }
        }

        private void btnDoctorGrading_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientHospitalPoll();
        }
    }
}
