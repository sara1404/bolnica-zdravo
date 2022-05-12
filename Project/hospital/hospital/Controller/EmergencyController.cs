using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class EmergencyController
    {
        private readonly EmergencyService _emergencyService;

        public EmergencyController(EmergencyService emergencyService)
        {
            this._emergencyService = emergencyService;
        }

        public void tryMakeEmergencyAppointment(string patientUsername, Specialization requiredSpecialization, bool isOperation)
        {
            _emergencyService.tryMakeEmergencyAppointment(patientUsername, requiredSpecialization, isOperation);
        }
        public List<Appointment> FindSuggestedAppointments()
        {
            return _emergencyService.FindSuggestedAppointments();
        }
        public List<Appointment> FindAppointmentsForCancelation()
        {
            return _emergencyService.FindAppointmentsForCancelation();
        }
        public void MoveAppointemntAndMakeNotification(Appointment oldAppointment, Appointment newAppoitnemnt)
        {
            _emergencyService.MoveAppointemntAndMakeNotification((Appointment)oldAppointment, (Appointment)newAppoitnemnt);
        }
    }
}
