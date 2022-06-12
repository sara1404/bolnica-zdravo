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
using Controller;
using System.Collections.ObjectModel;
using System.Data;
using System.ComponentModel;
using ViewModel;

namespace hospital.View.UserControls
{
    public partial class RevisionOfRestUserControl : UserControl
    {
      /*  private VacationRequestController _vacationRequestController;
        private DoctorController _doctorController;
        private ObservableCollection<VacationRequest> _vacationRequests;
        private VacationRequest _currentSelected; */
        public RevisionOfRestUserControl()
        {
            this.DataContext = new RevisionOfRestViewModel();
           /* App app = Application.Current as App;
            _vacationRequestController = app.vacationRequestController;
            _doctorController = app.doctorController;
            _vacationRequests = _vacationRequestController.FindAll(); */
            InitializeComponent();
        }
  /*      public ObservableCollection<VacationRequest> VacationRequests
        {
            get { return _vacationRequests; }
            set { _vacationRequests = value; }
        }
        private void dateGridHandlingRest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSelected = (VacationRequest)dateGridHandlingRest.SelectedItem;
            if (_currentSelected != null)
            {
                if(_currentSelected.Status == Status.approved || _currentSelected.Status == Status.rejected)
                {
                    btnApprove.IsEnabled = false;
                    btnReject.IsEnabled = false;
                }
                else
                {
                    btnApprove.IsEnabled = true;
                    btnReject.IsEnabled = true;
                }
                txtDate.Text = _currentSelected.StartDate.ToString().Split(' ')[0] + "-" + _currentSelected.EndDate.ToString().Split(' ')[0];
                txtMotive.Text = _currentSelected.Note;
                header.Header = _doctorController.GetByUsername(_currentSelected.DoctorId);
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            _vacationRequestController.FinishRequest("approve", _currentSelected.Id);
            dateGridHandlingRest.Items.Refresh();
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            _vacationRequestController.FinishRequest("reject", _currentSelected.Id);
            dateGridHandlingRest.Items.Refresh();
        }
        private void ResetFields()
        {
            txtDate.Text = "";
            txtMotive.Text = "";
            txtReason.Text = "";
        } */
    }
}
