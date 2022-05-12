using System.Collections.Generic;

namespace Model
{
    public class Medicine : Equipment

    {
        private string id;
        private string name;
        private List<string> ingridients;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public List<string> Ingridients { get => ingridients; set => ingridients = value; }

        private List<Medicine> alternatives;

        

        public Medicine() : base()
        {

        }
        public Medicine(string id, string name, List<string> ingridients, string type, int quantity) : base(type, quantity)
        {
            this.id = id;
            this.name = name;
            this.ingridients = ingridients;
            alternatives = new List<Medicine>();
        }

        public List<Medicine> Alternatives {
            get
            {
                return alternatives;
            }
            set
            {
                alternatives = value;
            }
        }

        public string AlternativesToString() {
            string alternativesToString = "";
            foreach (Medicine alternative in alternatives) {
                alternativesToString += alternative.ToString() + ", ";
            }
            return alternativesToString;
        }

        public override string ToString()
        {
            return name;
        }
    }
}