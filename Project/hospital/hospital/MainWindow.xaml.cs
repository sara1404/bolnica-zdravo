using hospital.View;
using System.Windows;

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
    }
}
