using Model;
using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientAppointmentNote.xaml
    /// </summary>
    public partial class PatientAppointmentNote : Page
    {
        public PatientAppointmentNote(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new AppointmentNoteViewModel(appointment);
        }
    }
}
