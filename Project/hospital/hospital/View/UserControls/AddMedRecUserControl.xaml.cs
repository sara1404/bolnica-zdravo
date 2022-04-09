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
            Patients = pc.FindAll();
            dc = app.doctorController;
            mc = app.mediicalRecordsController;
            Doctors = dc.GetDoctors();
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            mc.Create(new MedicalRecord(pc.FindById(cmbUsername.Text), "1241421", txtAllergens.Text, dc.getByName(cmbDoctor.Text), getBloodType(cmbBlood.Text), txtNote.Text));
            this.Visibility=Visibility.Collapsed;
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
