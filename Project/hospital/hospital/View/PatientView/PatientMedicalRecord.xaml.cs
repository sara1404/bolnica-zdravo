using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecord.xaml
    /// </summary>
    public partial class PatientMedicalRecord : Page
    {
        public PatientMedicalRecord()
        {
            InitializeComponent();
            DataContext = new MedicalRecordViewModel();
        }

    }
}