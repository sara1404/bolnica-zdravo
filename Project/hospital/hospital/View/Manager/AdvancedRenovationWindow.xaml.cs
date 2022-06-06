using Controller;
using hospital.Controller;
using hospital.Model;
using hospital.View.Manager;
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

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AdvancedRenovationWindow.xaml
    /// </summary>
    public partial class AdvancedRenovationWindow : Window
    {
        private RoomController roomController;
        private ScheduledAdvancedRenovationController scheduledAdvancedRenovationController;
        private Timer timer;
        private bool demoStarted;

        public AdvancedRenovationWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            roomController = app.roomController;
            scheduledAdvancedRenovationController = app.scheduledAdvancedRenovationController;
            listViewRooms.ItemsSource = roomController.FindAll();
            scheduleBtn.IsEnabled = false;
        }

        private void Show_Appointments_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ListAppointments();
            }
        }

        private void ListAppointments() {
            int renovationDuration = Int32.Parse(durationRenovation.Text);
            try
            {
                CheckTypeOfAdvancedRenovation(renovationDuration);
            }
            catch (Exception ex)
            {
                DisplayError();
            }
        }

        private void CheckTypeOfAdvancedRenovation(int renovationDuration)
        {
            if (listViewRooms.SelectedItems.Count >= 2)
                ListIntervalsWhenMerge(renovationDuration);
            else
                ListIntervalsWhenSplit(renovationDuration);
        }

        private void ListIntervalsWhenMerge(int renovationDuration)
        {
            List<Room> rooms = new List<Room>();
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForMergingRooms(rooms, renovationDuration);
        }

        private void ListIntervalsWhenSplit(int renovationDuration)
        {
            Room room = (Room)listViewRooms.SelectedItem;
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForSplitingRoom(room, renovationDuration);
        }

        private void DisplayError()
        {
            durationRenovation.Text = "Invalid input";
            durationRenovation.Foreground = Brushes.Red;
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Schedule_Advanced_Renovation(object sender, RoutedEventArgs e)
        {
            if (mergeBtn.IsChecked == true)
            {
                ScheduleMergeRenovation();

            }
            else if (splitBtn.IsChecked == true) {
                ScheduleSplitRenovation();
            }
        }

        public void ScheduleMergeRenovation() {
            MergeRoomsWindow mergeRoomsWindow = new MergeRoomsWindow(demoStarted);
            mergeRoomsWindow.floor.Text = ((Room)listViewRooms.SelectedItems[0]).floor.ToString();
            if (mergeRoomsWindow.ShowDialog() == false && IsMergeFormFilled(mergeRoomsWindow.room))
            {
                CreateMergeRenovation(new List<Room>(), (TimeInterval)renovationListView.SelectedItem, mergeRoomsWindow.room);
            }
        }

        public void ScheduleSplitRenovation()
        {
            SplitRoomWindow splitRoomWindow = new SplitRoomWindow(demoStarted);
            splitRoomWindow.floor.Text = (((Room)listViewRooms.SelectedItems[0]).floor.ToString());
            if (splitRoomWindow.ShowDialog() == false && IsSplitFormFilled(splitRoomWindow.rooms))
            {
                CreateSplitRenovation(splitRoomWindow.rooms, (TimeInterval)renovationListView.SelectedItem);
                if(demoStarted)
                {
                    MergeDemo();
                }
            }
        }

        private bool IsMergeFormFilled(Room room)
        {
            if (room == null) return false;
            if (room._Name == null || room.id == null || room._Purpose == null)
            {
                return false;
            }
            return true;
        }

        private bool IsSplitFormFilled(List<Room> rooms)
        {
            if (rooms == null  || rooms[0] == null || rooms[1] == null ) return false;
            if (rooms[0]._Name == null || rooms[0].id == null || rooms[0]._Purpose == null || rooms[1]._Name == null || rooms[1].id == null || rooms[1]._Purpose == null)
            {
                return false;
            }
            return true;
        }


        private void CreateMergeRenovation(List<Room> rooms, TimeInterval interval, Room resultRoom)
        {
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(GenerateId().ToString(),
                resultRoom, interval, description.Text, rooms, "merge");
            scheduledAdvancedRenovationController.Create(newRenovation);
        }

        private void CreateSplitRenovation(List<Room> rooms, TimeInterval interval)
        {
            Room room = (Room)listViewRooms.SelectedItem;
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(GenerateId().ToString(),
                room, interval, description.Text, rooms, "split");
            scheduledAdvancedRenovationController.Create(newRenovation);
        }

        private int GenerateId()
        {
            if (scheduledAdvancedRenovationController.FindAll().Count == 0)
                return 0;
            return Int32.Parse(scheduledAdvancedRenovationController.FindAll()[scheduledAdvancedRenovationController.FindAll().Count - 1]._Id) + 1;
        }

        private void ValidateId(string id)
        {
            if (roomController.FindRoomById(id) != null)
                throw new Exception("Room with this id already exists!");
        }

        private void IsFormFilled(object sender, SelectionChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilledValidation()
        {
            if (listViewRooms.SelectedItems.Count != 0 && renovationListView.SelectedItems.Count != 0 && description.Text != "" && (mergeBtn.IsChecked == true || splitBtn.IsChecked == true))
            {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }

        private void SelectSplitRadioBtn() {
            splitBtn.IsChecked = true;
        }

        private void SelectMergeRadioBtn() {
            mergeBtn.IsChecked = true;
        }

        private void FocusOnSaveButton()
        {
            scheduleBtn.BorderThickness = new Thickness(5, 5, 5, 5);
            scheduleBtn.BorderBrush = Brushes.Pink;
        }

        private void Start_Demo(object sender, RoutedEventArgs e)
        {
            demoStarted = true;
            SplitDemo();
        }

        private void SplitDemo() {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new SelectFromListBoxCommand(listViewRooms, 10),
                new FillTxtFieldCommand(durationRenovation, "0"),
                new ActionExecuteCommand(ListAppointments),
                new SelectFromListBoxCommand(renovationListView, 0),
                new FillTxtFieldCommand(description, "s"),
                new FillTxtFieldCommand(description, "sp"),
                new FillTxtFieldCommand(description, "spl"),
                new FillTxtFieldCommand(description, "spli"),
                new FillTxtFieldCommand(description, "split"),
                new FillTxtFieldCommand(description, "spliti"),
                new FillTxtFieldCommand(description, "splitin"),
                new FillTxtFieldCommand(description, "spliting"),
                new ActionExecuteCommand(SelectSplitRadioBtn),
                new ActionExecuteCommand(FocusOnSaveButton),
                new ActionExecuteCommand(ScheduleSplitRenovation),
            };
            timer = new Timer((Object o) => TimerCallback(commands, timer), null, 0, 500);
        }

        private void MergeDemo() {
            listViewRooms.ItemsSource = roomController.FindAll();

            List<IDemoCommand> commands = new List<IDemoCommand> {
                new FillTxtFieldCommand(durationRenovation, ""),
                new FillTxtFieldCommand(description, ""),
                new SelectMultipleFromListViewCommand(listViewRooms, roomController),
                new FillTxtFieldCommand(durationRenovation, "0"),
                new ActionExecuteCommand(ListAppointments),
                new SelectFromListBoxCommand(renovationListView, 0),
                new FillTxtFieldCommand(description, "m"),
                new FillTxtFieldCommand(description, "me"),
                new FillTxtFieldCommand(description, "mer"),
                new FillTxtFieldCommand(description, "merg"),
                new FillTxtFieldCommand(description, "mergi"),
                new FillTxtFieldCommand(description, "mergin"),
                new FillTxtFieldCommand(description, "merging"),
                new ActionExecuteCommand(SelectMergeRadioBtn),
                new ActionExecuteCommand(FocusOnSaveButton),
                new ActionExecuteCommand(ScheduleMergeRenovation)
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
                return;
            }
            if(commands.Count == 0)
            {
                timer.Dispose();
                return;
            }
            commands[0].execute();
            commands.RemoveAt(0);
        }

        private void CleanAfterDemo() {
            durationRenovation.Text = "";
            renovationListView.Items.Clear();
            description.Text = "";
            splitBtn.IsChecked = false;
        }

        private void IsFormFiled(object sender, RoutedEventArgs e)
        {
            IsFormFilledValidation();
        }
     }


    }
