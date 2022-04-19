using hospital.View;
using System.Windows;
using Model;
namespace hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Patient_Button_Click(object sender, RoutedEventArgs e)
        {
            new PatientAppointmentsWindow().Show();
        }

        private void Doctor_Button_Click(object sender, RoutedEventArgs e)
        {
            new DoctorHomeWindow().Show();
        }

        private void Secretary_Button_Click(object sender, RoutedEventArgs e)
        {
            new SecretaryHomeWindow().Show();
            this.Close();
        }

        private void Manager_Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerMainWindow().Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Controller.UserController uc;
            App app = Application.Current as App;
            uc = app.userController;

            User u = uc.sendDate(txtUsername.Text, txtPassword.Password);

            if (u == null)
            {
                labIncorect.Text = "The username or password you've entered is incorrect.";
                txtPassword.Password = "";
            }
            else
            {
                labIncorect.Text = "";


                if (u.Role == Model.Role.Doctor)
                {
                    new DoctorHomeWindow().Show();
                    this.Close();
                }
                else if (u.Role == Model.Role.Secretary)
                {
                    new SecretaryHomeWindow().Show();
                    this.Close();
                }
                else if (u.Role == Model.Role.Manager)
                {
                    new ManagerMainWindow().Show();
                    this.Close();
                }
                else if (u.Role == Model.Role.Patient)
                {
                    new PatientAppointmentsWindow().Show();
                    this.Close();
                }
            }

        }
    }
}
