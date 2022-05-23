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

        public List<PollBlueprint> FindPollResultsForDoctor(string id) {
            return pollResultRepository.FindPollResultsForDoctor(id);
        }


        public double CalculateDoctorFinalGrade(string doctorId) {
            double average = 0;
            foreach (PollCategory category in GetDoctorPollBlueprint().Categories) {
                average += CalculateCategoryGrade(doctorId, category.Id);
            }
            return Math.Round(average / GetDoctorPollBlueprint().Categories.Count, 1);
        }

        public double CalculateCategoryGrade(string doctorId, int categoryId) {
            double average = 0;
            int count = 0;
            foreach (PollBlueprint poll in FindPollResultsForDoctor(doctorId))
            {
                foreach (PollCategory category in poll.Categories)
                {
                    if (category.Id == categoryId)
                    {
                        foreach (PollQuestion question in category.PollQuestions)
                        {
                            Console.WriteLine(question.Grade);
                            average += question.Grade;
                            count++;
                        }
                    }
                }
            }
            return Math.Round(average / count, 1);
        }

        public double CalculateQuestionGrade(string doctorId, int categoryId, int questionId) {
            double average = 0;
            int count = 0;
            foreach (PollBlueprint poll in FindPollResultsForDoctor(doctorId)) {
                foreach (PollCategory category in poll.Categories) {
                    if (category.Id == categoryId) {
                        foreach (PollQuestion question in category.PollQuestions) {
                            if (question.Id == questionId) {
                                average += question.Grade;
                                count++;
                            }
                        }
                    }
                }
            }
            return Math.Round(average / count, 1);
        }

    }
}
