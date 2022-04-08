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
using System.Collections.ObjectModel;
using Model;
using Controller;
namespace hospital.View
{
    /// <summary>
    /// Interaction logic for HandlingAccountPage.xaml
    /// </summary>
    public partial class HandlingAccountPage : Page
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientController pc;
        public HandlingAccountPage()
        {
            InitializeComponent();
            this.DataContext = this;
            pc = new PatientController();
            Patients = pc.FindAll();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAccount.SelectedIndex != -1){
                
                pc.DeleteById(dateGridHandlingAccount.SelectedItem.ToString());
            }
        }
    }
}
