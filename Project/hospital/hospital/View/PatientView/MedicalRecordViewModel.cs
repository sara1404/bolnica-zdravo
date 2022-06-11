using Controller;
using Model;
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

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

            Application.Current.Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Medical record successfully changed!");
            });
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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
