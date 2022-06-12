using hospital.View.PatientView.ViewModel;
using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientTherapies.xaml
    /// </summary>
    public partial class PatientTherapies : Page
    {
        public PatientTherapies()
        {
            InitializeComponent();
            DataContext = new TherapyViewModel();
        }
    }
}
