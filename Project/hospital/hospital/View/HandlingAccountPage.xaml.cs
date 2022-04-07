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
namespace hospital.View
{
    /// <summary>
    /// Interaction logic for HandlingAccountPage.xaml
    /// </summary>
    public partial class HandlingAccountPage : Page
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public HandlingAccountPage()
        {
            InitializeComponent();
            this.DataContext = this;
            Patients = new ObservableCollection<Patient>();
            Patients.Add(new Patient("Nikola", "Kalinic", "123"));
            Patients.Add(new Patient("Nikola", "Kalinic", "123"));
            Patients.Add(new Patient("Nikola", "Kalinic", "123"));
        }
    }
}
