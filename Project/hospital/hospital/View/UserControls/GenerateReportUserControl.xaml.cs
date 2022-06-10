using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
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
using System.ComponentModel;
using System.Drawing;
using Syncfusion.Pdf.Grid;
using System.Data;
using Controller;
using Model;

namespace hospital.View.UserControls
{
    public partial class GenerateReportUserControl : UserControl
    {
        private AppointmentManagementController _appointmentManagementController;
        private PatientController _patientController;
        private DoctorController _doctorController;
        public GenerateReportUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _appointmentManagementController = app.appointmentController;
            _patientController = app.patientController; 
            _doctorController = app.doctorController;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
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
                RectangleF bounds = new RectangleF(9, 0, 80, 40);
                //Draws the image to the PDF page
                page.Graphics.DrawImage(image, bounds);

                //Draw the text
                graphics.DrawString("Report on scheduled",font, PdfBrushes.Black, new PointF(180, 0));
                graphics.DrawString("Report on scheduled operations and examinations in: " + dpFrom.Text.Split(' ')[0] + " - " + dpTo.Text.Split(' ')[0] + ".", fontSmall, PdfBrushes.Black, new PointF(9, 60));
                //graphics.DrawString(dpFrom.Text.Split(' ')[0] + "-" + dpTo.Text.Split(' ')[0], fontSmall, PdfBrushes.Black, new PointF(200, 0));

                dataTable.Columns.Add("Patient");
                dataTable.Columns.Add("Doctor");
                dataTable.Columns.Add("Date");
                dataTable.Columns.Add("Start time");
                dataTable.Columns.Add("Duration");
                dataTable.Columns.Add("Room");
                foreach (Appointment appointment in _appointmentManagementController.GetAppointmenetsBetweenDate((DateTime)dpFrom.SelectedDate, (DateTime)dpTo.SelectedDate))
                {
                    dataTable.Rows.Add(new object[] {(_patientController.FindById(appointment.PatientUsername)).FirstName + " " + (_patientController.FindById(appointment.PatientUsername)).LastName, _doctorController.GetByUsername(appointment.DoctorUsername).ToString(),appointment.StartTime.ToString().Split(' ')[0], appointment.StartTime.ToString().Split(' ')[1],"30",appointment.RoomId });
                }

                pdfGrid.DataSource = dataTable;
                pdfGrid.Draw(page, new PointF(10, 90));
                doc.Save("Output.pdf");
                doc.Close(true);
            }
        }
        private bool isValidate()
        {
            bool[] isCorrected = new bool[2];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }

            if(dpFrom.Text.Trim() == "")
            {
                isCorrected[0] = false;
                errFrom.Text = "Must be filled.";
            }
            else
            {
                isCorrected[0] = true;
                errFrom.Text = "";
            }

            if (dpTo.Text.Trim() == "")
            {
                isCorrected[1] = false;
                errTo.Text = "Must be filled.";
            }
            else
            {
                isCorrected[1] = true;
                errTo.Text = "";
            }
            return (isCorrected[0] && isCorrected[1]);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void ResetFields()
        {
            dpFrom.Text = "";
            dpTo.Text = "";
            txtNote.Text = "";
            errFrom.Text = "";
            errTo.Text = "";
        }

        private void dpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            errFrom.Text = "";
        }

        private void dpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            errTo.Text = "";
        }
    }
}
