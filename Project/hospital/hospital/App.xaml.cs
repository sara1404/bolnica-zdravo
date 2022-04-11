using Controller;
using Repository;
using Service;
using System;
using System.Windows;

namespace hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public RoomRepository roomRepository;
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public MedicalRecordsController mediicalRecordsController { get; set; }
        public DoctorController doctorController { get; set; }

        public App()
        {
            roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);

            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository);
            appointmentController = new AppointmentController(appointmentService);

            MedicalRecordsRepository medicalRecordsRepository = new MedicalRecordsRepository();
            MedicalRecordsService medicalRecordsService = new MedicalRecordsService(medicalRecordsRepository);
            mediicalRecordsController = new MedicalRecordsController(medicalRecordsService);

            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);

            roomRepository.LoadRoomData();
        }

        private void App_Closing(object sender, ExitEventArgs e)
        {
            roomRepository.WriteRoomData();
        }
    }
}
