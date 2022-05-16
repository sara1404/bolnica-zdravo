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
    public partial class HandlingAccountUserControl : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientController pc;
        public UserController uc;
        public HandlingAccountUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            pc = app.patientController;
            uc = app.userController;
            Patients = pc.FindAll();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1)
            {
                Patient p = (Patient)dateGridHandlingAccount.SelectedItem;
                pc.DeleteById(p.Username);
                uc.DeleteByUsername(p.Username);
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
            addAccountUserControl.radioMale.IsChecked = true;
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

                if (p.Gender.Equals("Male")) {
                    editAccountUserControl.radioMale.IsChecked = true;
                    editAccountUserControl.radioFemale.IsChecked = false;
                }
                else
                {
                    editAccountUserControl.radioMale.IsChecked = false;
                    editAccountUserControl.radioFemale.IsChecked = true;
                }
                if (p.Role == Model.Role.Patient)
                    editAccountUserControl.cmbRole.SelectedIndex = 0;
                else if (p.Role == Model.Role.Doctor)
                    editAccountUserControl.cmbRole.SelectedIndex = 1;
                else if (p.Role == Model.Role.Secretary)
                    editAccountUserControl.cmbRole.SelectedIndex = 2;
                else if (p.Role == Model.Role.Manager)
                    editAccountUserControl.cmbRole.SelectedIndex = 3;

                editAccountUserControl.txtDate.Text = p.DateOfBirth;
                if (p.IsBlocked)
                {
                    editAccountUserControl.cbBlocked.IsChecked = true;
                }
                else
                {
                    editAccountUserControl.cbBlocked.IsChecked = false;
                }
                editAccountUserControl.Visibility=Visibility.Visible;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filteredList = Patients.Where(x => x.FirstName.ToLower().Contains(tbx.Text.ToLower()) || x.Username.ToLower().Contains(tbx.Text.ToLower()) || x.LastName.ToLower().Contains(tbx.Text.ToLower()) || x.Email.ToLower().Contains(tbx.Text.ToLower()) || x.PhoneNumber.ToLower().Contains(tbx.Text.ToLower())).ToList();
                dateGridHandlingAccount.ItemsSource = null;
                dateGridHandlingAccount.ItemsSource = filteredList;
            }
            else
            {
                dateGridHandlingAccount.ItemsSource = Patients;
            }
        }
    }
}
