using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.PatientView
{
    public class AppointmentNoteViewModel
    {
        public string DoctorNote { get; set;}
        public string PatientNote { get; set; }
        public MyICommand ConfirmCommand { get; set; }
        private AppointmentManagementController ac;
        private Appointment oldAppointment;

        public AppointmentNoteViewModel(Appointment appointment)
        {
            App app = Application.Current as App;
            ac = app.appointmentController;
            ConfirmCommand = new MyICommand(Confirm);
            DoctorNote = appointment.DoctorNote;
            PatientNote = appointment.PatientNote;
            oldAppointment = appointment;
        }

        private void Confirm()
        {
            Appointment newAppointment = oldAppointment;
            newAppointment.PatientNote = PatientNote;
            ac.UpdateAppointment(oldAppointment, newAppointment);
            GoToAppointmentsPage();
        }
        private void GoToAppointmentsPage()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    (window as PatientHomeWindow).Main.Content = new PatientAppointmentsPage();
                    (window as PatientHomeWindow).lbPageName.Content = "All appointments";
                }
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("Patient note succesfully changed!");
            });
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
