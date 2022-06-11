using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;
using hospital;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using System.Windows;
using ToastNotifications.Messages;
using hospital.View;
using System.Security.Cryptography;

namespace ViewModel
{
    public class ChangePasswordViewModel: INotifyPropertyChanged
    {
        public string _currentPassword;
        private string _newPassword;
        private string _confirmNewPassword;
        private UserController _userController;
        private Notifier Notifier { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ChangePasswordViewModel()
        {
            InstantiateControllers();
            InsatiateData();
        }
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _userController = app.userController;
        }

        private void InsatiateData()
        {
            Notifier = GetNotifier();
            SubmitCommand = new RelayCommand(param => ExecuteSubmitCommand(), param => ValidationInput());
            CancelCommand = new RelayCommand(param => ExecuteCancelCommand(), param => true);
        }
        private bool ValidationInput()
        {
            if(CurrentPassword != null && NewPassword != null && ConfirmNewPassword != null && _userController.CurentLoggedUser != null)
            {
            if (_userController.CurentLoggedUser.Password != ComputeSha256Hash(CurrentPassword))
                return false;
            if (NewPassword != ConfirmNewPassword)
                return false;
            return true;
            }
            else
            {
                return false;
            }


        }
        private void ExecuteSubmitCommand()
        {
            _userController.ChangePassword(_userController.CurentLoggedUser.Username, ComputeSha256Hash(NewPassword));
            Notifier.ShowSuccess("Password successfully changed");
        }
        private void ExecuteCancelCommand()
        {
            CurrentPassword = "";
            ConfirmNewPassword = "";
            NewPassword = "";
        }
        public string CurrentPassword
        {
            get { return _currentPassword; }
            set { _currentPassword = value; OnPropertyChanged(nameof(CurrentPassword)); }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        public string ConfirmNewPassword
        {
            get { return _confirmNewPassword; }
            set {  _confirmNewPassword = value; OnPropertyChanged(nameof(ConfirmNewPassword)); }
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

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}
