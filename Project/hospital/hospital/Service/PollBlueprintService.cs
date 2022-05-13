using hospital.Model;
using hospital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class PollBlueprintService
    {
        private PollBlueprintRepository pbr;
        private PollResultRepository prr;

        public PollBlueprintService(PollBlueprintRepository _pbr, PollResultRepository _prr)
        {
            pbr = _pbr;
            prr = _prr;
        }

        public PollBlueprint GetHospitalPollBlueprint()
        {
            return pbr.GetHospitalPollBlueprint();
        }

        public PollBlueprint GetDoctorPollBlueprint()
        {
            return pbr.GetDoctorPollBlueprint();
        }

        public List<PollBlueprint> GetHospitalPollResults()
        {
            return prr.GetHospitalPollResults();
        }

        public bool AppointmentPollAlreadyFilled(int appointmentId)
        {
            foreach(PollBlueprint poll in prr.GetDoctorPollResults())
            {
                if(poll.AppointmentId == appointmentId)
                {
                    return true;
                }
            }
            return false;
        }

        public void SavePoll(PollBlueprint poll)
        {
            prr.AddResult(poll);
        }
    }
}
