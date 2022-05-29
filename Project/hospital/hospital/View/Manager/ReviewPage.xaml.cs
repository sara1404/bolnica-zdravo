using hospital.VM;
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

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : Page
    {
        public ReviewPage()
        {
            this.DataContext = new ReviewPageViewModel();
            InitializeComponent();
        }

        //private void Doctor_Review_Click(object sender, RoutedEventArgs e)
        //{
        //    new DoctorReviewWindow().Show();
        //}

        //private void Hospital_Review_Click(object sender, RoutedEventArgs e)
        //{
        //    new HospitalReviewWindow().Show();
        //}

        //private void Logout_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
