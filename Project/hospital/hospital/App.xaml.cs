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
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public MedicalRecordsController mediicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }
        public UserController userController { get; set; }
        public ScheduledRelocationController scheduledRelocationController { get; set; }
        public ScheduledBasicRenovationController scheduledBasicRenovationController { get; set; }
        public MedicineController medicineController { get; set; }

        Thread relocationThread;
        Thread renovationThread;
        public App()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            userController = new UserController(userService);

            roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            PatientService patientService = new PatientService(patientRepository, medicalRecordsRepository, userRepository);
            patientController = new PatientController(patientService, userService);

            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository, userController);
            appointmentController = new AppointmentController(appointmentService);

            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            mediicalRecordsController = new MedicalRecordsController(medicalRecordsService);

            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);




            scheduledBasicRenovationRepository = new ScheduledBasicRenovationRepository();
            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository, scheduledBasicRenovationRepository);

            ScheduledBasicRenovationService scheduledBasicRenovationService = new ScheduledBasicRenovationService(scheduledBasicRenovationRepository, timeSchedulerService);
            scheduledBasicRenovationController = new ScheduledBasicRenovationController(scheduledBasicRenovationService);


            scheduledRelocationRepository = new ScheduledRelocationRepository();
            ScheduledRelocationService scheduledRelocationService = new ScheduledRelocationService(scheduledRelocationRepository, timeSchedulerService);
            scheduledRelocationController = new ScheduledRelocationController(scheduledRelocationService);

            MedicineRepository medicineRepository = new MedicineRepository();
            MedicineService medicineService = new MedicineService(medicineRepository);
            medicineController = new MedicineController(medicineService);
            
            roomRepository.LoadRoomData();
            scheduledRelocationRepository.LoadRelocationData();

            relocationThread = new Thread(scheduledRelocationService.relocationTracker);
            relocationThread.Start();

            renovationThread = new Thread(scheduledBasicRenovationService.renovationTracker);
            renovationThread.Start();
        }



        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
            scheduledRelocationRepository.WriteRelocationData();
            relocationThread.Abort();
            renovationThread.Abort();
        }
    }
}
