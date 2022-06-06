using Controller;
using Model;
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

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecord.xaml
    /// </summary>
    public partial class PatientMedicalRecord : Page
    {
        private App app;
        private PatientController pc;
        private UserController uc;
        private MedicalRecordsController mrc;
        
        public PatientMedicalRecord()
        {
            InitializeComponent();
            DataContext = new MedicalRecordViewModel();
            /*app = Application.Current as App;
            pc = app.patientController;
            uc = app.userController;
            mrc = app.medicalRecordsController;

            Patient currentPatient = pc.FindById(uc.CurentLoggedUser.Username);
            tbName.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            tbGender.Text = currentPatient.Gender;
            tbDateOfBirth.Text = currentPatient.DateOfBirth.ToString();
            tbAlergens.Text = mrc.FindById(currentPatient.RecordId).Alergies;
            tbNote.Text = mrc.FindById(currentPatient.RecordId).Note;*/
        }

    }
}
