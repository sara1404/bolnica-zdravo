using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace FileHandler
{
    public class NotificationFileHandler
    {
        
        private readonly string path = @"../../Resources/Data/NotificationData.txt";

        public List<Notification> Read()
        {
            try
            {
                string serializedNotifications = System.IO.File.ReadAllText(path);
                List<Notification> notifications = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Notification>>(serializedNotifications);
                return notifications;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Notification> notifications)
        {
            string serializedNotifications = Newtonsoft.Json.JsonConvert.SerializeObject(notifications);
            System.IO.File.WriteAllText(path, serializedNotifications);
        }
    }
}

