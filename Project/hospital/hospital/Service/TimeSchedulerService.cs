using hospital.Model;
using hospital.Repository;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{

    public class TimeSchedulerService
    {

        private AppointmentRepository appointmentRepository;
        private ScheduledBasicRenovationRepository scheduledBasicRenovationRepository;
        private ScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository;

        public TimeSchedulerService(AppointmentRepository appointmentRepository, ScheduledBasicRenovationRepository scheduledBasicRenovationRepository, ScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository) {
            this.appointmentRepository = appointmentRepository;
            this.scheduledBasicRenovationRepository = scheduledBasicRenovationRepository;
            this.scheduledAdvancedRenovationRepository = scheduledAdvancedRenovationRepository;
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration) {
            List<Appointment> appointments = appointmentRepository.FindAll().ToList();
            List<ScheduledBasicRenovation> renovations = scheduledBasicRenovationRepository.FindAll();
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(30);
            List<TimeInterval> forDeleting = new List<TimeInterval>();

            while (true)
            {
                if (last.Date.CompareTo(now.Date.AddDays(renovationDuration)) < 0) break;
                freeTimeIntervals.Add(new TimeInterval(now.Date, now.Date.AddDays(renovationDuration)));
                now = now.Date.AddDays(1);
            }
            forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointments, room);
            forDeleting.AddRange(FindUnavailableIntervalsByBasicRenovations(freeTimeIntervals, renovations, room));

            foreach (TimeInterval interval in forDeleting)
            {
                if (freeTimeIntervals.Contains(interval))
                    freeTimeIntervals.Remove(interval);
            }

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


        public List<TimeInterval> FindTimeIntervalsForMergingRooms(List<Room> rooms, int renovationDuration) {
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(30);
            List<Appointment> appointments = appointmentRepository.FindAll().ToList();
            List<ScheduledAdvancedRenovation> scheduledAdvancedRenovations = scheduledAdvancedRenovationRepository.FindAll();
            List<TimeInterval> forDeleting = new List<TimeInterval>();


            while (true)
            {
                if (last.Date.CompareTo(now.Date.AddDays(renovationDuration)) < 0) break;
                freeTimeIntervals.Add(new TimeInterval(now.Date, now.Date.AddDays(renovationDuration)));
                now = now.Date.AddDays(1);
            }

            forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointments, rooms[0]);
            forDeleting.AddRange(FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointments, rooms[1]));
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, scheduledAdvancedRenovations, rooms[0]));
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, scheduledAdvancedRenovations, rooms[1]));

            foreach (TimeInterval interval in forDeleting)
            {
                if (freeTimeIntervals.Contains(interval))
                    freeTimeIntervals.Remove(interval);
            }

            return freeTimeIntervals;

        }

        public List<TimeInterval> FindTimeIntervalsForSplitingRoom(Room room, int renovationDuration) {
            List<Appointment> appointments = appointmentRepository.FindAll().ToList();
            List<ScheduledAdvancedRenovation> renovations = scheduledAdvancedRenovationRepository.FindAll();
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(30);
            List<TimeInterval> forDeleting = new List<TimeInterval>();

            while (true)
            {
                if (last.Date.CompareTo(now.Date.AddDays(renovationDuration)) < 0) break;
                freeTimeIntervals.Add(new TimeInterval(now.Date, now.Date.AddDays(renovationDuration)));
                now = now.Date.AddDays(1);
            }
            forDeleting = FindUnavailableIntervalsByAppointments(freeTimeIntervals, appointments, room);
            forDeleting.AddRange(FindUnavailableIntervalsByAdvancedRenovations(freeTimeIntervals, renovations, room));
          
            foreach (TimeInterval interval in forDeleting)
            {
                if(freeTimeIntervals.Contains(interval))
                    freeTimeIntervals.Remove(interval);
            }

            return freeTimeIntervals;
        }


        private List<TimeInterval> FindUnavailableIntervalsByAppointments(List<TimeInterval> freeTimeIntervals, List<Appointment> appointments, Room room) {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();
            foreach (TimeInterval interval in freeTimeIntervals)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (appointment.RoomId.Equals(room._Name))
                    {
                        if (interval._Start.Date.CompareTo(appointment.StartTime.Date) == 0 || interval._End.Date.CompareTo(appointment.StartTime.Date) == 0 ||
                            (interval._Start.Date.CompareTo(appointment.StartTime.Date) == -1 && interval._End.Date.CompareTo(appointment.StartTime.Date) == 1))
                        {
                            unavailableIntervals.Add(interval);
                        }

                    }
                }
            }
            return unavailableIntervals;
        }

        private List<TimeInterval> FindUnavailableIntervalsByBasicRenovations(List<TimeInterval> freeTimeIntervals, List<ScheduledBasicRenovation> renovations, Room room) {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();

            foreach (TimeInterval interval in freeTimeIntervals)
            {
                foreach (ScheduledBasicRenovation renovation in renovations)
                {
                    if (renovation._Room._Name.Equals(room._Name))
                    {
                        if (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == 0 || interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 0 ||
                            (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == -1 && interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 1))
                        {
                            unavailableIntervals.Add(interval);
                        }
                    }
                }
            }
            return unavailableIntervals;
        }

        private List<TimeInterval> FindUnavailableIntervalsByAdvancedRenovations(List<TimeInterval> freeTimeIntervals, List<ScheduledAdvancedRenovation> renovations, Room room)
        {
            List<TimeInterval> unavailableIntervals = new List<TimeInterval>();

            foreach (TimeInterval interval in freeTimeIntervals)
            {
                foreach (ScheduledAdvancedRenovation renovation in renovations)
                {
                    if (renovation._Room._Name.Equals(room._Name))
                    {
                        if (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == 0 || interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 0 ||
                            (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == -1 && interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 1))
                        {
                            unavailableIntervals.Add(interval);
                        }
                    }
                }
            }
            return unavailableIntervals;
        }
    }
}
