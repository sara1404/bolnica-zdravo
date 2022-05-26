using Controller;
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
    /// Interaction logic for DoctorVacationViewWindow.xaml
    /// </summary>
    public partial class DoctorVacationViewWindow : Window
    {
        private VacationRequestController vc;

        private VacationRequest selectedVacation;
        public DoctorVacationViewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            vc = app.vacationRequestController;
            this.DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorVacationWindow))
                {
                    selectedVacation = (window as DoctorVacationWindow).tableRequests.SelectedItem as VacationRequest;
                    lbStatus.Content = selectedVacation.Status;
                    tbNote.Text = selectedVacation.Note;
                }
            }

        }
    }
}
