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
        
        public PollBlueprint GetHospitalPoll()
        {
            return pbs.GetHospitalPoll();
        }

        public List<PollQuestion> GetHospitalPollQuestions()
        {
            List<PollQuestion> retVal = new List<PollQuestion>();
            foreach(PollCategory category in pbs.GetHospitalPoll().Categories)
            {
                foreach(PollQuestion question in category.PollQuestions)
                {
                    retVal.Add(question);
                }
            }
            return retVal;
        }
    }
}
