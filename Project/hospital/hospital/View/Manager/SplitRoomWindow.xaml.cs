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
    /// Interaction logic for SplitRoomWindow.xaml
    /// </summary>
    public partial class SplitRoomWindow : Window
    {
        public List<Room> rooms { get; set; }
        private bool demoStarted;
        private Timer timer;

        public SplitRoomWindow(bool demoStarted)
        {
            Console.WriteLine(demoStarted);
            InitializeComponent();
            scheduleBtn.IsEnabled = false;
            floor.IsEnabled = false;
            this.demoStarted = demoStarted;
            if (demoStarted) StartDemo();
        }

        private void Schedule_Spliting(object sender, RoutedEventArgs e)
        {
            CreateRenovation();
        }

        private void CreateRenovation() {
            rooms = new List<Room>();
            rooms.Add(new Room(newRoom1.Text, newPurpose1.Text, Int32.Parse(floor.Text), newCode1.Text));
            rooms.Add(new Room(newRoom2.Text, newPurpose2.Text, Int32.Parse(floor.Text), newCode2.Text));
            Close();
        }

        private void Cancel_Spliting(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            if (newRoom1.Text != "" && newRoom2.Text != "" && newCode1.Text != "" && newCode2.Text != "" && newPurpose1.Text != "" && newPurpose2.Text != "")
            {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }

        private void FocusOnSaveButton()
        {
            scheduleBtn.BorderThickness = new Thickness(5, 5, 5, 5);
            scheduleBtn.BorderBrush = Brushes.Pink;
        }

        private void StartDemo() {
            Console.WriteLine("Starting demo on new window");
            List<IDemoCommand> commands = new List<IDemoCommand> {
                new FillTxtFieldCommand(newRoom1, "0"),
                new FillTxtFieldCommand(newRoom1, "00"),
                new FillTxtFieldCommand(newRoom1, "001"),
                new FillTxtFieldCommand(newRoom2, "0"),
                new FillTxtFieldCommand(newRoom2, "00"),
                new FillTxtFieldCommand(newRoom2, "002"),
                new FillTxtFieldCommand(newCode1, "M"),
                new FillTxtFieldCommand(newCode1, "ME"),
                new FillTxtFieldCommand(newCode1, "ME0"),
                new FillTxtFieldCommand(newCode1, "ME00"),
                new FillTxtFieldCommand(newCode1, "ME001"),
                new FillTxtFieldCommand(newCode2, "M"),
                new FillTxtFieldCommand(newCode2, "ME"),
                new FillTxtFieldCommand(newCode2, "ME0"),
                new FillTxtFieldCommand(newCode2, "ME00"),
                new FillTxtFieldCommand(newCode2, "ME002"),
                new FillTxtFieldCommand(newPurpose1, "m"),
                new FillTxtFieldCommand(newPurpose1, "me"),
                new FillTxtFieldCommand(newPurpose1, "mee"),
                new FillTxtFieldCommand(newPurpose1, "meet"),
                new FillTxtFieldCommand(newPurpose1, "meeti"),
                new FillTxtFieldCommand(newPurpose1, "meetin"),
                new FillTxtFieldCommand(newPurpose1, "meeting"),
                new FillTxtFieldCommand(newPurpose2, "m"),
                new FillTxtFieldCommand(newPurpose2, "me"),
                new FillTxtFieldCommand(newPurpose2, "mee"),
                new FillTxtFieldCommand(newPurpose2, "meet"),
                new FillTxtFieldCommand(newPurpose2, "meeti"),
                new FillTxtFieldCommand(newPurpose2, "meetin"),
                new FillTxtFieldCommand(newPurpose2, "meeting"),
                new ActionExecuteCommand(FocusOnSaveButton),
                new ActionExecuteCommand(CreateRenovation)
            };
            timer = new Timer((Object o) => TimerCallback(commands, timer), null, 0, 500);

        }

        private void TimerCallback(List<IDemoCommand> commands, Timer timer)
        {
            Console.WriteLine(commands.Count);
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
    }
}
