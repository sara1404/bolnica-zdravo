using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;

namespace Service
{
    public class MeetingService
    {
        private readonly MeetingRepository _meetingRepository;

        public MeetingService(MeetingRepository _repo)
        {
            _meetingRepository = _repo;
        }
        public void Create(Meetings meeting)
        {
            _meetingRepository.Create(meeting);

        }

        public ObservableCollection<Meetings> FindAll()
        {
            return _meetingRepository.FindAll();
        }

        public Meetings FindById(int id)
        {
            return _meetingRepository.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return _meetingRepository.DeleteById(id);
        }
    }
}
