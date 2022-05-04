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
            btnhandlingMedRecord.BorderBrush = Brushes.Transparent;
            btnAppointment.BorderBrush = Brushes.Transparent;
            btnhandlingAccount.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4")); 
            btnhandlingAccount.BorderThickness = new Thickness(3,0,0,0);

            handlingMedRecordUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.addMedRecUserControl.Visibility=Visibility.Collapsed;
            handlingMedRecordUserControl.editMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.medRecUserControl.Visibility = Visibility.Collapsed;

            handlingAppointmentUserControl.Visibility = Visibility.Collapsed;

            handlingAccountUserControl.Visibility = Visibility.Visible;
        }

        private void btnHandMedRecord_Click(object sender, RoutedEventArgs e)
        {
            btnhandlingMedRecord.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnhandlingAccount.BorderBrush = Brushes.Transparent;
            btnAppointment.BorderBrush = Brushes.Transparent;
            btnhandlingMedRecord.BorderThickness = new Thickness(3, 0, 0, 0);

            handlingAccountUserControl.addAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.editAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.Visibility = Visibility.Collapsed;

            handlingAppointmentUserControl.Visibility = Visibility.Collapsed;

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
            //clossing handling account user control
            handlingAccountUserControl.addAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.editAccountUserControl.Visibility = Visibility.Collapsed;
            handlingAccountUserControl.Visibility = Visibility.Collapsed;
            //closing med. records user control
            handlingMedRecordUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.addMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.editMedRecUserControl.Visibility = Visibility.Collapsed;
            handlingMedRecordUserControl.medRecUserControl.Visibility = Visibility.Collapsed;

            btnAppointment.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#c8d8e4"));
            btnhandlingAccount.BorderBrush = Brushes.Transparent;
            btnhandlingMedRecord.BorderBrush = Brushes.Transparent;
            btnAppointment.BorderThickness = new Thickness(3, 0, 0, 0);

            handlingAppointmentUserControl.Visibility = Visibility.Visible;
        }
    }
}
