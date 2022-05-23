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

        public double CalculateHospitalFinalGrade()
        {
            double average = 0;
            foreach (PollCategory category in GetHospitalPollBlueprint().Categories)
            {
                average += CalculateHospitalCategoryGrade(category.Id);
            }
            return Math.Round(average / GetHospitalPollBlueprint().Categories.Count, 1);
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

        public double CalculateHospitalCategoryGrade(int categoryId)
        {
            double average = 0;
            int count = 0;
            foreach (PollBlueprint poll in GetHospitalPollResults())
            {
                foreach (PollCategory category in poll.Categories)
                {
                    if (category.Id == categoryId)
                    {
                        foreach (PollQuestion question in category.PollQuestions)
                        {
                            average += question.Grade;
                            count++;
                        }
                    }
                }
            }
            return Math.Round(average / count, 1);
        }

        public double CalculateDoctorQuestionGrade(string doctorId, int categoryId, int questionId) {
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

        public double CalculateHospitalQuestionGrade(int categoryId, int questionId)
        {
            double average = 0;
            int count = 0;
            foreach (PollBlueprint poll in GetHospitalPollResults())
            {
                foreach (PollCategory category in poll.Categories)
                {
                    if (category.Id == categoryId)
                    {
                        foreach (PollQuestion question in category.PollQuestions)
                        {
                            if (question.Id == questionId)
                            {
                                average += question.Grade;
                                count++;
                            }
                        }
                    }
                }
            }
            return Math.Round(average / count, 1);
        }

        public int[] CalculateCountOfEachGrade(string doctorId, int categoryId, int questionId) {
            int[] result = { 0, 0, 0, 0, 0 };
            foreach (PollBlueprint poll in FindPollResultsForDoctor(doctorId))
            {
                foreach (PollCategory category in poll.Categories)
                {
                    if (category.Id == categoryId)
                    {
                        foreach (PollQuestion question in category.PollQuestions)
                        {
                            if (question.Id == questionId)
                            {
                                result = CountEachGrade(question.Grade, result);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public int[] CalculateCountOfEachHospitalGrade(int categoryId, int questionId)
        {
            int[] result = { 0, 0, 0, 0, 0 };
            foreach (PollBlueprint poll in GetHospitalPollResults())
            {
                foreach (PollCategory category in poll.Categories)
                {
                    if (category.Id == categoryId)
                    {
                        foreach (PollQuestion question in category.PollQuestions)
                        {
                            if (question.Id == questionId)
                            {
                                result = CountEachGrade(question.Grade, result);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public int[] CountEachGrade(int grade, int[] grades)
        {
            switch (grade)
            {
                case 1:
                    grades[0]++;
                    break;
                case 2:
                    grades[1]++;
                    break;
                case 3:
                    grades[2]++;
                    break;
                case 4:
                    grades[3]++;
                    break;
                case 5:
                    grades[4]++;
                    break;
            }
            return grades;
        }

    }
}
