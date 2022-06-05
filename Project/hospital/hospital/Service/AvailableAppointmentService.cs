using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace hospital.Service
{
    public class AvailableAppointmentService
    {
        private readonly AppointmentManagementService _appointmentService;
        private readonly DoctorRepository _doctorRepository;
        private readonly MeetingService _meetingService;
        public AvailableAppointmentService(AppointmentManagementService appointmentService, DoctorRepository doctorRepository, MeetingService meetingService)
        {
            _appointmentService = appointmentService;
            _doctorRepository = doctorRepository;
            _meetingService = meetingService;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string doctorUsername, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByDoctor(doctorUsername));
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            List<DateTime> allTimeSlots = AddTimeSlots(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1));
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                appointments.Add(new Appointment(-1, doctorUsername, patientUsername, time));
            }

            return appointments;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername));
            List<DateTime> allTimeSlots = AddTimeSlots(date, date);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            List<Appointment> appointments = new List<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                appointments.AddRange(FillAppointmentsWithAllDoctors(time, patientUsername));
            }

            return new ObservableCollection<Appointment>(appointments);
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string doctorUsername, string patientUsername)
        {
            List<DateTime> startTimes = ConvertAppointmentsToStartTimes(_appointmentService.GetByDoctor(doctorUsername));
            startTimes.AddRange(ConvertAppointmentsToStartTimes(_appointmentService.GetByPatient(patientUsername)));
            List<DateTime> allTimeSlots = AddTimeSlots(date, date);
            allTimeSlots.RemoveAll(x => startTimes.Contains(x));
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (DateTime time in allTimeSlots)
            {
                appointments.Add(new Appointment(-1, doctorUsername, patientUsername, time));
            }
            return appointments;
        }

        private List<Appointment> FillAppointmentsWithAllDoctors(DateTime time, string patientUsername)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Doctor d in _doctorRepository.FindAll())
            {
                if (!DoctorHasAppointment(time, d.Username))
                {
                    appointments.Add(new Appointment(-1, d.Username, patientUsername, time));
                }
            }
            return appointments;
        }

        private List<DateTime> ConvertAppointmentsToStartTimes(ObservableCollection<Appointment> appointments)
        {
            List<DateTime> startTimes = new List<DateTime>();
            foreach (Appointment a in appointments)
            {
                startTimes.Add(a.StartTime);
            }
            startTimes.AddRange(_meetingService.FindAllMeetingsStartTime());
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
