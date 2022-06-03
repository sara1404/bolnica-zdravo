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

            List<DateTime> allTimeSlots = FilterAllTimeslots(startTimes, startDate, endDate, patientUsername);
            return new ObservableCollection<Appointment>(FillAppointmentsWithTimeslots(allTimeSlots, patientUsername, doctor.Username));
        }

        public ObservableCollection<Appointment> GetRecommendedByDate(DateTime startDate, DateTime endDate, Doctor doctor, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername));
            List<DateTime> allTimeSlots = AddTimeSlots(startDate, endDate);
            List<Appointment> appointments = new List<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                appointments.AddRange(FillAppointmentsWithTimeslotsByDoctor(time, patientUsername));
            }
            if (FoundDefaultDoctorsAppointment(allTimeSlots, doctor)) appointments.RemoveAll(x => !x.DoctorUsername.Equals(doctor.Username));
            return new ObservableCollection<Appointment>(appointments);
        }

        private List<DateTime> FilterAllTimeslots(List<DateTime> startTimes, DateTime startDate, DateTime endDate, string patientUsername)
        {
            List<DateTime> allTimeSlots = AddTimeSlots(startDate, endDate);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            if (allTimeSlots.Count == 0)
            {
                allTimeSlots.AddRange(ExpandDoctorTimeframePre(startDate));
                allTimeSlots.AddRange(ExpandDoctorTimeframePost(endDate));
            }
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            return allTimeSlots;
        }

        private List<Appointment> FillAppointmentsWithTimeslots(List<DateTime> allTimeSlots, string patientUsername, string doctorUsername)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                appointments.Add(new Appointment(-1, doctorUsername, patientUsername, time));
            }
            return appointments;
        }

        private List<Appointment> FillAppointmentsWithTimeslotsByDoctor(DateTime time, string patientUsername)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in _doctorRepository.FindAll())
            {
                if (!DoctorHasAppointment(d, time))
                {
                    appointments.Add(new Appointment(-1, d.Username, patientUsername, time));
                }
            }
            return appointments;
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
            
            DateTime startDatePrev = startDate.CompareTo(DateTime.Today.AddDays(-4)) < 0 ? DateTime.Today.AddDays(1) : startDate.AddDays(-4);
            return AddTimeSlots(startDatePrev, startDate);
        }

        private List<DateTime> ExpandDoctorTimeframePost(DateTime endDate)
        {
            return AddTimeSlots(endDate, endDate.AddDays(4));
        }

        public bool TryMakeAppointment(string patientUsername, DateTime newDate, Doctor doctor)
        {
            ObservableCollection<Appointment> apointments = _availableAppointmentService.GetFreeAppointmentsByDateAndDoctor(newDate, doctor.Username, patientUsername);
            for (int i = 0; i < apointments.Count; i++)
            {
                if ((apointments[i]).StartTime.Hour.Equals(newDate.Hour) && (apointments[i]).StartTime.Minute.Equals(newDate.Minute))
                {
                    (apointments[i]).PatientUsername = patientUsername;
                    (apointments[i]).RoomId = doctor.OrdinationId;
                    _appointmentService.Create(apointments[i]);
                    return true;
                }
            }
            return false;
        }
        public Appointment RecommendedOne { set; get; }
        public Appointment RecommendedTwo { set; get; }
        public void FindFreeForward(ObservableCollection<Appointment> apointments, DateTime time)
        {
           /* if ( time.Minute== 30)
            {
                time = new DateTime(time.Year, time.Month, time.Day, time.Hour+1, 0, 0);
            }
            else if (time.Minute == 0)
            {
                time = new DateTime(time.Year, time.Month, time.Day, time.Hour , 30, 0);
            } */
            time = new DateTime(DateTime.Now.Year, time.Month, time.Day,
                (time.Minute == 30) ? time.Hour + 1 : time.Hour,
                (time.Minute == 30) ? 0 : 30, 0);

            for (int i = 0; i < apointments.Count; i++)
            {
                if ((apointments[i]).StartTime.Hour == (time.Hour % 13) && (apointments[i]).StartTime.Minute == time.Minute)
                {
                    RecommendedOne = apointments[i];
                    return;
                }
            }
            FindFreeForward(apointments, time);
        }
        public void FindFreeBack(ObservableCollection<Appointment> apointments, DateTime time)
        {
            /*if (time.Minute == 30)
            {
                time = new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
            }
            else
            {
                time = new DateTime(time.Year, time.Month, time.Day, time.Hour-1, 30, 0);
            } */

            time = new DateTime(DateTime.Now.Year, time.Month, time.Day,
                (time.Minute == 30) ? time.Hour : time.Hour-1,
                (time.Minute == 30) ? 0 : 30, 0);

            for (int i = 0; i < apointments.Count; i++)
            {
                if ((apointments[i]).StartTime.Hour == (time.Hour % 12) && (apointments[i]).StartTime.Minute == time.Minute)
                {
                    RecommendedTwo = apointments[i];
                    return;
                }
            }
            FindFreeBack(apointments, time);

        }

        public void FindRecByTime(ObservableCollection<Appointment> apointments,DateTime time)
        {
            bool oneRecFilled = false;
            foreach (Appointment appointment in apointments)
            {
                if (appointment.StartTime.Hour.Equals(time.Hour) && appointment.StartTime.Minute.Equals(time.Minute))
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
            FindFreeBack(apointments, time);
            if (!oneRecFilled)
                FindFreeForward(apointments, time);
        }

        public bool TryChangeAppointment(Appointment oldAppointment, DateTime newDate, string newTime)
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
                    MakeNotificationForDelayAppointment(oldAppointment);
                    return true;
                }
            }
            return false;
        }

        private void MakeNotificationForDelayAppointment(Appointment appointment)
        {
            _notificationRepository.Create(new Notification(appointment.PatientUsername,"Your appointment has delayed."));
            _notificationRepository.Create(new Notification(appointment.DoctorUsername, "Your appointment has delayed."));
        }
    }
}
