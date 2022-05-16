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
            Main.Content = new MainPage();
        }

      
    }
}
