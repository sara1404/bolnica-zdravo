using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;

namespace Controller
{
    public class MeetingController
    {
        private readonly MeetingService _meetingService;
        private readonly NotificationService _notificationService;
        public MeetingController(MeetingService _mservice, NotificationService _nservice) { _meetingService = _mservice; _notificationService = _nservice; }
        public void Create(Meetings meeting)
        {
            _meetingService.Create(meeting);
            MakeNotification(meeting);
        }
        private void MakeNotification(Meetings meeting)
        {
            foreach(string tmp in meeting.Doctors)
            {
                _notificationService.Create(new Notification(tmp, "New meeting is scheduled."));
            }
        }

        public ObservableCollection<Meetings> FindAll()
        {
            return _meetingService.FindAll();
        }

        public Meetings FindById(int id)
        {
            return _meetingService.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return _meetingService.DeleteById(id);
        }
    }
}
