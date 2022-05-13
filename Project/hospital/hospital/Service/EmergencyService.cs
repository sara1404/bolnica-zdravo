using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using System.Collections.ObjectModel;
using Repository;
using DTO;

namespace Service
{
    public class EmergencyService
    {
        private readonly AppointmentService _appointmentService;
        private readonly NotificationRepository _notificationRepository;
        private readonly DoctorService doctorService;
        private readonly RoomService _roomService;
        private EmergencyDTO _emergencyDTO;

        public EmergencyService(AppointmentService appointmentService, NotificationRepository notificationRepository, DoctorService doctorService, RoomService roomService)
        {
            this._notificationRepository = notificationRepository;
            this.doctorService = doctorService;
            this._roomService = roomService;
            this._appointmentService = appointmentService;
        }


        public void MoveAppointemntAndMakeNotification(Appointment oldAppointment, Appointment newAppoitnemnt) 
        {
            _appointmentService.Update(oldAppointment, newAppoitnemnt);
            try
            {
                tryMakeEmergencyAppointment(_emergencyDTO.PatientUsername, _emergencyDTO.RequiredSpecialization, _emergencyDTO.IsOperation);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            MakeNotification(oldAppointment.PatientUsername,oldAppointment.doctorUsername);
        }

        public List<DateTime> MakeTimeSlotsNextTwoHours()
        {
            DateTime currentDateAndTime = DateTime.Now;
            List<DateTime> allTimeSlots = new List<DateTime>();
            DateTime startTime;

            if (currentDateAndTime.Minute > 30)
            {
                startTime = new DateTime(currentDateAndTime.Year, currentDateAndTime.Month, currentDateAndTime.Day, currentDateAndTime.AddHours(1).Hour, 0, 0);
            }
            else
            {
                startTime = new DateTime(currentDateAndTime.Year, currentDateAndTime.Month, currentDateAndTime.Day, currentDateAndTime.Hour, 30, 0);

            }
            return GetTimeSlot(startTime);
        }

        private List<DateTime> GetTimeSlot(DateTime startTime)
        {
            List<DateTime> allTimeSlots = new List<DateTime>();
            for (int i = 0; i < 4; i++)
            {
                allTimeSlots.Add(startTime.AddMinutes(i * 30));
            }
            return allTimeSlots;
        }

        private ObservableCollection<Appointment> FindAvailableAppointmentsNextTwoHours(List<Doctor> requiredDoctor, string patientUsername)
        {
             List<DateTime> timeSlots = MakeTimeSlotsNextTwoHours();
             ObservableCollection<Appointment> availableAppointment = new ObservableCollection<Appointment>();
             foreach (Doctor doctor in requiredDoctor)
             {
                 ObservableCollection<Appointment> allAvailableAppointmenTodaytByDoctor = _appointmentService.GetFreeAppointmentsByDateAndDoctor(DateTime.Now, doctor.Username, patientUsername);

                 foreach (Appointment appointment in allAvailableAppointmenTodaytByDoctor)
                 {
                     foreach (DateTime dateTime in timeSlots)
                     {
                         if (appointment.StartTime == dateTime)
                         {
                             appointment.roomId = doctor.OrdinationId;
                             availableAppointment.Add(appointment);
                         }
                     }
                 }
             } 

             return SortAppointmentByTime(availableAppointment);
        }

        private ObservableCollection<Appointment> SortAppointmentByTime(ObservableCollection<Appointment> availableAppointment)
        {
            int size = availableAppointment.Count;

            for (int i = 0; i < (size - 1); i++)
            {
                for (int j = 0; j < (size - i - 1); j++)
                {
                    if (availableAppointment[j].StartTime > availableAppointment[j + 1].StartTime)
                    {
                        Appointment temp = availableAppointment[j];
                        availableAppointment[j] = availableAppointment[j + 1];
                        availableAppointment[j + 1] = temp;
                    }
                }
            }

            return availableAppointment;
        }

        private void MakeEmergencyOperation(ObservableCollection<Appointment> availableAppointment)
        {
            foreach (Appointment app in availableAppointment)
            {
                if (_roomService.FindRoomForOperationByTime(app.StartTime) != null)
                {
                    app.RoomId = _roomService.FindRoomForOperationByTime(app.StartTime).id;
                    _appointmentService.Create(app);
                    MakeNotification(app.patientUsername, app.doctorUsername);
                    return;
                }
            }
        }

        public void tryMakeEmergencyAppointment(string patientUsername, Specialization requiredSpecialization, bool isOperation)
        {
            _emergencyDTO = new EmergencyDTO(patientUsername, requiredSpecialization, isOperation);

            List<Doctor> requiredDoctor = doctorService.GetDoctorsBySpecialization(requiredSpecialization);

            if (requiredDoctor.Count == 0) throw new Exception("There is no doctor with such a specialization");

            ObservableCollection<Appointment> availableAppointment = FindAvailableAppointmentsNextTwoHours(requiredDoctor, patientUsername);

            if (availableAppointment.Count == 0) throw new Exception("No free appointments");

            if (isOperation)
            {
                MakeEmergencyOperation(availableAppointment);
            }
            else
            {
                _appointmentService.Create(availableAppointment[0]);
                MakeNotification(patientUsername, availableAppointment[0].doctorUsername);
            }
        }
        private void MakeNotification(string patientUsername,string doctorUsername)
        {
            _notificationRepository.Create(new Notification(patientUsername));
            _notificationRepository.Create(new Notification(doctorUsername));
        }
        private DateTime FindNearestBusyTimeSlot()
        {
            DateTime currentDateAndTime = DateTime.Now;

            if (currentDateAndTime.Minute > 30)
            {
                return new DateTime(currentDateAndTime.Year, currentDateAndTime.Month, currentDateAndTime.Day, currentDateAndTime.AddHours(1).Hour, 0, 0);
            }
            else
            {
                return new DateTime(currentDateAndTime.Year, currentDateAndTime.Month, currentDateAndTime.Day, currentDateAndTime.Hour, 30, 0);
            }
        }

        public List<Appointment> FindAppointmentsForCancelation()
        {
            DateTime firstBusyTimeSlot = FindNearestBusyTimeSlot();

            List<Appointment> appointmentsForCancelation = new List<Appointment>();
            foreach (Appointment appointment in _appointmentService.GetAll())
            {
                if (appointment.StartTime == firstBusyTimeSlot)
                    appointmentsForCancelation.Add(appointment);
            }
            return appointmentsForCancelation;
        }

        private void FindNewSuggestedAppointment(Appointment delayAppointment)
        {
            ObservableCollection<Appointment> allFreeAppointment = _appointmentService.GetFreeAppointmentsByDateAndDoctor(delayAppointment.StartTime, delayAppointment.doctorUsername, delayAppointment.patientUsername);
            foreach (Appointment appointment in allFreeAppointment)
            {
                if (delayAppointment.StartTime < appointment.StartTime)
                {
                    _appointmentService.RecommendedOne = appointment;
                    return;
                }
            }
            delayAppointment.StartTime.AddDays(1);
            FindNewSuggestedAppointment(delayAppointment);
        }

        public List<Appointment> FindSuggestedAppointments()
        {
            List<Appointment> suggestionAppointments = new List<Appointment>();

            foreach (Appointment appointment in FindAppointmentsForCancelation())
            {
                FindNewSuggestedAppointment(appointment);
                suggestionAppointments.Add(_appointmentService.RecommendedOne);
            }
            return suggestionAppointments;

        }
    }
}
