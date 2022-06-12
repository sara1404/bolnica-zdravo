using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using hospital;
using Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace ViewModel
{
    public class GenerateReportViewModel : INotifyPropertyChanged
    {
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string _note;
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private AppointmentManagementController _appointmentManagementController;
        private PatientController _patientController;
        private DoctorController _doctorController;
        private Notifier Notifier { get; set; }
        public DateTime DisplayDateEnd {get;set;}
        public DateTime DispleyDateStart { get; set; }
        public GenerateReportViewModel()
        {
            InstantiateControllers();
            InsatiateData();
        }
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentManagementController = app.appointmentController;
            _patientController = app.patientController;
            _doctorController = app.doctorController;
        }

        private void InsatiateData()
        {
            Notifier = GetNotifier();
            DateFrom = DateTime.Now.AddMonths(-1);
            DispleyDateStart = DateFrom;
            DateTo = DateTime.Now.AddMonths(1);
            DisplayDateEnd = DateTo;
            SubmitCommand = new RelayCommand(param => ExecuteSubmitCommand(), param => ValidationInput());
            CancelCommand = new RelayCommand(param => ExecuteCancelCommand(), param => true);
        }
        private void ExecuteCancelCommand()
        {
            Note = "";
        }
        private bool ValidationInput()
        {
            if (DateFrom != null && DateTo != null)
                return true;
            return false;
        }
        private void ExecuteSubmitCommand()
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            DataTable dataTable = new DataTable();
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            PdfFont fontSmall = new PdfStandardFont(PdfFontFamily.Helvetica, 10);


            PdfImage image = PdfImage.FromFile("../../Resources/Images/logo.png");
            RectangleF bounds = new RectangleF(439, 0, 80, 40);
            //Draws the image to the PDF page
            page.Graphics.DrawImage(image, bounds);
            page.Graphics.DrawString("Hello Hospital \n" +
                "Trg Dositeja Obradovica 5\n" +
                "21000 Novi Sad\n" +
                "Phone: 021/555-333\n", fontSmall, PdfBrushes.Black, new PointF(9, 0));

            //Draw the text
            graphics.DrawString("Report on scheduled", font, PdfBrushes.Black, new PointF(180, 70));
            graphics.DrawString("Report on scheduled operations and examinations in: " + DateFrom.ToString().Split(' ')[0] + " - " + DateTo.ToString().Split(' ')[0] + ".", fontSmall, PdfBrushes.Black, new PointF(9, 130));

            dataTable.Columns.Add("Patient");
            dataTable.Columns.Add("Doctor");
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Start time");
            dataTable.Columns.Add("Duration");
            dataTable.Columns.Add("Room");
            foreach (Appointment appointment in _appointmentManagementController.GetAppointmenetsBetweenDate(DateFrom, DateTo))
            {
                dataTable.Rows.Add(new object[] { (_patientController.FindById(appointment.PatientUsername)).FirstName + " " + (_patientController.FindById(appointment.PatientUsername)).LastName, _doctorController.GetByUsername(appointment.DoctorUsername).ToString(), appointment.StartTime.ToString().Split(' ')[0], appointment.StartTime.ToString().Split(' ')[1], "30", appointment.RoomId });
            }
            //////////////
            PdfPen pen = new PdfPen(PdfBrushes.Black, 1);

            //Initialize PdfLinearGradientBrush for drawing the polygon
            PdfLinearGradientBrush brush = new PdfLinearGradientBrush(new PointF(409, 750), new PointF(500, 750), new PdfColor(Color.Black), new PdfColor(Color.Black));

            //Create the polygon points
            PointF p1 = new PointF(409, 750);
            PointF p2 = new PointF(500, 750);
            PointF[] points = { p1, p2 };

            //Draw the polygon on PDF document
            page.Graphics.DrawPolygon(pen, brush, points);
            /////
            ///
            graphics.DrawString("signature", new PdfStandardFont(PdfFontFamily.Helvetica, 8), PdfBrushes.Black, new PointF(440, 752));
            pdfGrid.DataSource = dataTable;
            pdfGrid.Draw(page, new PointF(10, 160));
            doc.Save("Output.pdf");
            doc.Close(true);

            Notifier.ShowSuccess("Successfully generate report");
        }


        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; OnPropertyChanged(nameof(DateFrom)); }
        }
        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; OnPropertyChanged(nameof(DateTo)); }
        }
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged(nameof(Note)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
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
}
