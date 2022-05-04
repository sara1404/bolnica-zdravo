using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;

namespace Service
{
    public class NotificationService
    {
        private readonly NotificationRepository notificationRepository;

        public NotificationService(NotificationRepository _repo) { notificationRepository = _repo; }

        public void Create(Notification notification)
        {
             notificationRepository.Create(notification);
        }

        public ObservableCollection<Notification> FindAll()
        {
            return notificationRepository.FindAll();
        }

        public bool Delete(Notification n)
        {
            return notificationRepository.Delete(n);
        }
    }
}
