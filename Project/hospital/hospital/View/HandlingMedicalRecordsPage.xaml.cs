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
using System.Collections.ObjectModel;
using Controller;
namespace hospital.View
{
    /// <summary>
    /// Interaction logic for HandlingMedicalRecordsPage.xaml
    /// </summary>
    public partial class HandlingMedicalRecordsPage : Page
    {
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }
        public MedicalRecordsController mc;
        public HandlingMedicalRecordsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            mc = app.mediicalRecordsController;
            MedicalRecords = mc.FindAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addMedRecUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingMedRec.SelectedIndex != -1)
            {
                mc.DeleteById(((MedicalRecord)dateGridHandlingMedRec.SelectedItem).RecordId);
            }
        }
    }
}
