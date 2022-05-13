using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollQuestion
    {
        private int id;
        private string question;
        private int grade;

        public PollQuestion() { }

        public PollQuestion(int _id, string _question)
        {
            id = _id;
            question = _question;
        }
        public int Id { get => id; set => id = value; }
        public string Question { get => question; set => question = value; }
        public int Grade
        {
            get
            {
                return grade;
            }
            set
            {
                if(value >= 1 && value <= 5)
                {
                    grade = value;
                }
            }
        }
    }
}
