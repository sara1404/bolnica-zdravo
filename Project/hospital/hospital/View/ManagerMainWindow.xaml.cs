using Controller;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        private UserController uc;
        public ManagerMainWindow()
        {
            InitializeComponent();
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

        private void Main_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

    }
}
