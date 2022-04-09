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
using System.Collections.ObjectModel;
using Model;
using Controller;
namespace hospital.View
{
    /// <summary>
    /// Interaction logic for HandlingAccountPage.xaml
    /// </summary>
    public partial class HandlingAccountPage : Page
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientController pc;
        public HandlingAccountPage()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            pc = app.patientController;
            Patients = pc.FindAll();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1){
                
                pc.DeleteById(dateGridHandlingAccount.SelectedItem.ToString());
            }
        }

        private void AddAccountUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            addUserControl.Visibility = Visibility;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1)
            {
                editUserControl.Visibility= Visibility;
                Patient p = (Patient)dateGridHandlingAccount.SelectedItem;
                editUserControl.txtFirstName.Text =p.FirstName;
                editUserControl.txtEmail.Text = p.Email;
                editUserControl.txtId.Text = p.Id;
                editUserControl.txtSurname.Text = p.LastName;
                editUserControl.txtUsername.Text = p.Username;
                editUserControl.txtPhone.Text = p.PhoneNumber;
                pc.EditPatient = (Patient)dateGridHandlingAccount.SelectedItem;
            }
        }
    }
}
