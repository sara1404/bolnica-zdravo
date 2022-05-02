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

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for MedicalRecordUserControl.xaml
    /// </summary>
    public partial class MedicalRecordUserControl : UserControl
    {
        public MedicalRecordUserControl()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtId.Clear();
            txtDoctor.Clear();
            txtRecordId.Clear();
            txtDate.Clear();
            txtBlood.Clear();
            txtNote.Clear();
            listAllergens.ItemsSource = new List<string>();

        }
    }
}
