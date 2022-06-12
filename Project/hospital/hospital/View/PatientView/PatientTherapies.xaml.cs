using Controller;
using hospital.DTO;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            //Create a header and draw the image.

            RectangleF bounds = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 80);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);
 
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
            PdfFont fontH1 = new PdfStandardFont(PdfFontFamily.Helvetica, 15);
            PdfBrush brush = new PdfSolidBrush(System.Drawing.Color.Black);

            Patient patient = pc.FindById(currentUser.Username);
            string fullName = patient.FirstName.Trim() + " " + patient.LastName.Trim();

            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Zdravo d.o.o. ambulanta Novi Sad");
            PdfCompositeField compositeField1 = new PdfCompositeField(font, brush, "Tel/fax +381 234567");
            PdfCompositeField compositeField2 = new PdfCompositeField(font, brush, "Pavla Pavlovića 7, Novi Sad 21000");
            PdfCompositeField compositeField3 = new PdfCompositeField(fontH1, brush, "Lekovi za pacijenta: " + fullName);

            compositeField.Bounds = header.Bounds;
            compositeField.Draw(header.Graphics, new PointF(0, 0));
            compositeField1.Draw(header.Graphics, new PointF(0, 10));
            compositeField2.Draw(header.Graphics, new PointF(0, 20));
            compositeField3.Draw(header.Graphics, new PointF(0, 50));

            //Add the header at the top.

            doc.Template.Top = header;

            PdfLightTable pdfLightTable = new PdfLightTable();

            DataTable table = new DataTable();

            table.Columns.Add("Wednesday");
            table.Columns.Add("Thursday");
            table.Columns.Add("Friday");
            table.Columns.Add("Saturday");
            table.Columns.Add("Sunday");
            table.Columns.Add("Monday");
            table.Columns.Add("Tuesday");

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
            pdfLightTable.Style.ShowHeader = true;
            pdfLightTable.BeginRowLayout += new BeginRowLayoutEventHandler(table_StartRowLayout);
            pdfLightTable.DataSource = table;
            pdfLightTable.Draw(page, new PointF(0, 40));
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
            args.CellStyle.BackgroundBrush = new PdfSolidBrush(System.Drawing.Color.LightBlue);
        }
    }
}
