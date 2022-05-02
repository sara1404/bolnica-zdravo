using hospital.Model;
using Model;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientHomeWindow.xaml
    /// </summary>
    public partial class PatientHomeWindow : Window
    {
        public PatientHomeWindow()
        {
            InitializeComponent();
            Main.Content = new PatientMainMenu();

            // loading all notifications
            App app = Application.Current as App;
            User current = app.userController.CurentLoggedUser;
            MedicalRecord mr = app.mediicalRecordsController.FindById(app.patientController.FindById(current.Username).RecordId);
            if (mr != null && mr.Therapy != null)
            {
                foreach (Therapy t in mr.Therapy)
                {
                    new Notification(t.TimeStart, t.TimeEnd, t.Interval, "Take " + t.Medicine.Name);
                }
            }

        }
    }
}
