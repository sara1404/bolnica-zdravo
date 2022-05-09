using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollQuestion
    {
        private string question;
        private string category;
        private int grade;

        public PollQuestion() { }

        public PollQuestion(string _question, string _category)
        {
            question = _question;
            category = _category;
        }
        public string Question { get => question; set => question = value; }
        public string Category { get => category; set => category = value; }
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
