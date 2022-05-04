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
using Model;
using Controller;
using System.Collections.ObjectModel;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for RemoveAppointementUserControl.xaml
    /// </summary>
    public partial class RemoveAppointementUserControl : UserControl
    {
        public AppointmentController ac;
        public RemoveAppointementUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ac = app.appointmentController;
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            errUsername.Text = "";
            errTime.Text = "";
            errDate.Text =  "";
            notFree.Text = "";
        }

        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source == txtTime)
                errTime.Text = "";
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == date)
                if (date.Text.Equals(""))
                    errDate.Text = "Choose one date";
                else
                    errDate.Text = "";
        }

        private void cmbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errUsername.Text = "";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                ObservableCollection<Appointment> apps = ac.GetAppointmentByPatient(cmbUsername.Text);
                foreach (Appointment appointment in apps.ToList())
                {
                    string dates = appointment.StartTime.ToString().Split(' ')[0];
                    string hours = appointment.StartTime.ToString().Split(' ')[1].Split(':')[0];
                    string minuts = appointment.StartTime.ToString().Split(' ')[1].Split(':')[1];
                    if (dates.Equals(date.Text.Split(' ')[0]) && hours.Equals(txtTime.Text.Split(':')[0]) && minuts.Equals(txtTime.Text.Split(':')[1]))
                    {
                        ac.DeleteAppointment(appointment.Id);
                        notifier.ShowSuccess("Appointment successfully removed.");
                        this.Visibility = Visibility.Collapsed;
                    }
                }

            }
            else
            {
                notFree.Text = "Appointment not exists";
            }
        }

        private void btnMake_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Text = "";
            errUsername.Text = "";
            errTime.Text = "";
            errDate.Text =  "";
            notFree.Text = "";
        }

        private bool isValidate()
        {
            bool[] isCorrected = new bool[3];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }
            //username
            if (cmbUsername.Text.Equals(""))
            {
                errUsername.Text = "Choose one option";
                isCorrected[0] = false;
            }
            else
            {
                errUsername.Text = "";
                isCorrected[0] = true;
            }
            //date
            if (date.Text.Equals(""))
            {
                errDate.Text = "Choose one date";
                isCorrected[1] = false;
            }
            else
            {
                errDate.Text = "";
                isCorrected[1] = true;
            }
            //time
            if (txtTime.Text.Equals(""))
            {
                errTime.Text = "Must be filled";
                isCorrected[2] = false;
            }
            else
            {
                errTime.Text = "";
                isCorrected[2] = true;
            }

            return (isCorrected[0] && isCorrected[1] && isCorrected[2]);
        }
    }
}
