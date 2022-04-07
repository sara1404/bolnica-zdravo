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
using hospital.View;

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

        }

        private void Doctor_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Secretary_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Manager_Button_Click(object sender, RoutedEventArgs e)
        {
            new PatientAppointmentsWindow().Show();
        }
    }
}
