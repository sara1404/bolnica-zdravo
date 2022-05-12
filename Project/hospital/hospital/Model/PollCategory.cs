using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollCategory
    {
        private int id;
        private string name;
        private List<PollQuestion> pollQuestions;

        public PollCategory(int _id, string _name, List<PollQuestion> _pollQuestions)
        {
            id = _id;
            name = _name;
            pollQuestions = _pollQuestions;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<PollQuestion> PollQuestions { get => pollQuestions; set => pollQuestions = value; }
    }
}
