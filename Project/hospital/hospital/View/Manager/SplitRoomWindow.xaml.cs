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
    /// Interaction logic for SplitRoomWindow.xaml
    /// </summary>
    public partial class SplitRoomWindow : Window
    {
        public List<Room> rooms { get; set; }
        public SplitRoomWindow()
        {
            InitializeComponent();
            scheduleBtn.IsEnabled = false;
            floor.IsEnabled = false;
        }

        private void Schedule_Spliting(object sender, RoutedEventArgs e)
        {
            CreateRenovation();
            Close();
        }

        private void CreateRenovation() {
            rooms = new List<Room>();
            rooms.Add(new Room(newRoom1.Text, newPurpose1.Text, Int32.Parse(floor.Text), newCode1.Text));
            rooms.Add(new Room(newRoom2.Text, newPurpose2.Text, Int32.Parse(floor.Text), newCode2.Text));
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
    }
}
