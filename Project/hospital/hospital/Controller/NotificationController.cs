using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using System.Collections.ObjectModel;

namespace Controller
{
    public class NotificationController
    {
        private readonly NotificationService notificationService;

        public NotificationController(NotificationService _service) { notificationService = _service; }

        public void Create(Notification notification)
        {
            if (notification.Username == null)
            {
                Console.WriteLine("JEste null");
            }
            Console.WriteLine(notification.Username);
            notificationService.Create(notification);
        }

        public ObservableCollection<Notification> FindAll()
        {
            return notificationService.FindAll();
        }
        public bool Delete(Notification n)
        {
            return notificationService.Delete(n);
        }
    }
}
