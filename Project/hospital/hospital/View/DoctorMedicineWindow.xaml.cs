using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorMedicineWindow.xaml
    /// </summary>
    public partial class DoctorMedicineWindow : Window
    {
        public ObservableCollection<Medicine> medicineList { get; set; }
        private MedicineController mc;
        private InvalidMedicineReportController imrc;
        public DoctorMedicineWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            mc = app.medicineController;
            imrc = app.invalidMedicineReportController;
            medicineList = mc.FindAll();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if(medicineTable.SelectedIndex != -1 && tbNote.Text != "")
            {
                Medicine selectedMedicine = (Medicine)medicineTable.SelectedItem;
                InvalidMedicineReport newReport = new InvalidMedicineReport(selectedMedicine.Id, tbNote.Text, -1);
                imrc.Create(newReport);
                MessageBox.Show("Report sent!");
            }
        }
    }
}
