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
    /// Interaction logic for EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        public EquipmentPage()
        {
            InitializeComponent();
        }

        private void Show_Relocation_Window_Click(object sender, RoutedEventArgs e)
        {
            new EquipmentRelocationWindow().Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
