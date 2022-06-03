using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.DTO
{
    public class TherapyDTO
    {
        private string _name;
        private DateTime _date;
        public TherapyDTO(string name, DateTime date)
        {
            _name = name;
            _date = date;
        }

        public DateTime Date { get { return _date; } }
        public string Name { get { return _name; } }
    }
}
