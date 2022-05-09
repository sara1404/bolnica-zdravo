using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public class PollBlueprint
    {
        private int id;
        private string pollName;
        private List<PollQuestion> pollQuestions;

        public int Id { get => id; set => id = value; }
        public string PollName { get => pollName; set => pollName = value; }
        public List<PollQuestion> PollQuestions { get => pollQuestions; set => pollQuestions = value; }
    }
}
