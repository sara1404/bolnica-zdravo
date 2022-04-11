using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
        }

        private void Room_Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerRoomsWindow().Show();
        }

        private void ManagerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
