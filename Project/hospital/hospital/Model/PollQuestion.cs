using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollQuestion
    {
        private int _id;
        private string _question;
        private int _grade;

        public PollQuestion() { }

        public PollQuestion(int id, string question)
        {
            _id = id;
            _question = question;
        }
        public int Id { get => _id; set => _id = value; }
        public string Question { get => _question; set => _question = value; }
        public int Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                if(value >= 1 && value <= 5)
                {
                    _grade = value;
                }
            }
        }
    }
}
