using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Timers;

namespace Service
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService(NotificationRepository _repo)
        {
            _notificationRepository = _repo;
        }

        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public ObservableCollection<Notification> FindAll()
        {
            return _notificationRepository.FindAll();
        }

        public bool Delete(Notification n)
        {
            return _notificationRepository.Delete(n);
        }
    }
}
