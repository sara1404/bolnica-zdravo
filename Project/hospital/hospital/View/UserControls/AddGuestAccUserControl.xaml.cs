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
    /// Interaction logic for AddGuestAccUserControl.xaml
    /// </summary>
    public partial class AddGuestAccUserControl : UserControl
    {
        public PatientController pc;
        public AddGuestAccUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pc = app.patientController;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValidate())
                {
                    pc.Create(new Patient(txtFirstName.Text, txtSurname.Text, txtUsername.Text));
                    this.Visibility = Visibility.Hidden;
                }
            }catch(Exception ex)
            {
                if(ex.Message.Equals("Username already exists !"))
                {
                txtUsername.BorderBrush = Brushes.Red;
                errUsername.Text = ex.Message;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool isValidate()
        {
            bool[] isCorrected = new bool[3];

            for (int i = 0; i < 2; i++)
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
            return (isCorrected[0] && isCorrected[1] && isCorrected[2]);
        }
    }
}
