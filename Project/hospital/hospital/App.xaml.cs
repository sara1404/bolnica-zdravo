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
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public MedicalRecordsController mediicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }
        public UserController userController { get; set; }
        public ScheduledRelocationController scheduledRelocationController { get; set; }

        public App()
        {
            Repository.UserRepository userRepository = new Repository.UserRepository();
            Service.UserService userService = new Service.UserService(userRepository);
            userController = new UserController(userService);

            roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            PatientService patientService = new PatientService(patientRepository, medicalRecordsRepository);
            patientController = new PatientController(patientService);

            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository, userController);
            appointmentController = new AppointmentController(appointmentService);

            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            mediicalRecordsController = new MedicalRecordsController(medicalRecordsService);

            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);


            scheduledRelocationRepository = new ScheduledRelocationRepository();
            ScheduledRelocationService scheduledRelocationService = new ScheduledRelocationService(scheduledRelocationRepository);
            scheduledRelocationController = new ScheduledRelocationController(scheduledRelocationService);
            
            roomRepository.LoadRoomData();
            scheduledRelocationRepository.LoadRelocationData();

            Thread relocationThread = new Thread(scheduledRelocationService.relocationTracker);
            relocationThread.Start();
        }



        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
            scheduledRelocationRepository.WriteRelocationData();
        }
    }
}
