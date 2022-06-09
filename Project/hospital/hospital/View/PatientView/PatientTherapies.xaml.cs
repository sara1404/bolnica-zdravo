using Controller;
using hospital.DTO;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Tables;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    /// Interaction logic for PatientTherapies.xaml
    /// </summary>
    public partial class PatientTherapies : Page
    {
        private PatientController pc;
        private User currentUser;
        public PatientTherapies()
        {
            InitializeComponent();

            App app = Application.Current as App;
            pc = app.patientController;
            currentUser = app.userController.CurentLoggedUser;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach (TherapyDTO t in pc.FindCurrentMonthTherapies(currentUser.Username))
            {
                ScheduleAppointment sa = new ScheduleAppointment
                {
                    StartTime = t.Date,
                    EndTime = t.Date.AddMinutes(30),
                    IsAllDay = false,
                    Subject = t.Name,
                    AppointmentBackground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(6, 158, 47))
                };
                sac.Add(sa);
            }
            therapyCalendar.ItemsSource = sac;
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();

            PdfLightTable pdfLightTable = new PdfLightTable();

            DataTable table = new DataTable();

            table.Columns.Add("Monday");
            table.Columns.Add("Tuesday");
            table.Columns.Add("Wednesday");
            table.Columns.Add("Thursday");
            table.Columns.Add("Friday");
            table.Columns.Add("Saturday");
            table.Columns.Add("Sunday");

            List<string> row = new List<string>();
            for (int day = 1; day <= 31; day++)
            {
                row.Add(GetTherapyForDay(day));
                if (day == 31)
                {
                    row.Add("");
                    row.Add("");
                    row.Add("");
                    row.Add("");
                    table.Rows.Add(row.ToArray());
                }
                if (day % 7 == 0)
                {
                    table.Rows.Add(row.ToArray());
                    row.Clear();
                }
            }
            pdfLightTable.BeginRowLayout += new BeginRowLayoutEventHandler(table_StartRowLayout);
            pdfLightTable.DataSource = table;
            pdfLightTable.Draw(page, new PointF(0, 0));
            doc.Save("therapy_report.pdf");
            doc.Close(true);
            Dispatcher.Invoke(() =>
            {
                notifier.ShowInformation("A new therapy report has been made and is located in bin/Debug folder!");
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

        private string GetTherapyForDay(int day)
        {
            foreach (TherapyDTO therapy in pc.FindCurrentMonthTherapies(currentUser.Username))
            {
                if(therapy.Date.Day == day)
                {
                    return day + " " + therapy.Name;
                }
            }
            return day.ToString();
        }

        void table_StartRowLayout(object sender, BeginRowLayoutEventArgs args)
        {
            args.MinimalHeight = 40f;
        }
    }
}
