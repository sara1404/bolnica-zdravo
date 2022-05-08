using Controller;
using Repository;
using Service;
using Controller;
using System;
using System.Windows;
using hospital.Model;
using hospital.Controller;
using hospital.Repository;
using hospital.Service;
using System.Threading;
using System.Collections.Generic;

namespace hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public RoomRepository roomRepository;
        public ScheduledRelocationRepository scheduledRelocationRepository;
        public ScheduledBasicRenovationRepository scheduledBasicRenovationRepository;
        public ScheduledAdvancedRenovationRepository scheduledAdvancedRenovationRepository;
        public NotificationRepository notificationRepository;
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public NotificationController notificationController { get; set; }
        public MedicalRecordsController mediicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }
        public UserController userController { get; set; }
        public ScheduledRelocationController scheduledRelocationController { get; set; }
        public ScheduledBasicRenovationController scheduledBasicRenovationController { get; set; }
        public ScheduledAdvancedRenovationController scheduledAdvancedRenovationController { get; set; }
        public MedicineController medicineController { get; set; }

        public App()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            userController = new UserController(userService);

            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository);
            notificationController = new NotificationController(notificationService);

            roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            PatientService patientService = new PatientService(patientRepository, medicalRecordsRepository, userRepository);
            patientController = new PatientController(patientService, userService);

            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository, userController,notificationRepository);
            appointmentController = new AppointmentController(appointmentService);

            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            mediicalRecordsController = new MedicalRecordsController(medicalRecordsService);

            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);




            scheduledBasicRenovationRepository = new ScheduledBasicRenovationRepository();
            scheduledAdvancedRenovationRepository = new ScheduledAdvancedRenovationRepository();
            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository, scheduledBasicRenovationRepository, scheduledAdvancedRenovationRepository);

            ScheduledBasicRenovationService scheduledBasicRenovationService = new ScheduledBasicRenovationService(scheduledBasicRenovationRepository, timeSchedulerService);
            scheduledBasicRenovationController = new ScheduledBasicRenovationController(scheduledBasicRenovationService);

            ScheduledAdvancedRenovationService scheduledAdvancedRenovationService = new ScheduledAdvancedRenovationService(scheduledAdvancedRenovationRepository, timeSchedulerService, roomService);
            scheduledAdvancedRenovationController = new ScheduledAdvancedRenovationController(scheduledAdvancedRenovationService);

            scheduledRelocationRepository = new ScheduledRelocationRepository();
            ScheduledRelocationService scheduledRelocationService = new ScheduledRelocationService(scheduledRelocationRepository, timeSchedulerService);
            scheduledRelocationController = new ScheduledRelocationController(scheduledRelocationService);

            MedicineRepository medicineRepository = new MedicineRepository();
            MedicineService medicineService = new MedicineService(medicineRepository);
            medicineController = new MedicineController(medicineService);
            
            roomRepository.LoadRoomData();
            scheduledRelocationRepository.LoadRelocationData();
            scheduledBasicRenovationRepository.LoadRenovationData();
            scheduledAdvancedRenovationRepository.LoadRenovationData();

            SystemTimer systemTimer = new SystemTimer(scheduledAdvancedRenovationService, scheduledBasicRenovationService, scheduledRelocationService);
        }



        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
            scheduledRelocationRepository.WriteRelocationData();
            scheduledBasicRenovationRepository.WriteRenovationData();
            scheduledAdvancedRenovationRepository.WriteRenovationData();
        }
    }
}
