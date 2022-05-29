using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace hospital.View.PatientView
{

    public class MedicalRecordViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string DateOfBirth { get; set; }
        public string Note { get; set; }
        public string Alergens { get; set; }
        public MyICommand ConfirmCommand { get; set; }

        public MedicalRecordViewModel()
        {
            FillFields();
            ConfirmCommand = new MyICommand(ChangeNote);
        }

        private void ChangeNote()
        {
            App app = Application.Current as App;
            MedicalRecordsController mrc = app.medicalRecordsController;
            UserController uc = app.userController;
            PatientController pc = app.patientController;
            Patient loggedInPatient = pc.FindById(uc.CurentLoggedUser.Username);
            MedicalRecord patientMedicalRecord = mrc.FindById(loggedInPatient.RecordId);
            patientMedicalRecord.Note = Note;

            mrc.UpdateById(patientMedicalRecord.RecordId, patientMedicalRecord);
        }
        private void FillFields()
        {
            App app = Application.Current as App;
            PatientController pc = app.patientController;
            UserController uc = app.userController;
            MedicalRecordsController mrc = app.medicalRecordsController;

            Patient loggedInPatient = pc.FindById(uc.CurentLoggedUser.Username);
            Name = loggedInPatient.FirstName + " " + loggedInPatient.LastName;
            Gender = loggedInPatient.Gender;
            DateOfBirth = loggedInPatient.DateOfBirth;

            MedicalRecord patientMedicalRecord = mrc.FindById(loggedInPatient.RecordId);
            BloodType = patientMedicalRecord.BloodType.ToString();
            Note = patientMedicalRecord.Note;
            Alergens = patientMedicalRecord.Alergies;
        }
    }
}
