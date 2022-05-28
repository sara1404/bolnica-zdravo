using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Meetings
    {
        private List<string> _doctors;
        private DateTime _date;
        private string _roomId;
        private string _meetingTopic;
        private int _id;

        public Meetings(List<string> doctors, DateTime date, string roomId, string meetingTopic)
        {
            Doctors = doctors;
            Date = date;
            Room = roomId;
            MeetingTopic = meetingTopic;
        }

        public List<string> Doctors { get { return _doctors; } set { _doctors = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public string Room { get { return _roomId; } set { _roomId = value; } }
        public string MeetingTopic { get { return _meetingTopic; } set { _meetingTopic = value; } }
        public int Id { get { return _id; } set { _id = value; } }
    }
}
