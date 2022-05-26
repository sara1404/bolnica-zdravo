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
            foreach (PollBlueprint poll in pollResultRepository.GetDoctorPollResults())
            {
                if (poll.AppointmentId == appointmentId)
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

        public List<PollBlueprint> FindPollResultsForDoctor(string id)
        {
            return pollResultRepository.FindPollResultsForDoctor(id);
        }


        public double CalculateDoctorFinalGrade(string doctorId)
        {
            double average = 0;
            foreach (PollCategory category in GetDoctorPollBlueprint().Categories)
            {
                average += CalculateCategoryGrade(FindPollResultsForDoctor(doctorId), category.Id);
            }
            return Math.Round(average / GetDoctorPollBlueprint().Categories.Count, 1);
        }

        public double CalculateHospitalFinalGrade()
        {
            double average = 0;
            foreach (PollCategory category in GetHospitalPollBlueprint().Categories)
            {
                average += CalculateCategoryGrade(GetHospitalPollResults(), category.Id);
            }
            return Math.Round(average / GetHospitalPollBlueprint().Categories.Count, 1);
        }

        public double CalculateCategoryGrade(List<PollBlueprint> polls, int categoryId)
        {

            double average = 0;
            foreach (PollBlueprint poll in polls)
            {
                PollCategory category = poll.FindPollCategoryById(categoryId);
                average += category.CalculateCategoryGradeSum();
            }
            return Math.Round(average / (polls.Count * 3), 1);
        }

        public double CalculateAverageForQuestion(List<PollBlueprint> polls, int categoryId, int questionId)
        {
            double average = 0;
            foreach (PollBlueprint poll in polls)
            {
                PollCategory category = poll.FindPollCategoryById(categoryId);
                PollQuestion question = category.FindQuestionById(questionId);
                average += question.Grade;
            }

            return Math.Round(average / polls.Count, 1);
        }


        public int[] CalculateCountOfEachGrade(List<PollBlueprint> polls, int categoryId, int questionId)
        {
            int[] result = { 0, 0, 0, 0, 0 };
            foreach (PollBlueprint poll in polls)
            {
                PollCategory category = poll.FindPollCategoryById(categoryId);
                PollQuestion question = category.FindQuestionById(questionId);
                result[question.Grade - 1]++;
            }

            return result;
        }
    }
}
