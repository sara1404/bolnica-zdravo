using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorMedicalRecordsWindow.xaml
    /// </summary>
    public partial class DoctorMedicalRecordsWindow : Window
    {
        private DoctorController dc;
        private MedicalRecordsController mrc;
        private UserController uc;
        private PatientController pc;
        private AppointmentController ac;

        private Doctor loggedInDoctor;
        private Patient selectedPatient;

        public ObservableCollection<Appointment> appointments { get; set; }

        public ObservableCollection<BloodType> BloodTypes;
        public DoctorMedicalRecordsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            dc = app.doctorController;
            mrc = app.mediicalRecordsController;
            uc = app.userController;
            pc = app.patientController;
            ac = app.appointmentController;
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            cmbPatients.ItemsSource = loggedInDoctor.myPatients;//samo pacijente tog doktora ce da da
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
            cmbBloodType.ItemsSource = BloodTypes;

        }

        private void cmbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPatient = pc.FindById((string)cmbPatients.SelectedItem);
            if( mrc.FindById(selectedPatient.RecordId) == null)
            {
                MedicalRecord medicalRecord = new MedicalRecord(selectedPatient.Username, "", loggedInDoctor.Username, BloodType.abPositive, "");
                mrc.Create(medicalRecord);
                selectedPatient.RecordId = medicalRecord.RecordId;
            }
            tbAlergies.Text = mrc.FindById(selectedPatient.RecordId).Alergies;
            tbNotes.Text = mrc.FindById(selectedPatient.RecordId).Note;
            cmbBloodType.SelectedItem = mrc.FindById(selectedPatient.RecordId).BloodType;
            appointments = ac.GetAppointmentByPatient(selectedPatient.Username);
            AppointmentTable.ItemsSource = appointments;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(cmbPatients.SelectedIndex != -1)
            {
                MedicalRecord newMedRec = new MedicalRecord(selectedPatient.Id, tbAlergies.Text, null, getBloodType(cmbBloodType.Text), tbNotes.Text);
                mrc.UpdateById(selectedPatient.RecordId, newMedRec);
                this.Close();
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

       
    }
}
