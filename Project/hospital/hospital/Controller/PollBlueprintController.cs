using hospital.Model;
using hospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class PollBlueprintController
    {
        private PollBlueprintService pbs;
        
        public PollBlueprintController(PollBlueprintService _pbs)
        {
            pbs = _pbs;
        }
        
        public PollBlueprint GetHospitalPollBlueprint()
        {
            return pbs.GetHospitalPollBlueprint();
        }

        public PollBlueprint GetDoctorPollBlueprint()
        {
            return pbs.GetDoctorPollBlueprint();
        }

        public List<PollQuestion> GetHospitalPollQuestions()
        {
            List<PollQuestion> retVal = new List<PollQuestion>();
            foreach(PollCategory category in GetHospitalPollBlueprint().Categories)
            {
                foreach(PollQuestion question in category.PollQuestions)
                {
                    retVal.Add(question);
                }
            }
            return retVal;
        }

        public List<PollQuestion> GetDoctorPollQuestions()
        {
            List<PollQuestion> retVal = new List<PollQuestion>();
            foreach (PollCategory category in GetDoctorPollBlueprint().Categories)
            {
                foreach (PollQuestion question in category.PollQuestions)
                {
                    retVal.Add(question);
                }
            }
            return retVal;
        }

        public bool HospitalPollAlreadyFilled(string username)
        {
            foreach(PollBlueprint poll in pbs.GetHospitalPollResults())
            {
                if(poll.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AppointmentPollAlreadyFilled(int appointmentId)
        {
            return pbs.AppointmentPollAlreadyFilled(appointmentId);
        }

        public void SavePoll(PollBlueprint poll)
        {
            pbs.SavePoll(poll);
        }
    }
}
