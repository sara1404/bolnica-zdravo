using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using hospital;
using Model;

namespace ViewModel
{
    public class RevisionOfRestViewModel : INotifyPropertyChanged
    {
        private VacationRequestController _vacationRequestController;
        private DoctorController _doctorController;
        private String _date;
        private VacationRequest _selectedItem;
        private string _motive;
        public ObservableCollection<VacationRequest> VacationRequests { get; set; }
        public String Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public RelayCommand ApproveCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        public string Motive { get { return _motive; } set { _motive = value; OnPropertyChanged(nameof(Motive)); } }
        public VacationRequest SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                DoctorName = _doctorController.GetByUsername(_selectedItem.DoctorId).ToString();
                Date = _selectedItem.StartDate.ToString().Split(' ')[0] + " - " + _selectedItem.EndDate.ToString().Split(' ')[0];
                Motive = _selectedItem.Note;
                OnPropertyChanged(nameof(DoctorName));
            }
        }
        private string _doctorName;
        public string DoctorName
        {
            get { return _doctorName; }
            set { _doctorName = value; OnPropertyChanged(nameof(DoctorName)); }
        }

        public RevisionOfRestViewModel()
        {
            InstantiateControllers();
            InsatiateData();
        }
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _vacationRequestController = app.vacationRequestController;
            _doctorController = app.doctorController;
        }

        private void InsatiateData()
        {
            VacationRequests = _vacationRequestController.FindAll();
            //Notifier = GetNotifier();
            //Quantity = 1;
            ApproveCommand = new RelayCommand(param => ExecuteApproveCommand(), param => Validation());
            RejectCommand = new RelayCommand(param => ExecuteRejectCommand(), param => Validation());
        }

        private bool Validation()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem.Status == Status.waiting)
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }
        private void ExecuteApproveCommand()
        {
            _vacationRequestController.FinishRequest("approve", SelectedItem.Id);
            VacationRequests = _vacationRequestController.FindAll();
        }
        private void ExecuteRejectCommand()
        {
            _vacationRequestController.FinishRequest("reject", SelectedItem.Id);
            VacationRequests = _vacationRequestController.FindAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
