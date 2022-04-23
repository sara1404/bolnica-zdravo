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
    /// Interaction logic for RoomRenovationPage.xaml
    /// </summary>
    public partial class RoomRenovationPage : Page
    {
        public RoomRenovationPage()
        {
            InitializeComponent();
        }

        private void Basic_Renovation_Click(object sender, RoutedEventArgs e)
        {
            new BasicRenovationWindow().Show();
        }

        private void Advanced_Renovation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
