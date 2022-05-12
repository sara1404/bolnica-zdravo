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

        public PollBlueprintService(PollBlueprintRepository _pbr)
        {
            pbr = _pbr;
        }

        public PollBlueprint GetHospitalPoll()
        {
            return pbr.GetHospitalPoll();
        }
    }
}
