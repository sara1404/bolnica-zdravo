using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PatientCustomNotification.xaml
    /// </summary>
    public partial class PatientCustomNotification : Page
    {
        private App app;
        private UserController uc;
        private NotificationController nc;
        public PatientCustomNotification()
        {
            InitializeComponent();
            app = Application.Current as App;
            uc = app.userController;
            nc = app.notificationController;
            dateStart.DisplayDateStart = DateTime.Now;
            dateEnd.DisplayDateStart = DateTime.Now;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private bool Validate()
        {
            if (tbText.Text.Equals(""))
            {
                lblWarning.Content = "Please enter desired notification text!";
                return false;
            }
            else if ((bool)rbOneTime.IsChecked)
            {
                if(dateStart.SelectedDate == null)
                {
                    lblWarning.Content = "Please select date!";
                    return false;
                } else if (timeEnd.Value == null)
                {
                    lblWarning.Content = "Please enter time!";
                    return false;
                }
                return true;
            }
            else
            {
                int interval;
                try
                {
                    interval = Int32.Parse(tbInterval.Text);
                }
                catch
                {
                    lblWarning.Content = "Interval has to be a positive integer!";
                    return false;
                }
                if (dateStart.SelectedDate == null || dateEnd.SelectedDate == null)
                {
                    lblWarning.Content = "Please select date!";
                    return false;
                }
                else if (timeEnd.Value == null || timeStart.Value == null)
                {
                    lblWarning.Content = "Please enter time!";
                    return false;
                }
                else if (dateEnd.SelectedDate <= dateStart.SelectedDate)
                {
                    lblWarning.Content = "Date end is before date start!";
                    return false;
                }
                else if (tbInterval.Text.Equals(""))
                {
                    lblWarning.Content = "Enter repetition interval!";
                    return false;
                }
                else if(interval <= 0)
                {
                    lblWarning.Content = "Interval has to be a positive integer!";
                    return false;
                }
                return true;
            }
            
        }

        private void rbOneTime_Checked(object sender, RoutedEventArgs e)
        {
            lblEndsAt.Visibility = Visibility.Hidden;
            lblHours.Visibility = Visibility.Hidden;
            lblRepeats.Visibility = Visibility.Hidden;
            lblTimeEnd.Visibility = Visibility.Hidden;
            dateEnd.Visibility = Visibility.Hidden;
            timeEnd.Visibility = Visibility.Hidden;
            tbInterval.Visibility = Visibility.Hidden;
        }

        private void rbPeriodically_Checked(object sender, RoutedEventArgs e)
        {
            lblEndsAt.Visibility = Visibility.Visible;
            lblHours.Visibility = Visibility.Visible;
            lblRepeats.Visibility = Visibility.Visible;
            lblTimeEnd.Visibility = Visibility.Visible;
            dateEnd.Visibility = Visibility.Visible;
            timeEnd.Visibility = Visibility.Visible;
            tbInterval.Visibility = Visibility.Visible;
        }

        private void SendNotificationToHomeWindow(Notification notification)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).StartNotification(notification);
                }
            }
        }

        private DateTime JoinDateAndTimeStart()
        {
            if (dateStart.SelectedDate != null && timeStart.Value != null)
            {
                return new DateTime(dateStart.SelectedDate.Value.Year,
                                                           dateStart.SelectedDate.Value.Month,
                                                               dateStart.SelectedDate.Value.Day,
                                                                   timeStart.Value.Value.Hour,
                                                                       timeStart.Value.Value.Minute, 0);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        private DateTime JoinDateAndTimeEnd()
        {
            if (dateEnd.SelectedDate != null && timeEnd.Value != null)
            {
                return new DateTime(dateEnd.SelectedDate.Value.Year,
                                                       dateEnd.SelectedDate.Value.Month,
                                                           dateEnd.SelectedDate.Value.Day,
                                                               timeEnd.Value.Value.Hour,
                                                                   timeEnd.Value.Value.Minute, 0);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() && (bool)rbOneTime.IsChecked)
            {
                Notification n = new Notification(uc.CurentLoggedUser.Username, JoinDateAndTimeStart(), tbText.Text);
                nc.Create(n);
                SendNotificationToHomeWindow(n);
            }
            else
            {
                Notification n = new Notification(uc.CurentLoggedUser.Username, JoinDateAndTimeStart(), JoinDateAndTimeEnd(), Int32.Parse(tbInterval.Text), tbText.Text);
                nc.Create(n);
                SendNotificationToHomeWindow(n);
            }
        }
    }
}
