using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollCategory
    {
        private int _id;
        private string _name;
        private List<PollQuestion> _pollQuestions;

        public PollCategory(int id, string name, List<PollQuestion> pollQuestions)
        {
            _id = id;
            _name = name;
            _pollQuestions = pollQuestions;
        }
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public List<PollQuestion> PollQuestions { get => _pollQuestions; set => _pollQuestions = value; }


        public double CalculateCategoryGradeSum()
        {
            double sum = 0;
            foreach(PollQuestion question in _pollQuestions)
            {
                sum += question.Grade;
            }

            return sum;
        }

        public PollQuestion FindQuestionById(int id) {
            return _pollQuestions.FirstOrDefault(x => x.Id == id);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
