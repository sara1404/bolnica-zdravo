using hospital.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class TimeSchedulerRepository
    {
        private AppointmentRepository appointmentRepository;
        private ScheduledBasicRenovationRepository scheduledBasicRenovationRepository;
        private ScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository;

        public TimeSchedulerRepository(AppointmentRepository appointmentRepository, ScheduledBasicRenovationRepository scheduledBasicRenovationRepository, ScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.scheduledBasicRenovationRepository = scheduledBasicRenovationRepository;
            this.scheduledAdvancedRenovationRepository = scheduledAdvancedRenovationRepository;
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration)
        {
            List<ScheduledBasicRenovation> renovations = scheduledBasicRenovationRepository.FindAll();
            List<TimeInterval> freeTimeIntervals = GenerateNextMonthIntervals(renovationDuration);
            List<TimeInterval> forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointmentRepository.FindAppointmentsForSpecifiedRoom(room));
            forDeleting.AddRange(FindUnavailableIntervalsByBasicRenovations(freeTimeIntervals, scheduledBasicRenovationRepository.FindForSpecifiedRoom(room)));
            RemoveUnavailableIntervals(freeTimeIntervals, forDeleting);
            return freeTimeIntervals;
        }

        public List<TimeInterval> FindRelocationIntervals(int relocationDuration)
        {
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(7);
            while (true)
            {
                if (last.CompareTo(now.AddDays(relocationDuration)) < 0) break;
                freeTimeIntervals.Add(new TimeInterval(now, now.AddDays(relocationDuration)));
                now = now.AddDays(1);
            }
            return freeTimeIntervals;
        }

        public List<TimeInterval> FindTimeIntervalsForMergingRooms(List<Room> rooms, int renovationDuration)
        {
            List<TimeInterval> freeTimeIntervals = GenerateNextMonthIntervals(renovationDuration);
            List<TimeInterval> forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointmentRepository.FindAppointmentsForSpecifiedRoom(rooms[0]));
            forDeleting.AddRange(FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointmentRepository.FindAppointmentsForSpecifiedRoom(rooms[1])));
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, scheduledAdvancedRenovationRepository.FindForSpecifiedRoom(rooms[0])));
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, scheduledAdvancedRenovationRepository.FindForSpecifiedRoom(rooms[1])));
            RemoveUnavailableIntervals(freeTimeIntervals, forDeleting);

            return freeTimeIntervals;
        }

        public List<TimeInterval> FindTimeIntervalsForSplitingRoom(Room room, int renovationDuration)
        {
            List<Appointment> appointments = appointmentRepository.FindAppointmentsForSpecifiedRoom(room);
            List<ScheduledAdvancedRenovation> renovations = scheduledAdvancedRenovationRepository.FindForSpecifiedRoom(room);
            List<TimeInterval> freeTimeIntervals = GenerateNextMonthIntervals(renovationDuration);

            List<TimeInterval> forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointments);
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, renovations));
            RemoveUnavailableIntervals(freeTimeIntervals, forDeleting);

            return freeTimeIntervals;
        }

        private List<TimeInterval> FindUnavailableIntervalsByAppointments(List<TimeInterval> freeTimeIntervals, List<Appointment> appointments)
        {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();
            foreach (TimeInterval interval in freeTimeIntervals)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (interval.IsOverlaping(new TimeInterval(appointment.StartTime, appointment.StartTime.AddMinutes(appointment.Duration)))) { }
                    unavailableIntervals.Add(interval);
                }
            }
            return unavailableIntervals;
        }

        private List<TimeInterval> FindUnavailableIntervalsByBasicRenovations(List<TimeInterval> freeTimeIntervals, List<ScheduledBasicRenovation> renovations)
        {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();

            foreach (TimeInterval interval in freeTimeIntervals)
            {
                AddUnavailableIntervalByBasicRenovation(unavailableIntervals, interval, renovations);
            }
            return unavailableIntervals;
        }

        private List<TimeInterval> FindUnavailableIntervalsByAdvancedRenovations(List<TimeInterval> freeTimeIntervals, List<ScheduledAdvancedRenovation> renovations)
        {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();

            foreach (TimeInterval interval in freeTimeIntervals)
            {
                AddUnavailableIntervalByAdvancedRenovation(unavailableIntervals, interval, renovations);
            }
            return unavailableIntervals;
        }

        private void AddUnavailableIntervalByBasicRenovation(List<TimeInterval> unavailableIntervals, TimeInterval interval, List<ScheduledBasicRenovation> renovations)
        {
            foreach (ScheduledBasicRenovation renovation in renovations)
            {
                if (interval.IsOverlaping(renovation._Interval))
                {
                    unavailableIntervals.Add(interval);
                }
            }
        }

        private void AddUnavailableIntervalByAdvancedRenovation(List<TimeInterval> unavailableIntervals, TimeInterval interval, List<ScheduledAdvancedRenovation> renovations)
        {
            foreach (ScheduledAdvancedRenovation renovation in renovations)
            {
                if (interval.IsOverlaping(renovation._Interval))
                {
                    unavailableIntervals.Add(interval);
                }
            }
        }

        private List<TimeInterval> GenerateNextMonthIntervals(int renovationDuration)
        {
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(30);
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();

            while (true)
            {
                if (last.Date.CompareTo(now.Date.AddDays(renovationDuration)) < 0) break;
                freeTimeIntervals.Add(new TimeInterval(now.Date, now.Date.AddDays(renovationDuration)));
                now = now.Date.AddDays(1);
            }
            return freeTimeIntervals;
        }



        private void RemoveUnavailableIntervals(List<TimeInterval> freeTimeIntervals, List<TimeInterval> forDeleting)
        {
            foreach (TimeInterval interval in forDeleting)
            {
                if (freeTimeIntervals.Contains(interval))
                    freeTimeIntervals.Remove(interval);
            }
        }
    }
}
