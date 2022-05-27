using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using System.Collections.ObjectModel;

namespace Controller
{
    public class RecommendedAppointmentController
    {
        private readonly RecommendedAppointmentService _recommendedAppointmentService;

        public ObservableCollection<Appointment> GetRecommendedByDoctor(DateTime startDate, DateTime endDate, Doctor doctor, string patientUsername)
        {
            return _recommendedAppointmentService.GetRecommendedByDoctor(startDate, endDate, doctor, patientUsername);
        }

        public ObservableCollection<Appointment> GetRecommendedByDate(DateTime startDate, DateTime endDate, Doctor doctor, string patientUsername)
        {
            return _recommendedAppointmentService.GetRecommendedByDate(startDate, endDate, doctor, patientUsername);
        }

        public RecommendedAppointmentController(RecommendedAppointmentService recommendedAppointmentService)
        {
            this._recommendedAppointmentService = recommendedAppointmentService;
        }
        public bool tryMakeAppointment(string _hours, string _minuts, string patientUsername, string roomId, DateTime date, Doctor doctor)
        {
            return _recommendedAppointmentService.tryMakeAppointment(_hours, _minuts, patientUsername, roomId, date, doctor);
        }
        public void findFreeForward(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            _recommendedAppointmentService.findFreeForward(apointments, hours, minuts);
        }
        public Appointment RecommendedOne
        {
            set { _recommendedAppointmentService.RecommendedOne = value; }
            get { return _recommendedAppointmentService.RecommendedOne; }
        }
        public Appointment RecommendedTwo
        {
            set { _recommendedAppointmentService.RecommendedTwo = value; }
            get { return _recommendedAppointmentService.RecommendedTwo; }
        }
        public void findFreeBack(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            _recommendedAppointmentService.findFreeBack(apointments, hours, minuts);
        }
        public void findRecByTime(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            _recommendedAppointmentService.findRecByTime(apointments, hours, minuts);
        }
        public bool tryChangeAppointment(Appointment oldAppointment, DateTime newDate, string newTime)
        {
            return _recommendedAppointmentService.tryChangeAppointment(oldAppointment, newDate, newTime);
        }
    }
}
