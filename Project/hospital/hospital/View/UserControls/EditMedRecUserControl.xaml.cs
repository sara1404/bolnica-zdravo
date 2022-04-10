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
            mc.UpdateById(mc.RecordId, new MedicalRecord(pc.FindById(cmbUsername.Text),mc.RecordId,txtAllergens.Text,dc.getByName(cmbDoctor.Text),getBloodType(cmbBlood.Text),txtNote.Text));
            this.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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
    }
}
