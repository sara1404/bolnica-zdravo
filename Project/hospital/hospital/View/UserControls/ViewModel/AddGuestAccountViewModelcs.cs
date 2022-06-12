using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Model;
using Controller;
using hospital;

namespace ViewModel
{
    public class AddGuestAccountViewModelcs : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _username;

        private PatientController _patientController;
        private Notifier Notifier { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public AddGuestAccountViewModelcs()
        {
            InstantiateControllers();
            InsatiateData();
        }
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.patientController;
        }

        private void InsatiateData()
        {
            Notifier = GetNotifier();
            //SubmitCommand = new RelayCommand(param => ExecuteAddOrderCommand(), param => ValidationInput());
            CancelCommand = new RelayCommand(param => ExecuteCancelCommand(), param => true);
        }
        private void ExecuteCancelCommand()
        {

        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
