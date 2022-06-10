﻿using Controller;
using hospital.Controller;
using hospital.Service;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMakeAppointmentFirst.xaml
    /// </summary>
    public partial class PatientMakeAppointmentFirst : Page
    {
        private AppointmentManagementController ac;
        private RecommendedAppointmentController rac;
        private AvailableAppointmentController aac;
        private UserController uc;
        private App app;
        public PatientMakeAppointmentFirst()
        {
            InitializeComponent();
            dateFrom.DisplayDateStart = DateTime.Today;
            dateTo.DisplayDateStart = DateTime.Today;
            app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            rac = app.recommendedAppointmentController;
            aac = app.availableAppointmentController;
            DoctorController dc = app.doctorController;
            cbDoctor.ItemsSource = dc.GetDoctors();
        }


        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            lbWarning.Content = "";
            bool priorityDoctor = (bool)rbDoctor.IsChecked;
            bool priorityDate = (bool)rbDate.IsChecked;
            Doctor doctor = (Doctor)cbDoctor.SelectedItem;


            if (dateFrom.SelectedDate != null && dateTo.SelectedDate != null && cbDoctor.SelectedItem != null && (priorityDoctor || priorityDate))
            {
                if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDoctor)
                {
                    appointmentTable.ItemsSource = rac.GetRecommendedByDoctor((DateTime)dateFrom.SelectedDate, (DateTime)dateTo.SelectedDate, doctor, uc.CurentLoggedUser.Username);
                }
                else if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) < 0 && priorityDate)
                {
                    appointmentTable.ItemsSource = rac.GetRecommendedByDate((DateTime)dateFrom.SelectedDate, (DateTime)dateTo.SelectedDate, doctor, uc.CurentLoggedUser.Username);
                }
                else if (dateFrom.SelectedDate.Value.CompareTo(dateTo.SelectedDate.Value) >= 0)
                {
                    lbWarning.Content = "Date from needs to be before date to!";
                }
                else
                {
                    lbWarning.Content = "Choose priority(and doctor)!";
                }
            }
            else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate == null && !priorityDoctor && !priorityDate)
            {
                Doctor d = (Doctor)cbDoctor.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDoctor(d.Username, uc.CurentLoggedUser.Username);

            }
            else if (cbDoctor.SelectedIndex == -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
            {
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDate((DateTime)dateFrom.SelectedDate, uc.CurentLoggedUser.Username);
            }
            else if (cbDoctor.SelectedIndex != -1 && dateFrom.SelectedDate != null && !priorityDoctor && !priorityDate)
            {
                Doctor d = (Doctor)cbDoctor.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)dateFrom.SelectedDate, d.Username, uc.CurentLoggedUser.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1)
            {
                ac.CreateAppointment((Appointment)appointmentTable.SelectedItem);
                appointmentTable.ItemsSource = null;
                Dispatcher.Invoke(() =>
                {
                    notifier.ShowInformation("New appointment created!");
                });
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PatientHomeWindow))
                    {
                        (window as PatientHomeWindow).Main.Content = new PatientCalendar();
                    }
                }
            }
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
