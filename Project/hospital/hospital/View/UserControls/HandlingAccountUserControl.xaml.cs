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
using System.Collections.ObjectModel;
using Model;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for HandlingAccountUserControl.xaml
    /// </summary>
    public partial class HandlingAccountUserControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientController pc;
        public HandlingAccountUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            pc = app.patientController;
            Patients = pc.FindAll();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1)
            {

                pc.DeleteById(dateGridHandlingAccount.SelectedItem.ToString());
            }
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            //reset fields
            addAccountUserControl.txtFirstName.Clear();
            addAccountUserControl.txtLastName.Clear();
            addAccountUserControl.txtPhone.Clear();
            addAccountUserControl.txtId.Clear();
            addAccountUserControl.txtEmail.Clear();
            addAccountUserControl.radioFemale.IsChecked=false;
            addAccountUserControl.radioMale.IsChecked = false;
            addAccountUserControl.txtDate.Text="";
            addAccountUserControl.txtUsername.Clear();
            addAccountUserControl.txtPassword.Clear();
            
            //reset error labels
            addAccountUserControl.errFristname.Text = "";
            addAccountUserControl.errLastname.Text = "";
            addAccountUserControl.errPhone.Text = "";
            addAccountUserControl.errId.Text = "";
            addAccountUserControl.errEmail.Text = "";
            addAccountUserControl.errUsername.Text = "";
            addAccountUserControl.errPassword.Text = "";

            addAccountUserControl.resetCorected();
            addAccountUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1)
            {
                //DODAJ JOS PODATAKA
                Patient p = (Patient)dateGridHandlingAccount.SelectedItem;
                pc.EditPatient = p;
                dateGridHandlingAccount.SelectedIndex = -1;
                editAccountUserControl.txtFirstName.Text = p.FirstName;
                editAccountUserControl.txtLastName.Text = p.LastName;
                editAccountUserControl.txtPhone.Text = p.PhoneNumber;
                editAccountUserControl.txtId.Text = p.Id;
                editAccountUserControl.txtEmail.Text = p.Email;
                editAccountUserControl.txtUsername.Text = p.Username;
                editAccountUserControl.Visibility=Visibility.Visible;
            }
        }
    }
}
