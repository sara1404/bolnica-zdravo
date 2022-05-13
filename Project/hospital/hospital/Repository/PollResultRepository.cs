using hospital.FileHandler;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class PollResultRepository
    {
        private List<PollBlueprint> pollResults;
        public PollFileHandler pollFileHandler;

        public PollResultRepository()
        {
            pollFileHandler = new PollFileHandler();

            List<PollBlueprint> deserializedList = pollFileHandler.Read();
            if (deserializedList == null)
            {
                pollResults = new List<PollBlueprint>();
            }
            else
            {
                pollResults = new List<PollBlueprint>(pollFileHandler.Read());
            }
        }

        public void AddResult(PollBlueprint result)
        {
            pollResults.Add(result);
            pollFileHandler.Write(pollResults);
        }

        public List<PollBlueprint> GetAllResults()
        {
            return null;
        }

        public List<PollBlueprint> GetHospitalPollResults()
        {
            List<PollBlueprint> retVal = new List<PollBlueprint>();
            foreach(PollBlueprint poll in pollResults)
            {
                if(poll.Type == PollType.HOSPITAL_POLL)
                {
                    retVal.Add(poll);
                }
            }
            return retVal;
        }

        public List<PollBlueprint> GetDoctorPollResults()
        {
            List<PollBlueprint> retVal = new List<PollBlueprint>();
            foreach (PollBlueprint poll in pollResults)
            {
                if (poll.Type == PollType.DOCTOR_POLL)
                {
                    retVal.Add(poll);
                }
            }
            return retVal;
        }
    }
}
