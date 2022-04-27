using hospital.Model;
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

        public TimeSchedulerService(AppointmentRepository appointmentRepository) {
            this.appointmentRepository = appointmentRepository;
        }

        public List<TimeInterval> FindFreeTimeIntervals(Room room, int renovationDuration) {
            List<Appointment> appointments = appointmentRepository.FindAll().ToList();
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
                            Console.WriteLine(interval._Start);
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


    }
}
