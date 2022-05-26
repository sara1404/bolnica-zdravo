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
using System.Windows.Shapes;
using Controller;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for SecretaryHomeWindow.xaml
    /// </summary>
    public partial class SecretaryHomeWindow : Window
    {
        public UserController uc;
       
        public SecretaryHomeWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            uc = app.userController;
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

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
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
            handlingMedRecordUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.addMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.editMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.medRecUserControl.Visibility = Visibility.Collapsed;

            handlingAccountUserControl.addAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.editAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.Visibility = Visibility.Collapsed;

            handlingAppointmentUserControl.Visibility = Visibility.Collapsed;
            handlingAppointmentUserControl.removeAppointmentUserControl.Visibility = Visibility.Collapsed;
            handlingAppointmentUserControl.delayAppointmentUserControl.Visibility = Visibility.Collapsed;

            handlingEmergencyUserControl.Visibility = Visibility.Collapsed;
            handlingEmergencyUserControl.addGuestuserControl.Visibility = Visibility.Collapsed;
            handlingEmergencyUserControl.suggestedDelayUserControl.Visibility = Visibility.Collapsed;

            orderEquipmentUserControl.Visibility = Visibility.Collapsed;

            revisionOfRestUserControl.Visibility = Visibility.Collapsed;

            btnhandlingAccount.BorderBrush = Brushes.Transparent;
            btnhandlingMedRecord.BorderBrush = Brushes.Transparent;
            btnAppointment.BorderBrush = Brushes.Transparent;
            btnEmergency.BorderBrush = Brushes.Transparent;
            btnOrder.BorderBrush = Brushes.Transparent;
            btnVacation.BorderBrush = Brushes.Transparent;
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
    }
}
