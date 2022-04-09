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
    /// <summary>
    /// Interaction logic for AddAccountUserControl.xaml
    /// </summary>
    public partial class AddAccountUserControl : UserControl
    {
        public PatientController pc;
        public AddAccountUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pc = app.patientController;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Sstring text = txtField.Text;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            pc.Create(new Model.Patient(txtUsername.Text,txtPassword.Text,txtEmail.Text,txtFirstName.Text,txtSurname.Text,txtId.Text,txtPhone.Text));
            this.Visibility = Visibility.Collapsed;
        }
    }
}
