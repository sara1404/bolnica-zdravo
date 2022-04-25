﻿using Controller;
using Model;
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

        private Doctor loggedInDoctor;
        private Patient selectedPatient;

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
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            cmbPatients.ItemsSource = loggedInDoctor.myPatients;//samo pacijente tog doktora ce da da
            BloodTypes = new ObservableCollection<BloodType>(Enum.GetValues(typeof(BloodType)).Cast<BloodType>().ToList());
            cmbBloodType.ItemsSource = BloodTypes;

        }

        private void cmbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPatient = pc.FindById((string)cmbPatients.SelectedItem);
            if(selectedPatient.MedicalRecord == null)
            {
                MedicalRecord medicalRecord = new MedicalRecord(selectedPatient, "", loggedInDoctor, BloodType.abPositive, "");
                mrc.Create(medicalRecord);
                selectedPatient.MedicalRecord = medicalRecord;
            }
            tbAlergies.Text = selectedPatient.MedicalRecord.Alergies;
            tbNotes.Text = selectedPatient.MedicalRecord.Note;
            cmbBloodType.SelectedItem = selectedPatient.MedicalRecord.BloodType;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(cmbPatients.SelectedIndex != -1)
            {
                //medical record controller nije najsrecnije napisan pa stoji ovako za sad
                selectedPatient.MedicalRecord.Alergies = tbAlergies.Text;
                selectedPatient.MedicalRecord.Note = tbNotes.Text;
                selectedPatient.MedicalRecord.BloodType = getBloodType(cmbBloodType.Text);
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