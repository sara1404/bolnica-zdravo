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
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (isCorrected())
                {
                    pc.Create(new Model.Patient(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFirstName.Text, txtSurname.Text, txtId.Text, txtPhone.Text));
                    this.Visibility = Visibility.Collapsed;
                }
            }catch (Exception ex)
            {
                if(ex.Message.Equals("Input first name")){
                    txtFirstName.BorderBrush=Brushes.Red;
                    errFirstname.Text=ex.Message;
                }
                else if(ex.Message.Equals("number in first name"))
                {
                    txtFirstName.BorderBrush = Brushes.Red;
                    errFirstname.Text = ex.Message;
                }

                if (ex.Message.Equals("Input surname"))
                {
                    txtSurname.BorderBrush = Brushes.Red;
                    errSurname.Text = ex.Message;
                }
                else if (ex.Message.Equals("number in surname"))
                {
                    txtSurname.BorderBrush = Brushes.Red;
                    errSurname.Text = ex.Message;
                }
            }


        }

        private bool isCorrected()
        {
            bool[] isCorrected= new bool[7];

            for (int i = 0; i < 7; i++)
            {
                isCorrected[i] = true;
            }

            if (txtFirstName.Text.Trim().Equals(""))
            {
                errFirstname.Text = "Input first name";
                txtFirstName.BorderBrush = Brushes.Red;
                isCorrected[0] = false;
            }
            else if (txtFirstName.Text.Any(char.IsDigit))
            {
                errFirstname.Text = "number isn't allowed";
                txtFirstName.BorderBrush = Brushes.Red;
                isCorrected[0] = false;
            }
            else
            {
                errFirstname.Text = "";
                txtFirstName.BorderBrush = Brushes.Gray;
                isCorrected[0] = true;
            }

            //Surname validation
            if (txtSurname.Text.Trim().Equals(""))
            {
                errSurname.Text = "Input surname";
                txtSurname.BorderBrush = Brushes.Red;
                isCorrected[1] = false;
            }
            else if (txtSurname.Text.Any(char.IsDigit))
            {
                errSurname.Text = "number isn't allowed";
                txtSurname.BorderBrush = Brushes.Red;
                isCorrected[1] = false;
            }
            else
            {
                errSurname.Text = "";
                txtSurname.BorderBrush = Brushes.Gray;
                isCorrected[1] = true;
            }
            //usernam validate
            if (txtUsername.Text.Trim().Equals(""))
            {
                errUsername.Text = "Input username";
                txtUsername.BorderBrush = Brushes.Red;
                isCorrected[2] = false;
            }
            else
            {
                errUsername.Text = "";
                txtUsername.BorderBrush = Brushes.Gray;
                isCorrected[2] = true;
            }
            //validate password
            if (txtPassword.Text.Trim().Equals(""))
            {
                errPassword.Text = "Input password";
                txtPassword.BorderBrush = Brushes.Red;
                isCorrected[3] = false;
            }
            else
            {
                errPassword.Text = "";
                txtPassword.BorderBrush = Brushes.Gray;
                isCorrected[3] = true;
            }

            //validate email
            if (txtEmail.Text.Trim().Equals(""))
            {
                errEmail.Text = "Input email";
                txtEmail.BorderBrush = Brushes.Red;
                isCorrected[4] = false;
            }
            else
            {
                errEmail.Text = "";
                txtEmail.BorderBrush = Brushes.Gray;
                isCorrected[4] = true;
            }

            //validate id
            if (txtId.Text.Trim().Equals(""))
            {
                errId.Text = "Input id";
                txtId.BorderBrush = Brushes.Red;
                isCorrected[5] = false;
            }
            else if (!txtId.Text.All(char.IsDigit))
            {
                errId.Text = "Only number allowed";
                txtId.BorderBrush = Brushes.Red;
                isCorrected[5] = false;
            }
            else
            {
                errId.Text = "";
                txtId.BorderBrush = Brushes.Gray;
                isCorrected[5] = true;
            }

            //validate phone
            if (txtPhone.Text.Trim().Equals(""))
            {
                errPhone.Text = "Input phone number";
                txtPhone.BorderBrush = Brushes.Red;
                isCorrected[6] = false;
            }
            else
            {
                errPhone.Text = "";
                txtPhone.BorderBrush = Brushes.Gray;
                isCorrected[6] = true;
            }


            return (isCorrected[0] && isCorrected[1] && isCorrected[2] && isCorrected[3] && isCorrected[4] && isCorrected[5] && isCorrected[6]);
        }


        
    }
}
