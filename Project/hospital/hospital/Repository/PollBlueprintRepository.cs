﻿using hospital.Model;
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
                    new PollQuestion(8, "How do you rate our ...?")
                }),
            };
            blueprints.Add(hospitalPoll);
        }

        public ObservableCollection<PollBlueprint> GetAll()
        {
            return blueprints;
        }

        // maybe add GetHospitalPoll() and GetDoctorPoll() methods 
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
    }
}
