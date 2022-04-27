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
    /// Interaction logic for EditMedRecUserControl.xaml
    /// </summary>
    public partial class EditMedRecUserControl : UserControl
    {
        public MedicalRecordsController mc;
        public PatientController pc;
        public DoctorController dc;
        public EditMedRecUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            mc = app.mediicalRecordsController;
            pc = app.patientController;
            dc = app.doctorController;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isCorected())
            {
                String doctorUsername = dc.getByName(cmbDoctor.Text).Username;
                mc.UpdateById(mc.RecordId, new MedicalRecord(cmbUsername.Text, txtAllergens.Text, doctorUsername, getBloodType(cmbBlood.Text), txtNote.Text));
                this.Visibility = Visibility.Collapsed;
                cmbUsername.Text = "";
                cmbDoctor.Text = "";
                cmbBlood.Text = "";
                txtAllergens.Text = "";
                txtNote.Text = "";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cmbUsername.Text = "";
            cmbDoctor.Text = "";
            cmbBlood.Text = "";
            txtAllergens.Text = "";
            txtNote.Text = "";
            this.Visibility = Visibility.Collapsed;
        }

        private BloodType getBloodType(string txt)
        {
            switch (txt)
            {
                case "abPositive":
                    return BloodType.abPositive;
                    break;
                case "abNegative":
                    return BloodType.abNegative;
                    break;
                case "aNegative":
                    return BloodType.aNegative;
                    break;
                case "aPositive":
                    return BloodType.aPositive;
                    break;
                case "bNegative":
                    return BloodType.bNegative;
                    break;
                case "bPositive":
                    return BloodType.bPositive;
                    break;
                case "oNegative":
                    return BloodType.oNegative;
                    break;
                case "oPositive":
                    return BloodType.oPositive;
                    break;
                case "hhNegative":
                    return BloodType.hhNegative;
                    break;
                default:
                    return BloodType.hhPositive;
                    break;
            }
        }
        private bool isCorected()
        {
            bool[] isCorrected = new bool[3];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }
            //username
            if (cmbUsername.Text.Equals(""))
            {
                errUsername.Text = "Choose one option";
                isCorrected[0] = false;
            }
            else
            {
                errUsername.Text = "";
                isCorrected[0] = true;
            }
            //doctor
            if (cmbDoctor.Text.Equals(""))
            {
                errDoctor.Text = "Choose one option";
                isCorrected[1] = false;
            }
            else
            {
                errDoctor.Text = "";
                isCorrected[1] = true;
            }
            //blood
            if (cmbBlood.Text.Equals(""))
            {
                errBlood.Text = "Choose one option";
                isCorrected[2] = false;
            }
            else
            {
                errBlood.Text = "";
                isCorrected[2] = true;
            }


            return (isCorrected[0] && isCorrected[1] && isCorrected[2]);
        }
    }
}
