using hospital.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class AvailableAppointmentController
    {
        private AvailableAppointmentService _availableAppointmentService;
        public AvailableAppointmentController(AvailableAppointmentService availableAppointmentService)
        {
            _availableAppointmentService = availableAppointmentService;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string doctorUsername, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDoctor(doctorUsername, patientUsername);
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDate(date, patientUsername);
        }
        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string username, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDateAndDoctor(date, username, patientUsername);
        }
    }
}
