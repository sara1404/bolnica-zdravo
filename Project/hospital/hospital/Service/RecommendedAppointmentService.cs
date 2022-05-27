using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
using hospital.Service;

namespace Service
{
    public class RecommendedAppointmentService
    {
        private readonly AppointmentService _appointmentService;
        private readonly NotificationRepository _notificationRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly AvailableAppointmentService _availableAppointmentService;

        public RecommendedAppointmentService(AppointmentService appointmentService, NotificationRepository notificationRepository, DoctorRepository doctorRepository, AvailableAppointmentService availableAppointmentService)
        {
            _notificationRepository = notificationRepository;
            _appointmentService = appointmentService;
            _doctorRepository = doctorRepository;
            _availableAppointmentService = availableAppointmentService;
        }

        public ObservableCollection<Appointment> GetRecommendedByDoctor(DateTime startDate, DateTime endDate, Doctor doctor, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByDoctor(doctor.Username));
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            List<DateTime> allTimeSlots = AddTimeSlots(startDate, endDate);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            if (allTimeSlots.Count == 0)
            {
                allTimeSlots.AddRange(ExpandDoctorTimeframePre(startDate));
                allTimeSlots.AddRange(ExpandDoctorTimeframePost(endDate));
            }
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                retVal.Add(new Appointment(-1, doctor.Username, patientUsername, time));
            }
            return retVal;
        }

        public ObservableCollection<Appointment> GetRecommendedByDate(DateTime startDate, DateTime endDate, Doctor doctor, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername));
            List<DateTime> allTimeSlots = AddTimeSlots(startDate, endDate);
            List<Appointment> appointments = new List<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                foreach (Doctor d in _doctorRepository.FindAll())
                {
                    if (!DoctorHasAppointment(d, time))
                    {
                        appointments.Add(new Appointment(-1, d.Username, patientUsername, time));
                    }
                }
            } 
            if (FoundDefaultDoctorsAppointment(allTimeSlots, doctor)) appointments.RemoveAll(x => !x.DoctorUsername.Equals(doctor.Username));
            return new ObservableCollection<Appointment>(appointments);
        }

        private bool FoundDefaultDoctorsAppointment(List<DateTime> allTimeSlots, Doctor doctor)
        {
            foreach (DateTime time in allTimeSlots)
            {
                foreach (Doctor d in _doctorRepository.FindAll())
                {
                    if (!DoctorHasAppointment(d, time))
                    {
                        if (d.Username.Equals(doctor.Username))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool DoctorHasAppointment(Doctor d, DateTime time)
        {
            foreach (Appointment a in _appointmentService.GetByDoctor(d.Username))
            {
                if (a.StartTime == time)
                {
                    return true;
                }
            }
            return false;
        }
        private List<DateTime> ConvertAppointmentsToStartTimes(ObservableCollection<Appointment> appointments)
        {
            List<DateTime> startTimes = new List<DateTime>();
            foreach (Appointment a in appointments)
            {
                startTimes.Add(a.StartTime);
            }
            return startTimes;
        }
        private List<DateTime> AddTimeSlots(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (DateTime dayIt = startDate; dayIt <= endDate; dayIt = dayIt.AddDays(1))
            {
                DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                for (int i = 0; i < 24; i++)
                {
                    allTimeSlots.Add(day.AddMinutes(i * 30));
                }
            }
            return allTimeSlots;
        }
        private List<DateTime> ExpandDoctorTimeframePre(DateTime startDate)
        {
            List<DateTime> allTimeSlots = new List<DateTime>();
            DateTime startDatePrev = startDate.CompareTo(DateTime.Today.AddDays(-4)) < 0 ? DateTime.Today.AddDays(1) : startDate.AddDays(-4);
            for (DateTime dayIt = startDatePrev; dayIt <= startDate; dayIt = dayIt.AddDays(1))
            {
                DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                for (int i = 0; i < 24; i++)
                {
                    allTimeSlots.Add(day.AddMinutes(i * 30));
                }
            }
            return allTimeSlots;
        }

        private List<DateTime> ExpandDoctorTimeframePost(DateTime endDate)
        {
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (DateTime dayIt = endDate; dayIt <= endDate.AddDays(4); dayIt = dayIt.AddDays(1))
            {
                DateTime day = new DateTime(dayIt.Year, dayIt.Month, dayIt.Day, 7, 0, 0);
                for (int i = 0; i < 24; i++)
                {
                    allTimeSlots.Add(day.AddMinutes(i * 30));
                }
            }
            return allTimeSlots;
        }

        public bool tryMakeAppointment(string _hours, string _minuts, string patientUsername, string roomId, DateTime date, Doctor doctor)
        {
            ObservableCollection<Appointment> apointments = _availableAppointmentService.GetFreeAppointmentsByDateAndDoctor(date, doctor.Username, patientUsername);
            for (int i = 0; i < apointments.Count; i++)
            {
                string time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                string hours = time.Split(':')[0];
                string minuts = time.Split(':')[1];
                if (_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    (apointments[i]).PatientUsername = patientUsername;
                    (apointments[i]).RoomId = roomId;
                    _appointmentService.Create(apointments[i]);
                    return true;
                }
            }
            return false;
        }
        public Appointment RecommendedOne { set; get; }
        public Appointment RecommendedTwo { set; get; }
        public void findFreeForward(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;
                _hours = ++_hours;
            }
            else if (_minuts == 0)
            {
                _minuts = 30;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i = 0; i < apointments.Count; i++)
            {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours) == (_hours % 13) && Int32.Parse(itemMinuts) == _minuts)
                {
                    RecommendedOne = apointments[i];
                    return;
                }
            }
            findFreeForward(apointments, _hours.ToString(), _minuts.ToString());
        }
        public void findFreeBack(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            int _hours = Int32.Parse(hours);
            int _minuts = Int32.Parse(minuts);
            if (_minuts == 30)
            {
                _minuts = 0;

            }
            else if (_minuts == 0)
            {
                _minuts = 30;
                _hours = --_hours;
            }

            string itemHours;
            string itemMinuts;
            string time;
            for (int i = 0; i < apointments.Count; i++)
            {
                time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                itemHours = time.Split(':')[0];
                itemMinuts = time.Split(':')[1];
                if (Int32.Parse(itemHours) == (_hours % 12) && Int32.Parse(itemMinuts) == _minuts)
                {
                    RecommendedTwo = apointments[i];
                    return;
                }
            }
            findFreeBack(apointments, _hours.ToString(), _minuts.ToString());

        }

        public void findRecByTime(ObservableCollection<Appointment> apointments, string hours, string minuts)
        {
            bool oneRecFilled = false;
            foreach (Appointment item in apointments)
            {
                Console.WriteLine(item.DoctorUsername + " " + item.StartTime);
            }
            foreach (Appointment appointment in apointments)
            {
                string time = (appointment).StartTime.ToString();
                time = time.Split(' ')[1];
                string _hours = time.Split(':')[0];
                string _minuts = time.Split(':')[1];
                if (_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    if (oneRecFilled == false)
                    {
                        RecommendedOne = appointment;
                        oneRecFilled = true;
                    }
                    else
                    {
                        RecommendedTwo = appointment;
                        return;
                    }
                }
            }
            //ako nije uspeo naci tacno taj nek nadje neke najblize
            findFreeBack(apointments, hours, minuts);
            if (!oneRecFilled)
                findFreeForward(apointments, hours, minuts);
        }

        public bool tryChangeAppointment(Appointment oldAppointment, DateTime newDate, string newTime)
        {
            //svi SLOBODNi pregledi tog dana za tog doktora
            ObservableCollection<Appointment> appointments = _availableAppointmentService.GetFreeAppointmentsByDateAndDoctor(newDate, oldAppointment.DoctorUsername, oldAppointment.PatientUsername);
            //zeljeno vreme
            string newHours = newTime.Split(':')[0];
            string newMinuts = newTime.Split(':')[1];
            for (int i = 0; i < appointments.Count; i++)
            {
                string time = (appointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                string hours = time.Split(':')[0];
                string minuts = time.Split(':')[1];
                if (newHours.Equals(hours) && newMinuts.Equals(minuts))
                {
                    Appointment newAppointmet = appointments[i];
                    newAppointmet.PatientUsername = oldAppointment.PatientUsername;
                    _appointmentService.Update(oldAppointment, newAppointmet);
                    _notificationRepository.Create(new Notification(oldAppointment.PatientUsername));
                    _notificationRepository.Create(new Notification(oldAppointment.DoctorUsername));
                    return true;
                }
            }
            return false;
        }
    }
}
