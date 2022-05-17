using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FileHandler;
using Model;

namespace Repository
{
    public class NotificationRepository
    {
        public NotificationFileHandler _notificationFileHandler;
        public ObservableCollection<Notification> _notifications;
        private MedicalRecordsRepository _medicalRecordsRepository;

        public NotificationRepository()
        {
            _notificationFileHandler = new NotificationFileHandler();

            List<Notification> deserializedList = _notificationFileHandler.Read();
            if (deserializedList != null)
            {
                _notifications = new ObservableCollection<Notification>(_notificationFileHandler.Read());
            }
            else
            {
                _notifications = new ObservableCollection<Notification>();
            }
        }

        public void Create(Notification notification)
        {
            _notifications.Add(notification);
            _notificationFileHandler.Write(_notifications.ToList());
        }

        public ObservableCollection<Notification> FindAll()
        {
            return _notifications;
        }

        public bool Delete(Notification n)
        {
            bool retVal = _notifications.Remove(n);
            _notificationFileHandler.Write(_notifications.ToList());
            return retVal;
        }
    }
}
