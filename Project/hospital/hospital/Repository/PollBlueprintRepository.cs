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
            blueprints = new ObservableCollection<PollBlueprint>();
            PollBlueprint hospitalPoll = new PollBlueprint();
            hospitalPoll.Type = PollType.HOSPITAL_POLL;
            hospitalPoll.PollName = "Hospital poll";
            hospitalPoll.Categories = new List<PollCategory>
            {
                new PollCategory(0, "Staff", new List<PollQuestion>()
                {
                    new PollQuestion(0, "How do you rate staff hospitality?"),
                    new PollQuestion(1, "How do you rate staff kindness?"),
                    new PollQuestion(2, "How do you rate staff dedication?")
                }),
                new PollCategory(1, "App", new List<PollQuestion>()
                {
                    new PollQuestion(3, "How do you rate app UI?"),
                    new PollQuestion(4, "How do you rate app UX?"),
                    new PollQuestion(5, "How do you rate app responsiveness?")
                }),
                new PollCategory(2, "Appointments", new List<PollQuestion>()
                {
                    new PollQuestion(6, "How do you rate our recommended appointments functionality?"),
                    new PollQuestion(7, "How do you rate our appointment scheduling system?"),
                    new PollQuestion(8, "How do you rate our waiting rooms?")
                }),
            };
            blueprints.Add(hospitalPoll);

            PollBlueprint doctorPoll = new PollBlueprint();
            doctorPoll.Type = PollType.DOCTOR_POLL;
            doctorPoll.PollName = "Doctor poll";
            doctorPoll.Categories = new List<PollCategory>()
            {
                new PollCategory(0, "Doctor", new List<PollQuestion>()
                {
                    new PollQuestion(0, "How polite was your doctor?"),
                    new PollQuestion(1, "How kind was your doctor?"),
                    new PollQuestion(2, "How would you rate doctors expertise?"),
                }),
                new PollCategory(1, "Appointment", new List<PollQuestion>()
                {
                    new PollQuestion(3, "How appropriate were the medicines the doctor gave you?"),
                    new PollQuestion(4, "Did you have to wait for a long time?"),
                    new PollQuestion(5, "Did you spend a lot of time on appointment?"),
                }),
                new PollCategory(2, "Ordination", new List<PollQuestion>()
                {
                    new PollQuestion(6, "Rate the cleanliness of the ordination?"),
                    new PollQuestion(7, "How equiped was the ordination?"),
                    new PollQuestion(8, "How are you satasfied with our waiting room?"),
                }),
            };
            blueprints.Add(doctorPoll);
        }

        public PollBlueprint GetHospitalPollBlueprint()
        {
            foreach(PollBlueprint blueprint in blueprints)
            {
                if(blueprint.Type == PollType.HOSPITAL_POLL)
                {
                    return blueprint;
                }
            }
            return null;
        }

        public PollBlueprint GetDoctorPollBlueprint()
        {
            foreach (PollBlueprint blueprint in blueprints)
            {
                if (blueprint.Type == PollType.DOCTOR_POLL)
                {
                    return blueprint;
                }
            }
            return null;
        }
    }
}
