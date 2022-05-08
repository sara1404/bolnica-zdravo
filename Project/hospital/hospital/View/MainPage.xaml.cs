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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame Main;
        private UserController uc;
        public MainPage()
        {
            InitializeComponent();
            foreach (Window win in Application.Current.Windows) {
                if (win is ManagerMainWindow) {
                    Main = ((ManagerMainWindow)win).Main;
                }
            }
            App app = Application.Current as App;
            uc = app.userController;
        }

        private void Room_Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerRoomsWindow().Show();
        }

        private void ManagerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new EquipmentPage();
        }

        private void Renovation_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RoomRenovationPage();
        }

   

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Medication_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePage();
        }
    }


}
