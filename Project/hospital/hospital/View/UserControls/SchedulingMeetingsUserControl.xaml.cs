using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Controller;
using System.Collections.ObjectModel;
using ToastNotifications;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using System.Data;
using System.Collections;

namespace hospital.View.UserControls
{
    public partial class SchedulingMeetingsUserControl : UserControl
    {
        private DoctorController _doctorController;
        private RoomController _roomController;
        private MeetingController _meetingController;
        private List<string> _doctors = new List<string>();
        private Notifier Notifier { get; set; }

        public SchedulingMeetingsUserControl()
        {
            this.DataContext = this;
            App app = Application.Current as App;
            _doctorController = app.doctorController;
            Doctors = _doctorController.GetDoctors();
            InitializeComponent();
            date.DisplayDateStart = DateTime.Today;
            _roomController = app.roomController;
            _meetingController = app.meetingController;
            Rooms = new ObservableCollection<Room>(_roomController.FindRoomsByPurpose("meeting"));
            cbRooms.ItemsSource = Rooms;
            Notifier = GetNotifier();
        }

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DateTime meetingDate = (DateTime)date.SelectedDate;
            DateTime meetingTime = (DateTime)timePicker.Value;
            DateTime meetingStartTime = new DateTime(meetingDate.Year, meetingDate.Month, meetingDate.Day, meetingTime.Hour, meetingTime.Minute, 0);

            Meeting newMeeting = new Meeting(_doctors, meetingStartTime, cbRooms.Text,txtReason.Text);
            _meetingController.Create(newMeeting);
            Notifier.ShowSuccess("Meeting successfully scheduled");
            ResetFields();
        }
        private void ResetFields()
        {
            date.Text = "";
            timePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            cbRooms.Text = "";
            txtReason.Text = "";
            _doctors.Clear();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Doctor selectedDoctor =(Doctor) dateGridHandlingDoctors.SelectedItem;
            _doctors.Add(selectedDoctor.Username);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Doctor selectedDoctor = (Doctor)dateGridHandlingDoctors.SelectedItem;
            _doctors.Remove(selectedDoctor.Username);
        }

        private Notifier GetNotifier()
        {
            return new Notifier(cfg =>
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
    }
    }
