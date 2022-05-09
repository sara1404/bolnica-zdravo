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
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;

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
        public NotificationRepository notificationRepository;
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public NotificationController notificationController { get; set; }
        public MedicalRecordsController mediicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }
        public UserController userController { get; set; }
        public OrderController orderController { get; set; }
        public ScheduledRelocationController scheduledRelocationController { get; set; }
        public ScheduledBasicRenovationController scheduledBasicRenovationController { get; set; }
        public MedicineController medicineController { get; set; }

        public VacationRequestController vacationRequestController { get; set; }
        public InvalidMedicineReportController invalidMedicineReportController { get; set; }

        public Notifier Notifier { get; set; }

        Thread relocationThread;
        Thread renovationThread;
        Thread orderThread;
        public App()
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            userController = new UserController(userService);

            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository);
            notificationController = new NotificationController(notificationService);

            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);

            roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            RoomService roomService = new RoomService(roomRepository, appointmentRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            PatientService patientService = new PatientService(patientRepository, medicalRecordsRepository, userRepository);
            patientController = new PatientController(patientService, userService);


            OrderRepository orderRepository = new OrderRepository();
            OrderService orderService = new OrderService(orderRepository,roomRepository);
            orderController = new OrderController(orderService);

            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository, userController, notificationRepository, doctorService,roomService);
            appointmentController = new AppointmentController(appointmentService);

            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            mediicalRecordsController = new MedicalRecordsController(medicalRecordsService);





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

            VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();
            VacationRequestService vacationRequestService = new VacationRequestService(vacationRequestRepository);
            vacationRequestController = new VacationRequestController(vacationRequestService);

            InvalidMedicineReportRepository invalidMedicineReportRepository = new InvalidMedicineReportRepository();
            InvalidMedicineReportService invalidMedicineReportService = new InvalidMedicineReportService(invalidMedicineReportRepository);
            invalidMedicineReportController = new InvalidMedicineReportController(invalidMedicineReportService);
            
            roomRepository.LoadRoomData();
            scheduledRelocationRepository.LoadRelocationData();
            scheduledBasicRenovationRepository.LoadRenovationData();
            medicineRepository.LoadMedicineData();


            relocationThread = new Thread(scheduledRelocationService.relocationTracker);
            relocationThread.Start();

            renovationThread = new Thread(scheduledBasicRenovationService.renovationTracker);
            renovationThread.Start();

            orderThread = new Thread(orderService.orderTracker);
            orderThread.Start();

            Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }



        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
            scheduledRelocationRepository.WriteRelocationData();
            scheduledBasicRenovationRepository.WriteRenovationData();
            relocationThread.Abort();
            renovationThread.Abort();
            orderThread.Abort();
        }
    }
}
