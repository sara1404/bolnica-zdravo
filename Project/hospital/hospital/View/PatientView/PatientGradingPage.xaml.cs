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
        public PatientGradingPage()
        {
            InitializeComponent();
            IEnumerable<int> grades = new List<int>() { 1, 2, 3, 4, 5 };
            cbGrade.ItemsSource = grades;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientMainMenu();
                }
            }
        }
    }
}
