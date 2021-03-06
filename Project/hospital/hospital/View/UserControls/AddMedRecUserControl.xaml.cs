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
    /// Interaction logic for AddMedRecUserControl.xaml
    /// </summary>
    public partial class AddMedRecUserControl : UserControl
    {
        
        public PatientController pc;
        public DoctorController dc;
        public MedicalRecordsController mc;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<BloodType> BloodTypes { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }
        public AddMedRecUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            pc = app.patientController;
            //ako ima vec karton ne moze da pravi jos jedan
            ObservableCollection<Patient>  p = pc.FindAll();
            Patients = new ObservableCollection<Patient>();
            foreach (Patient pat in p)
            {
                if (pat.RecordId == 0)
                {
                    Patients.Add(pat);
                }
            }
            if (Patients.Count == 0)
            {
                errUsername.Text = "All patients have record";
            }
            dc = app.doctorController;
            mc = app.medicalRecordsController;
            Doctors = dc.GetDoctors();
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isCorected()) { 
                mc.Create(new MedicalRecord(cmbUsername.Text, txtAllergens.Text, cmbDoctor.Text, getBloodType(cmbBlood.Text), txtNote.Text));
                this.Visibility=Visibility.Collapsed;
            }
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
