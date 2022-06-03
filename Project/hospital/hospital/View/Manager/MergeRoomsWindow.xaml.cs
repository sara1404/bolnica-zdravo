using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for MergeRoomsWindow.xaml
    /// </summary>
    public partial class MergeRoomsWindow : Window
    {
        public Room room { get; set; }
        private bool demoStarted;
        private Timer timer;

        public MergeRoomsWindow(bool demoStarted)
        {
            InitializeComponent();
            scheduleBtn.IsEnabled = false;
            this.demoStarted = demoStarted;
            if (demoStarted) StartDemo();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            if (newRoom.Text != "" && newCode.Text != "" && newPurpose.Text != "") {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }

        private void Schedule_Merging(object sender, RoutedEventArgs e)
        {
            CreateRoom();
        }

        private void Cancel_Merging(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateRoom()
        {
            Room room = new Room(newRoom.Text, newPurpose.Text, Int32.Parse(floor.Text), newCode.Text);
            this.room = room;
            Close();
        }

        private void FocusOnSaveButton()
        {
            scheduleBtn.BorderThickness = new Thickness(5, 5, 5, 5);
            scheduleBtn.BorderBrush = Brushes.Pink;
        }

        private void StartDemo()
        {
            Console.WriteLine("Starting demo on new window");
            List<IDemoCommand> commands = new List<IDemoCommand> {
                new FillTxtFieldCommand(newRoom, "0"),
                new FillTxtFieldCommand(newRoom, "00"),
                new FillTxtFieldCommand(newRoom, "003"),
                new FillTxtFieldCommand(newCode, "M"),
                new FillTxtFieldCommand(newCode, "ME"),
                new FillTxtFieldCommand(newCode, "ME0"),
                new FillTxtFieldCommand(newCode, "ME00"),
                new FillTxtFieldCommand(newCode, "ME003"),
                
                new FillTxtFieldCommand(newPurpose, "m"),
                new FillTxtFieldCommand(newPurpose, "me"),
                new FillTxtFieldCommand(newPurpose, "mee"),
                new FillTxtFieldCommand(newPurpose, "meet"),
                new FillTxtFieldCommand(newPurpose, "meeti"),
                new FillTxtFieldCommand(newPurpose, "meetin"),
                new FillTxtFieldCommand(newPurpose, "meeting"),
                new ActionExecuteCommand(FocusOnSaveButton),
                new ActionExecuteCommand(CreateRoom)
            };
            timer = new Timer((Object o) => TimerCallback(commands, timer), null, 0, 500);

        }

        private void TimerCallback(List<IDemoCommand> commands, Timer timer)
        {
            if (commands.Count <= 1)
            {
                Console.WriteLine("Disposing");
                IDemoCommand command = commands[0];
                commands.Clear();
                timer.Dispose();
                command.execute();
                //demoStarted = false;
                return;
            }
            if (commands.Count == 0)
            {
                timer.Dispose();
                return;
            }
            commands[0].execute();
            commands.RemoveAt(0);
        }

        //private void ScheduleMergingRooms(List<Room> rooms, TimeInterval interval)
        //{
        //    try
        //    {
        //        ValidateId(newCode.Text);
        //        ValidateFloor();
        //        CreateMergeRenovation(rooms, interval);
        //        Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void CreateMergeRenovation(List<Room> rooms, TimeInterval interval)
        //{
        //    rooms.Add((Room)listViewRooms.SelectedItems[0]);
        //    rooms.Add((Room)listViewRooms.SelectedItems[1]);
        //    Room resultRoom = new Room(newRoom.Text, newPurpose.Text, Int32.Parse(floor.Text), newCode.Text);
        //    CheckRoomFloor(rooms, resultRoom);
        //    ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(GenerateId().ToString(),
        //        resultRoom, interval, description.Text, rooms, "merge");
        //    scheduledAdvancedRenovationController.Create(newRenovation);
        //}


    }
}
