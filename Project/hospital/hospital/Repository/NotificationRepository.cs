using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHandler;
using Model;

namespace Repository
{
    public class NotificationRepository
    {
        public NotificationFileHandler notificationFileHandler;
        public ObservableCollection<Notification> notifications;

        public NotificationRepository()
        {
            notificationFileHandler = new NotificationFileHandler();


            List<Notification> deserializedList = notificationFileHandler.Read();
            if (deserializedList != null)
            {
                notifications = new ObservableCollection<Notification>(notificationFileHandler.Read());
            }
            else
            {
                notifications = new ObservableCollection<Notification>();
            }
        }


        public void Create(Notification notification)
        {
            this.notifications.Add(notification);
            notificationFileHandler.Write(this.notifications.ToList());
        }

        public ObservableCollection<Notification> FindAll()
        {
            return notifications;
        }

        public bool Delete(Notification n)
        {
            bool retVal= this.notifications.Remove(n);
            notificationFileHandler.Write(this.notifications.ToList());
            return retVal;  
        }
    }
}
