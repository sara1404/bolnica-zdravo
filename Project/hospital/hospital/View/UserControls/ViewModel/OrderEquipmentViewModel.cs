using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using hospital;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Model;
using ToastNotifications.Messages;

namespace ViewModel
{
    public class OrderEquipmentViewModel: INotifyPropertyChanged
    {
        private string _type;
        private int _quantity;
        private string _note;
        private OrderController _orderController;

        private Notifier Notifier { get; set; }

        public RelayCommand AddOrderCommand { get; set; }
        public RelayCommand CancelOrderCommand { get; set; }

        public string ErrType { get; set; }
        public string ErrQuantity { get; set; }

        public String Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public String Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged(nameof(Note)); }
        }


        public OrderEquipmentViewModel()
        {
            InstantiateControllers();
            InsatiateData();
        }
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _orderController = app.orderController;
        }

        private void InsatiateData()
        {
            Notifier = GetNotifier();
            Quantity = 1;
            AddOrderCommand = new RelayCommand(param => ExecuteAddOrderCommand(), param => ValidationInput());
            CancelOrderCommand = new RelayCommand(param => ExecuteCancelOrderCommand(), param => true);
        }
        private bool ValidationInput()
        {
            bool[] isCorrected = new bool[2];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }

            if (Quantity != null && Type != null)
            {
                if (Type.Trim().Equals(""))
                {
                    ErrType = "Must be filled";
                    isCorrected[1] = false;
                }
                else
                {
                    ErrType = "";
                    isCorrected[1] = true;
                }
                return isCorrected[0] & isCorrected[1];
            }
            else
            {
                return false;
            }
        }
        private void ExecuteCancelOrderCommand()
        {
            Type = "";
            Quantity = 1;
            Note = "";
        }
        private void ExecuteAddOrderCommand()
        {
            Equipment newEquipment = new Equipment(Type, Quantity);
            Notifier.ShowSuccess("Order successfully");
            _orderController.Create(new Order(DateTime.Now, newEquipment));
            Type = "";
            Quantity = 1;
            Note = "";

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
