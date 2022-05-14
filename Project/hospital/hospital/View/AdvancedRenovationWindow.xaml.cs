using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
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
            if (e.Key == Key.Enter) {
                int renovationDuration = Int32.Parse(durationRenovation.Text);
                try {
                    CheckTypeOfAdvancedRenovation(renovationDuration);
                }
                catch (Exception ex) {
                    DisplayError();
                }
            }
        }

        private void CheckTypeOfAdvancedRenovation(int renovationDuration) {
            if (listViewRooms.SelectedItems.Count >= 2)
                ListIntervalsWhenMerge(renovationDuration);
            else
                ListIntervalsWhenSplit(renovationDuration);
        }

        private void ListIntervalsWhenMerge(int renovationDuration) {
            List<Room> rooms = new List<Room>();
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForMergingRooms(rooms, renovationDuration);
        }

        private void ListIntervalsWhenSplit(int renovationDuration) {
            Room room = (Room)listViewRooms.SelectedItem;
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForSplitingRoom(room, renovationDuration);
        }

        private void DisplayError() {
            durationRenovation.Text = "Invalid input";
            durationRenovation.Foreground = Brushes.Red;
        }

        private void Schedule_Advanced_Renovation(object sender, RoutedEventArgs e)
        {
            List<Room> rooms = new List<Room>();
            TimeInterval interval = (TimeInterval)renovationListView.SelectedItem;

            if (listViewRooms.SelectedItems.Count >= 2)
                ScheduleMergingRooms(rooms, interval);
            else
                ScheduleSplitingRoom(rooms, interval);
        }

        private void ScheduleMergingRooms(List<Room> rooms, TimeInterval interval) {
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            Room resultRoom = new Room(newRoom.Text, newPurpose.Text, Int32.Parse(floor.Text), newCode.Text);
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(scheduledAdvancedRenovationController.FindAll().Count.ToString(),
                resultRoom, interval, description.Text, rooms, "merge");
            if (!checkRoomFloor(rooms, resultRoom)) {
                MessageBox.Show("Cannot merge rooms on different floors");
                return;
            }
            scheduledAdvancedRenovationController.Create(newRenovation);
            this.Close();
        }

        private void ScheduleSplitingRoom(List<Room> rooms, TimeInterval interval) {
            Room room = (Room)listViewRooms.SelectedItem;
            rooms.Add(new Room(newRoom.Text.Split(',')[0], newPurpose.Text.Split(',')[0], Int32.Parse(floor.Text), newCode.Text.Split(',')[0]));
            rooms.Add(new Room(newRoom.Text.Split(',')[1], newPurpose.Text.Split(',')[1], Int32.Parse(floor.Text), newCode.Text.Split(',')[1]));
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(scheduledAdvancedRenovationController.FindAll().Count.ToString(),
                room, interval, description.Text, rooms, "split");
            if (!checkRoomFloor(rooms, room)) {
                MessageBox.Show("Cannot merge rooms on different floors");
                return;
            }
            scheduledAdvancedRenovationController.Create(newRenovation);
            this.Close();
        }

        private bool checkRoomFloor(List<Room> rooms, Room room) {
            if (rooms[0].floor != rooms[1].floor || rooms[0].floor != room.floor)
                return false;
            return true;
        }

        private void IsFormFilled(object sender, SelectionChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilledValidation() {
            if (listViewRooms.SelectedItems.Count != 0 && renovationListView.SelectedItems.Count != 0 && newRoom.Text != "" && newCode.Text != "" && newPurpose.Text != ""
                && floor.Text != "" && description.Text != "") {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }
    }
}
