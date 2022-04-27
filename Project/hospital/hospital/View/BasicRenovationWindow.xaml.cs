using Controller;
using hospital.Controller;
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
    /// Interaction logic for BasicRenovationWindow.xaml
    /// </summary>
    public partial class BasicRenovationWindow : Window
    {
        private RoomController roomController;
        private ScheduledBasicRenovationController scheduledBasicRenovationController;
        public BasicRenovationWindow()
        {
            App app = Application.Current as App;
            roomController = app.roomController;
            scheduledBasicRenovationController = app.scheduledBasicRenovationController;
            InitializeComponent();
            loadRooms();
        }

        public void loadRooms() {
            foreach (Room room in roomController.FindAll()) {
                roomRenovationListView.Items.Add(room._Name);
            }
        }

        private void Show_Appointments_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                int renovationDuration = 0;
                try
                {
                    renovationDuration = Int32.Parse(durationRenovation.Text);
                }
                catch(Exception ex)
                {
                    durationRenovation.Foreground = Brushes.Red;
                }
                try
                {
                    if (roomRenovationListView.SelectedItem == null) throw new Exception();
                    Room room = roomController.FindRoomByName(roomRenovationListView.SelectedItem.ToString());
                    Console.WriteLine(room.id);
                    renovationListView.ItemsSource = scheduledBasicRenovationController.FindFreeTimeIntervals(room, renovationDuration);
                }
                catch(Exception ex) {
                    roomRenovationListView.Foreground = Brushes.Red;
                }
            }
           
        }
    }
}
