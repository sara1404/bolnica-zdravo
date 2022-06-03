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
        public bool TryMakeAppointment(string patientUsername, DateTime newDate, Doctor doctor)
        {
            return _recommendedAppointmentService.TryMakeAppointment(patientUsername, newDate, doctor);
        }
        public void FindFreeForward(ObservableCollection<Appointment> apointments, DateTime time)
        {
            _recommendedAppointmentService.FindFreeForward(apointments, time);
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
        public void FindFreeBack(ObservableCollection<Appointment> apointments, DateTime time)
        {
            _recommendedAppointmentService.FindFreeBack(apointments, time);
        }
        public void FindRecByTime(ObservableCollection<Appointment> apointments, DateTime time)
        {
            _recommendedAppointmentService.FindRecByTime(apointments, time);
        }
        public bool tryChangeAppointment(Appointment oldAppointment, DateTime newDate, string newTime)
        {
            return _recommendedAppointmentService.TryChangeAppointment(oldAppointment, newDate, newTime);
        }
    }
}
