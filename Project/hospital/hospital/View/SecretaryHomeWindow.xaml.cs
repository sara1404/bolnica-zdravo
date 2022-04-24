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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for SecretaryHomeWindow.xaml
    /// </summary>
    public partial class SecretaryHomeWindow : Window
    {
        public SecretaryHomeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            handlingAccountUserControl.Visibility = Visibility.Visible;
        }

        private void btnHandMedRecord_Click(object sender, RoutedEventArgs e)
        {
            handlingAccountUserControl.addAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.Visibility = Visibility.Collapsed;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
