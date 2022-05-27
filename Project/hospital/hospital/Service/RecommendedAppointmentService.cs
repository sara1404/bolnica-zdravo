using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;

namespace Service
{
    public class RecommendedAppointmentService
    {
        private readonly AppointmentService _appointmentService;
        private readonly NotificationRepository _notificationRepository;


        public RecommendedAppointmentService(AppointmentService appointmentService, NotificationRepository notificationRepository)
        {
            this._notificationRepository = notificationRepository;
            this._appointmentService = appointmentService;
        }


        public bool tryMakeAppointment(string _hours, string _minuts, string patientUsername, string roomId, DateTime date, Doctor doctor)
        {
            ObservableCollection<Appointment> apointments = _appointmentService.GetFreeAppointmentsByDateAndDoctor(date, doctor.Username, patientUsername);
            for (int i = 0; i < apointments.Count; i++)
            {
                string time = (apointments[i]).StartTime.ToString();
                time = time.Split(' ')[1];
                string hours = time.Split(':')[0];
                string minuts = time.Split(':')[1];
                if (_hours.Equals(hours) && _minuts.Equals(minuts))
                {
                    (apointments[i]).patientUsername = patientUsername;
                    (apointments[i]).roomId = roomId;
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
                Console.WriteLine(item.doctorUsername + " " + item.StartTime);
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
            ObservableCollection<Appointment> appointments = _appointmentService.GetFreeAppointmentsByDateAndDoctor(newDate, oldAppointment.doctorUsername, oldAppointment.patientUsername);
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
                    newAppointmet.patientUsername = oldAppointment.patientUsername;
                    _appointmentService.Update(oldAppointment, newAppointmet);
                    _notificationRepository.Create(new Notification(oldAppointment.PatientUsername));
                    _notificationRepository.Create(new Notification(oldAppointment.doctorUsername));
                    return true;
                }
            }
            return false;
        }
    }
}
