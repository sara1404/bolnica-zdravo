using Controller;
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

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientGradingPage.xaml
    /// </summary>
    public partial class PatientGradingPage : Page
    {
        private App app;
        public PatientGradingPage()
        {
            InitializeComponent();
            IEnumerable<int> grades = new List<int>() { 1, 2, 3, 4, 5 };
            cbGrade.ItemsSource = grades;
            app = Application.Current as App;
            DoctorController dc = app.doctorController;
            cbDoctor.ItemsSource = dc.GetDoctors();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(cbDoctor.SelectedItem == null)
            {
                lbWarning.Content = "Please choose doctor you wish to grade!";
            }
            else if(cbGrade.SelectedItem == null)
            {
                lbWarning.Content = "Please choose grade!";
            }
            else
            {
                app.PatientBackToMainMenu();
            }
        }
    }
}
