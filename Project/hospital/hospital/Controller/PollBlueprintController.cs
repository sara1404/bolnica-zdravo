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
            return pbs.GetById(0);
        }
    }
}
