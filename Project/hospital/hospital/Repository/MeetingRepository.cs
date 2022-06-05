using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using FileHandler;
using System.Collections.ObjectModel;

namespace Repository
{
    public class MeetingRepository
    {
        public MeetingFileHandler meetingFileHandler;
        public ObservableCollection<Meeting> meetings;

        public MeetingRepository()
        {
            meetingFileHandler = new MeetingFileHandler();


            List<Meeting> deserializedList = meetingFileHandler.Read();
            if (deserializedList != null)
            {
                meetings = new ObservableCollection<Meeting>(meetingFileHandler.Read());
            }
            else
            {
                meetings = new ObservableCollection<Meeting>();
            }

        }


        public void Create(Meeting newMeeting)
        {
            newMeeting.Id = GetNewId();
            this.meetings.Add(newMeeting);
            meetingFileHandler.Write(this.meetings.ToList());
        }
        public ObservableCollection<Meeting> FindAll()
        {
            return meetings;
        }

        public Meeting FindById(int id)
        {
            foreach (Meeting o in meetings)
            {
                if (o.Id == id)
                {
                    return o;
                }
            }
            return null;
        }
        public bool DeleteById(int id)
        {
            bool retVal = meetings.Remove(FindById(id));
            meetingFileHandler.Write(this.meetings.ToList());
            return retVal;
        }
        public int GetNewId()
        {
            if (meetings.Count == 0)
            {
                return 0;
            }
            else
            {
                int max = 0;
                foreach (Meeting a in meetings)
                {
                    if (a.Id > max)
                    {
                        max = a.Id;
                    }
                }
                return max + 1;
            }
        }
    }
}


    

