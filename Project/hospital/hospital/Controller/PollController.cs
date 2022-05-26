using hospital.Model;
using hospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class PollController
    {
        private PollService pollService;
        
        public PollController(PollService _pollService)
        {
            pollService = _pollService;
        }
        
        public PollBlueprint GetHospitalPollBlueprint()
        {
            return pollService.GetHospitalPollBlueprint();
        }

        public PollBlueprint GetDoctorPollBlueprint()
        {
            return pollService.GetDoctorPollBlueprint();
        }

        public List<PollQuestion> GetHospitalPollQuestions()
        {
            List<PollQuestion> retVal = new List<PollQuestion>();
            foreach(PollCategory category in GetHospitalPollBlueprint().Categories)
            {
                foreach(PollQuestion question in category.PollQuestions)
                {
                    retVal.Add(question);
                }
            }
            return retVal;
        }

        public List<PollQuestion> GetDoctorPollQuestions()
        {
            List<PollQuestion> retVal = new List<PollQuestion>();
            foreach (PollCategory category in GetDoctorPollBlueprint().Categories)
            {
                foreach (PollQuestion question in category.PollQuestions)
                {
                    retVal.Add(question);
                }
            }
            return retVal;
        }

        public bool HospitalPollAlreadyFilled(string username)
        {
            return pollService.HospitalPollAlreadyFilled(username);
        }

        public bool AppointmentPollAlreadyFilled(int appointmentId)
        {
            return pollService.AppointmentPollAlreadyFilled(appointmentId);
        }

        public void SavePoll(PollBlueprint poll)
        {
            pollService.SavePoll(poll);
        }

        public List<PollBlueprint> FindPollResultsForHospital() {
            return pollService.GetHospitalPollResults();
        }

        public List<PollBlueprint> FindPollResultsForDoctor(string id) {
            return pollService.FindPollResultsForDoctor(id);
        }

        public double CalculateDoctorFinalGrade(string doctorId)
        {
            return pollService.CalculateDoctorFinalGrade(doctorId);
        }

        public double CalculateCategoryGrade(string doctorId, int categoryId)
        {
            return pollService.CalculateCategoryGrade(FindPollResultsForDoctor(doctorId), categoryId);
        }

        public double CalculateDoctorQuestionGrade(string doctorId, int categoryId, int questionId)
        {
            return pollService.CalculateAverageForQuestion(FindPollResultsForDoctor(doctorId), categoryId, questionId);
        }

        public int[] CountEachGrade(string doctorId, int categoryId, int questionId) {
            return pollService.CalculateCountOfEachGrade(FindPollResultsForDoctor(doctorId), categoryId, questionId); 
        }

        public double CalculateHospitalFinalGrade()
        {
            return pollService.CalculateHospitalFinalGrade();
        }

        public double CalculateHospitalCategoryGrade(int categoryId)
        {
            return pollService.CalculateCategoryGrade(FindPollResultsForHospital(), categoryId);
        }

        public double CalculateHospitalQuestionGrade(int categoryId, int questionId)
        {
            return pollService.CalculateAverageForQuestion(FindPollResultsForHospital(), categoryId, questionId);
        }

        public int[] CountEachHospitalGrade(int categoryId, int questionId)
        {
            return pollService.CalculateCountOfEachGrade(FindPollResultsForHospital(),categoryId, questionId);
        }
    }
}
