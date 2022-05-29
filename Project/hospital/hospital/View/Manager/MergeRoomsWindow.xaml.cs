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

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for MergeRoomsWindow.xaml
    /// </summary>
    public partial class MergeRoomsWindow : Window
    {
        public Room room { get; set; }
        public MergeRoomsWindow()
        {
            InitializeComponent();
            scheduleBtn.IsEnabled = false;
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
            Close();
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
