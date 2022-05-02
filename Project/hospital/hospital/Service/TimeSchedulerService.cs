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

        public TimeSchedulerService(AppointmentRepository appointmentRepository, ScheduledBasicRenovationRepository scheduledBasicRenovationRepository) {
            this.appointmentRepository = appointmentRepository;
            this.scheduledBasicRenovationRepository = scheduledBasicRenovationRepository;
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
            foreach (TimeInterval interval in freeTimeIntervals) {
                foreach (Appointment appointment in appointments)
                {
                    if (appointment.RoomId.Equals(room._Name))
                    {
                        if (interval._Start.Date.CompareTo(appointment.StartTime.Date) == 0 || interval._End.Date.CompareTo(appointment.StartTime.Date) == 0 ||
                            (interval._Start.Date.CompareTo(appointment.StartTime.Date) == -1 && interval._End.Date.CompareTo(appointment.StartTime.Date) == 1)) {
                            forDeleting.Add(interval);
                        }

                    }
                }
            }

            foreach (TimeInterval interval in freeTimeIntervals) {
                foreach (ScheduledBasicRenovation renovation in renovations) {
                    if (renovation._Room._Name.Equals(room._Name)) {
                        if (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == 0 || interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 0 ||
                            (interval._Start.Date.CompareTo(renovation._Interval._Start.Date) == -1 && interval._End.Date.CompareTo(renovation._Interval._Start.Date) == 1))
                        {
                            forDeleting.Add(interval);
                        }
                    }
                }
            }
            foreach (TimeInterval interval in forDeleting) {
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
            
    }
}
