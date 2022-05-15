using hospital.Model;
using hospital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class PollService
    {
        private PollBlueprintRepository pollBlueprintRepository;
        private PollResultRepository pollResultRepository;

        public PollService(PollBlueprintRepository _pbr, PollResultRepository _prr)
        {
            pollBlueprintRepository = _pbr;
            pollResultRepository = _prr;
        }

        public PollBlueprint GetHospitalPollBlueprint()
        {
            return pollBlueprintRepository.GetHospitalPollBlueprint();
        }

        public PollBlueprint GetDoctorPollBlueprint()
        {
            return pollBlueprintRepository.GetDoctorPollBlueprint();
        }

        public List<PollBlueprint> GetHospitalPollResults()
        {
            return pollResultRepository.GetHospitalPollResults();
        }

        public bool AppointmentPollAlreadyFilled(int appointmentId)
        {
            foreach(PollBlueprint poll in pollResultRepository.GetDoctorPollResults())
            {
                if(poll.AppointmentId == appointmentId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HospitalPollAlreadyFilled(string username)
        {
            foreach (PollBlueprint poll in pollResultRepository.GetHospitalPollResults())
            {
                if (poll.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }

        public void SavePoll(PollBlueprint poll)
        {
            pollResultRepository.AddResult(poll);
        }
    }
}
