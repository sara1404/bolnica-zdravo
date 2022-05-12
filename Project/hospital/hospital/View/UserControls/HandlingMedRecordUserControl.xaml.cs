using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for HandlingMedRecordUserControl.xaml
    /// </summary>
    public partial class HandlingMedRecordUserControl : UserControl
    {
        private MedicalRecordsController mc;
        private PatientController pc;
        private DoctorController dc;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public ObservableCollection<BloodType> BloodTypes { get; set; }
        public HandlingMedRecordUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            mc = app.mediicalRecordsController;
            pc= app.patientController;
            dc = app.doctorController;
            Patients =  pc.FindAll();
            Doctors = dc.GetDoctors();
            
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            Patient p = (Patient)dateGridHandlingMedicalRecord.SelectedItem;
            MedicalRecord med = mc.FindById(p.RecordId);
            medRecUserControl.txtFirstName.Text = p.FirstName;
            medRecUserControl.txtLastName.Text = p.LastName;
            medRecUserControl.txtPhone.Text = p.PhoneNumber;
            medRecUserControl.txtId.Text = p.Id;
            if (med.DoctorUsername != null)
                medRecUserControl.txtDoctor.Text = (dc.GetByUsername(med.DoctorUsername)).ToString();
            medRecUserControl.txtRecordId.Text = p.RecordId.ToString();
            medRecUserControl.txtDate.Text = p.DateOfBirth;
            if(med.BloodType != 0)
                medRecUserControl.txtBlood.Text = getBloodType(med.BloodType);
            if(med.Note != null)
                medRecUserControl.txtNote.Text = med.Note;

            if(med.Alergies != null)
            {
                String[] tmp=med.Alergies.Split(',');
                Allergens = new List<String>();
                foreach (String s in tmp)
                {
                    if(!s.Trim().Equals(""))
                        Allergens.Add(s.Trim());
                }
                medRecUserControl.listAllergens.ItemsSource=Allergens;
            }

            medRecUserControl.Visibility = Visibility.Visible;
        }
        public List<String> Allergens { get; set; }
        private string getBloodType(BloodType type)
        {
            switch (type)
            {
                case BloodType.abPositive:
                    return "AB+";
                    break;
                case BloodType.abNegative:
                    return "AB-";
                    break;
                case BloodType.aNegative:
                    return "A-";
                    break;
                case BloodType.aPositive:
                    return "A+";
                    break;
                case BloodType.bNegative:
                    return "B-" ;
                    break;
                case BloodType.bPositive:
                    return "B+" ;
                    break;
                case BloodType.oNegative:
                    return "0-" ;
                    break;
                case BloodType.oPositive:
                    return "0+";
                    break;
                case BloodType.hhNegative:
                    return "HH-" ;
                    break;
                default:
                    return "HH+";
                    break;
            }
        }

        private void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            addMedRecUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingMedicalRecord.SelectedIndex != -1)
            {
                Patient p = (Patient)dateGridHandlingMedicalRecord.SelectedItem;
                editMedRecUserControl.cmbUsername.Text = p.Username;
                MedicalRecord med = mc.FindById(p.RecordId);
                mc.RecordId=med.RecordId;
                if(med.DoctorUsername !=null)
                {
                    editMedRecUserControl.cmbDoctor.Text = dc.GetByUsername(med.DoctorUsername).ToString();
                }
                if(med.BloodType != 0)
                {
                    editMedRecUserControl.cmbBlood.Text = med.BloodType.ToString();
                }
                if (med.Alergies != null)
                {
                    editMedRecUserControl.txtAllergens.Text = med.Alergies;
                }
                if(med.Note != null)
                {
                    editMedRecUserControl.txtNote.Text = med.Note;
                }
                editMedRecUserControl.Visibility = Visibility.Visible;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            pc.DeleteById(((Patient)dateGridHandlingMedicalRecord.SelectedItem).Username);
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filteredList = Patients.Where(x => x.FirstName.ToLower().Contains(tbx.Text.ToLower()) || x.RecordId.ToString().ToLower().Contains(tbx.Text.ToLower()) || x.LastName.ToLower().Contains(tbx.Text.ToLower()) || x.DateOfBirth.ToLower().Contains(tbx.Text.ToLower()) || x.PhoneNumber.ToLower().Contains(tbx.Text.ToLower())).ToList();
                dateGridHandlingMedicalRecord.ItemsSource = null;
                dateGridHandlingMedicalRecord.ItemsSource = filteredList;
            }
            else
            {
                dateGridHandlingMedicalRecord.ItemsSource = Patients;
            }
        }
    }
}
