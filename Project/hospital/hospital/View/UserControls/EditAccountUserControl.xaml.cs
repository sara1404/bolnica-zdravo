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
using Model;
namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for EditAccountUserControl.xaml
    /// </summary>
    public partial class EditAccountUserControl : UserControl
    {
        public PatientController pc;
        public EditAccountUserControl()
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
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            pc.UpdateById(pc.EditPatient.Username, new Model.Patient(txtUsername.Text, txtEmail.Text, pc.EditPatient.Password, txtFirstName.Text, txtSurname.Text, txtId.Text, txtPhone.Text));
            //pc.Create(new Model.Patient(txtUsername.Text, txtEmail.Text,"123", txtFirstName.Text, txtSurname.Text, txtId.Text, txtPhone.Text));
            this.Visibility = Visibility.Collapsed;
        }
    
}
}
