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
using Controller;
namespace hospital.View.UserControls
{
    public partial class PopUpUserControl : UserControl
    {
        UserController uc;
        public PopUpUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            mw.Show();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            ((SecretaryHomeWindow)parentWindow).CloseAllUserControl();
            ((SecretaryHomeWindow)parentWindow).changePasswordUserControl.Visibility = Visibility.Visible;
        }
    }
}
