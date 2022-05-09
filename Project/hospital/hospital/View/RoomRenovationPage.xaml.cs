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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for RoomRenovationPage.xaml
    /// </summary>
    public partial class RoomRenovationPage : Page
    {
        private UserController uc;
        public RoomRenovationPage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
        }

        private void Basic_Renovation_Click(object sender, RoutedEventArgs e)
        {
            new BasicRenovationWindow().Show();
        }

        private void Advanced_Renovation_Click(object sender, RoutedEventArgs e)
        {
            new AdvancedRenovationWindow().Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
