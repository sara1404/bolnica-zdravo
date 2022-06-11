﻿using System;
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
using System.Windows.Shapes;
using Controller;
using hospital.View.UserControls;

namespace hospital.View
{
    public partial class SecretaryHomeWindow : Window
    {
        public UserController uc;
       
        public SecretaryHomeWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            uc = app.userController;

            btnhandlingAccount.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnhandlingAccount.BorderThickness = new Thickness(3, 0, 0, 0);
            handlingAccountUserControl.Visibility = Visibility.Visible;
        }

        private void handlingAccount_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnhandlingAccount.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4")); 
            btnhandlingAccount.BorderThickness = new Thickness(3,0,0,0);
            handlingAccountUserControl.Visibility = Visibility.Visible;
        }

        private void btnHandMedRecord_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnhandlingMedRecord.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnhandlingMedRecord.BorderThickness = new Thickness(3, 0, 0, 0);
            handlingMedRecordUserControl.Visibility = Visibility.Visible;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            if (AlertPopup.IsOpen == false)
                AlertPopup.IsOpen = true;
            else
                AlertPopup.IsOpen = false;
            
        }

        public string LoggedName {
            get
            {
                return uc.CurentLoggedUser.Username;
            }
        }

        private void btnAppointment_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnAppointment.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnAppointment.BorderThickness = new Thickness(3, 0, 0, 0);
            handlingAppointmentUserControl.Visibility = Visibility.Visible;
        }

        private void btnEmergency_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnEmergency.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnEmergency.BorderThickness = new Thickness(3, 0, 0, 0);
            handlingEmergencyUserControl.Visibility = Visibility.Visible;
        }
        private void CloseAllUserControl()
        {
            //popUpUserControl.Visibility= Visibility.Collapsed;

            handlingMedRecordUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.addMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.editMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.medRecUserControl.Visibility = Visibility.Collapsed;

            handlingAccountUserControl.addAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.editAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.Visibility = Visibility.Collapsed;

            handlingAppointmentUserControl.Visibility = Visibility.Collapsed;
            handlingAppointmentUserControl.makeAppointmentUserControl.Visibility = Visibility.Collapsed;
            handlingAppointmentUserControl.editAppointmentUsercontrol.Visibility = Visibility.Collapsed;

            handlingEmergencyUserControl.Visibility = Visibility.Collapsed;
            handlingEmergencyUserControl.addGuestuserControl.Visibility = Visibility.Collapsed;
            handlingEmergencyUserControl.suggestedDelayUserControl.Visibility = Visibility.Collapsed;

            orderEquipmentUserControl.Visibility = Visibility.Collapsed;

            generateReportUsercontrol.Visibility = Visibility.Collapsed;

            revisionOfRestUserControl.Visibility = Visibility.Collapsed;

            scheduleMeetingsUserControl.Visibility = Visibility.Collapsed;
            
            btnhandlingAccount.BorderBrush = Brushes.Transparent;
            btnhandlingMedRecord.BorderBrush = Brushes.Transparent;
            btnAppointment.BorderBrush = Brushes.Transparent;
            btnEmergency.BorderBrush = Brushes.Transparent;
            btnOrder.BorderBrush = Brushes.Transparent;
            btnVacation.BorderBrush = Brushes.Transparent;
            btnMeetings.BorderBrush = Brushes.Transparent;
            btnPdf.BorderBrush = Brushes.Transparent;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnOrder.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnOrder.BorderThickness = new Thickness(3, 0, 0, 0);
            orderEquipmentUserControl.Visibility = Visibility.Visible;
        }

        private void btnVacation_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnVacation.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnVacation.BorderThickness = new Thickness(3, 0, 0, 0);
            revisionOfRestUserControl.Visibility = Visibility.Visible;
        }

        private void btnMeetings_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnMeetings.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnMeetings.BorderThickness = new Thickness(3, 0, 0, 0);
            scheduleMeetingsUserControl.Visibility = Visibility.Visible;
        }

        private void btnPdf_Click(object sender, RoutedEventArgs e)
        {
            CloseAllUserControl();
            btnPdf.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnPdf.BorderThickness = new Thickness(3, 0, 0, 0);
            generateReportUsercontrol.Visibility = Visibility.Visible;
        }
    }
}
