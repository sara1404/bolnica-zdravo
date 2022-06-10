using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientCalendar.xaml
    /// </summary>
    public partial class PatientCalendar : Page
    {
        public PatientCalendar()
        {
            InitializeComponent();
            DataContext = new PatientCalendarViewModel();
        }
    }
}
