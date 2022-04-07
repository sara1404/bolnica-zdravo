using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerRoomsWindow.xaml
    /// </summary>
    public partial class ManagerRoomsWindow : Window
    {
        public ManagerRoomsWindow()
        {
            InitializeComponent();
        }

        private void Add_Room_Click(object sender, RoutedEventArgs e)
        {
            new AddRoomWindow().Show();
        }
    }
}
