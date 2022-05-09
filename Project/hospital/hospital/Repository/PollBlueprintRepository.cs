using hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class PollBlueprintRepository
    {
        public ObservableCollection<PollBlueprint> blueprints;
        
        public PollBlueprintRepository()
        {
            // hospital poll will always have an id of 0
            blueprints = new ObservableCollection<PollBlueprint>();
            PollBlueprint hospitalPoll = new PollBlueprint();
            hospitalPoll.Id = 0;
            hospitalPoll.PollName = "Hospital poll";
            hospitalPoll.PollQuestions = new List<PollQuestion>() { 
                new PollQuestion("Do you like our hospital?", "general"),
                new PollQuestion("Do you like our staff?", "staff"),
                new PollQuestion("Do you like our equipment?", "equipment")};
            blueprints.Add(hospitalPoll);
        }

        public ObservableCollection<PollBlueprint> GetAll()
        {
            return blueprints;
        }

        // maybe add GetHospitalPoll() and GetDoctorPoll() methods 
        public PollBlueprint GetById(int id)
        {
            foreach(PollBlueprint blueprint in blueprints)
            {
                if(blueprint.Id == id)
                {
                    return blueprint;
                }
            }
            return null;
        }
    }
}
