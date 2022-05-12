using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Model
{
    public enum PollType
    {
        HOSPITAL_POLL,
        DOCTOR_POLL
    }
    public class PollBlueprint
    {
        private PollType type; // rename to type and add enum
        private string pollName;
        private string username;
        private int appointmentId;
        private List<PollCategory> categories;
        

        public PollType Type { get => type; set => type = value; }
        public string PollName { get => pollName; set => pollName = value; }
        public string Username { get => username; set => username = value; }
        public int AppointmentId
        {
            get
            {
                return appointmentId;
            }
            set
            {
                if(type == PollType.DOCTOR_POLL)
                {
                    appointmentId = value;
                }
                else
                {
                    // throw exception....
                }
            }
        }
        public List<PollCategory> Categories { get => categories; set => categories = value; }
    }
}
