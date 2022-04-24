﻿using System;
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
        public UserController uc;
        public HandlingAccountPage()
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
            if (dateGridHandlingAccount.SelectedIndex != -1){
                Patient p = (Patient)dateGridHandlingAccount.SelectedItem;
                pc.DeleteById(p.Username);
                uc.DeleteByUsername(p.Username);
            }
        }

        private void AddAccountUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            /*resetUserControl();
            addUserControl.txtEmail.Text = "";
            addUserControl.txtFirstName.Text = "";
            addUserControl.txtId.Text = "";
            addUserControl.txtPassword.Text = "";
            addUserControl.txtSurname.Text = "";
            addUserControl.txtUsername.Text = "";
            addUserControl.txtPhone.Text = "";
            addUserControl.datePicker.Text = "";
            addUserControl.Visibility = Visibility;
            */
            HandlingAccount.Content = new AddUserAccountPage();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            /*if (dateGridHandlingAccount.SelectedIndex != -1)
            {
                editUserControl.Visibility= Visibility;
                Patient p = (Patient)dateGridHandlingAccount.SelectedItem;
                editUserControl.txtFirstName.Text =p.FirstName;
                editUserControl.txtEmail.Text = p.Email;
                editUserControl.txtId.Text = p.Id;
                editUserControl.txtSurname.Text = p.LastName;
                editUserControl.txtUsername.Text = p.Username;
                editUserControl.txtPhone.Text = p.PhoneNumber;
                editUserControl.datePicker.Text = p.DateOfBirth;
                pc.EditPatient = (Patient)dateGridHandlingAccount.SelectedItem;
                dateGridHandlingAccount.SelectedIndex = -1;
            }*/
            pop1.IsOpen = true;
        }

        private void resetUserControl()
        {
           /* Console.WriteLine("JEbem ti sve");
            addUserControl.txtEmail.BorderBrush = Brushes.Gray;
            addUserControl.txtFirstName.BorderBrush = Brushes.Gray;
            addUserControl.txtId.BorderBrush = Brushes.Gray;
            addUserControl.txtPassword.BorderBrush = Brushes.Gray;
            addUserControl.txtSurname.BorderBrush = Brushes.Gray;
            addUserControl.txtUsername.BorderBrush = Brushes.Gray;
            addUserControl.txtPhone.BorderBrush = Brushes.Gray;

            addUserControl.errEmail.Text = "";
            addUserControl.errFirstname.Text = "";
            addUserControl.errId.Text = "";
            addUserControl.errPassword.Text = "";
            addUserControl.errSurname.Text = "";
            addUserControl.errUsername.Text = "";
            addUserControl.errPhone.Text = ""; */
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            /* addGuestUserControl.txtFirstName.Text = "";
            addGuestUserControl.txtSurname.Text = "";
            addGuestUserControl.txtUsername.Text = "";

            addGuestUserControl.txtFirstName.BorderBrush = Brushes.Gray;
            addGuestUserControl.txtSurname.BorderBrush = Brushes.Gray;
            addGuestUserControl.txtUsername.BorderBrush = Brushes.Gray;

            addGuestUserControl.errFirstname.Text = "";
            addGuestUserControl.errSurname.Text = "";
            addGuestUserControl.errUsername.Text = "";
            addGuestUserControl.Visibility = Visibility.Visible; */
        }

    }       
}           
