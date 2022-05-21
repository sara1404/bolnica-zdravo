using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Medicine : Equipment, INotifyPropertyChanged

    {
        private string id;
        private string name;
        private List<string> ingridients;
        private string status;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public List<string> Ingridients
        {
            get
            {
                return ingridients;
            }
            set
            {
                ingridients = value;
                OnPropertyChanged();
            }
        }

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
                OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}