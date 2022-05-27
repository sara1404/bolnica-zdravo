using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class AvailableAppointmentService
    {
        private readonly AppointmentService _appointmentService;
        private readonly DoctorRepository _doctorRepository;
        public AvailableAppointmentService(AppointmentService appointmentService, DoctorRepository doctorRepository)
        {
            _appointmentService = appointmentService;
            _doctorRepository = doctorRepository;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string doctorUsername, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByDoctor(doctorUsername));
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            List<DateTime> allTimeSlots = AddTimeSlots(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1));
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                retVal.Add(new Appointment(-1, doctorUsername, patientUsername, time));
            }

            return retVal;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername));
            List<DateTime> allTimeSlots = AddTimeSlots(date, date);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                foreach (Doctor d in _doctorRepository.FindAll())
                {
                    if (!DoctorHasAppointment(time, d.Username))
                    {
                        retVal.Add(new Appointment(-1, d.Username, patientUsername, time));
                    }
                }
            }

            return retVal;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string doctorUsername, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByDoctor(doctorUsername));
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            List<DateTime> allTimeSlots = AddTimeSlots(date, date);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> retVal = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                retVal.Add(new Appointment(-1, doctorUsername, patientUsername, time));
            }
            return retVal;
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

        private bool DoctorHasAppointment(DateTime startTime, string doctorUsername)
        {
            foreach (Appointment a in _appointmentService.GetByDoctor(doctorUsername))
            {
                if (a.StartTime == startTime)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
