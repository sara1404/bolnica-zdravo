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
using Model;
using System.Collections.ObjectModel;
using Controller;
namespace hospital.View
{
    /// <summary>
    /// Interaction logic for HandlingMedicalRecordsPage.xaml
    /// </summary>
    public partial class HandlingMedicalRecordsPage : Page
    {
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }
        public MedicalRecordsController mc;
        public PatientController pc;
        public DoctorController dc;

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<BloodType> BloodTypes { get; set; }
        public HandlingMedicalRecordsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            mc = app.medicalRecordsController;
            MedicalRecords = mc.FindAll();
            pc = app.patientController;
            Patients = pc.FindAll();
            dc = app.doctorController;
            mc = app.medicalRecordsController;
            Doctors = dc.GetDoctors();
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           // addMedRecUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
          /*  if(dateGridHandlingMedRec.SelectedIndex != -1)
            {
                MedicalRecord md =(MedicalRecord) dateGridHandlingMedRec.SelectedItem;
                mc.RecordId = md.RecordId;
                editMEdRecUserControl.cmbUsername.Text = md.Username;
                editMEdRecUserControl.cmbDoctor.Text = md.NameDoctor;
                editMEdRecUserControl.cmbBlood.Text = getBloodType(md.BloodType);
                editMEdRecUserControl.txtAllergens.Text = md.Alergies;
                editMEdRecUserControl.txtNote.Text = md.Note;
                editMEdRecUserControl.Visibility = Visibility.Visible;
                dateGridHandlingMedRec.SelectedIndex= -1;

            }*/
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
          /*  if (dateGridHandlingMedRec.SelectedIndex != -1)
            {
                mc.DeleteById(((MedicalRecord)dateGridHandlingMedRec.SelectedItem).RecordId);
            }*/
        }

        private string getBloodType(BloodType type)
        {
            switch (type)
            {
                case BloodType.abPositive:
                    return "abPositive";
                    break;
                case BloodType.abNegative:
                    return "abNegative";
                    break;
                case BloodType.aNegative:
                    return "aNegative";
                    break;
                case BloodType.aPositive:
                    return "aPositive";
                    break;
                case BloodType.bNegative:
                    return "bNegative";
                    break;
                case BloodType.bPositive:
                    return "bPositive";
                    break;
                case BloodType.oNegative:
                    return "oNegative";
                    break;
                case BloodType.oPositive:
                    return "oPositive";
                    break;
                case BloodType.hhNegative:
                    return "hhNegative";
                    break;
                default:
                    return "hhPositive";
                    break;
            }
        }
    }
}
