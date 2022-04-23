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
    /// Interaction logic for EquipmentRelocationWindow.xaml
    /// </summary>
    public partial class EquipmentRelocationWindow : Window
    {

        RoomController roomController;
        ScheduledRelocationController scheduledRelocationController;

        public EquipmentRelocationWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            //this.room = room;
            roomController = app.roomController;
            scheduledRelocationController = app.scheduledRelocationController;
            loadRoomsToComboBoxes();
        }

        private void Show_Appointmets_Click(object sender, KeyEventArgs e)
        {
            string relocationDuration = duration.Text;
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            DateTime now = DateTime.Now;
            DateTime last = now.AddDays(7);
            if (e.Key == Key.Enter)
            {

                int intervalDuration  = Int32.Parse(relocationDuration);
                while(true)
                {
                    if (last.CompareTo(now.AddDays(intervalDuration)) < 0) break;
                    freeTimeIntervals.Add(new TimeInterval(freeTimeIntervals.Count.ToString(), now, now.AddDays(intervalDuration)));
                    now = now.AddDays(1);
                }
                relocationListView.ItemsSource = freeTimeIntervals;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Schedule_Relocation_Click(object sender, RoutedEventArgs e)
        {
            Room fromRoomSelected = roomController.FindRoomByName(fromRoom.SelectedItem.ToString());
            Room toRoomSelected = roomController.FindRoomByName(toRoom.SelectedItem.ToString());
            string equip = equipment.SelectedItem.ToString();
            int equipmentQuantity = Int32.Parse(quantity.Text);
            String relocationId = relocationListView.SelectedItem.ToString().Split('\t')[0];
            //TimeInterval relocation = relocationController.FindById(relocationId);
            TimeInterval relocation = (TimeInterval)relocationListView.SelectedItem;
            string id = scheduledRelocationController.FindAll().Count.ToString();
            ScheduledRelocation scheduledRelocation = new ScheduledRelocation(id, fromRoomSelected, toRoomSelected, equip, equipmentQuantity, relocation);
            scheduledRelocationController.Create(scheduledRelocation);
            this.Close();
        }
        private void Cancel_Relocation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loadRoomsToComboBoxes() {
            foreach (Room room in roomController.FindAll()) {
                fromRoom.Items.Add(room._Name);
                toRoom.Items.Add(room._Name);
            }
        }

        private void loadEquipmentToComboBox(string roomName) {
            foreach (Room room in roomController.FindAll()) {
                if (room._Name.Equals(roomName)) {
                    foreach (Equipment eq in room.equipment) {
                        equipment.Items.Add(eq.type);
                        quantity.Text = eq.quantity.ToString();
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string roomName = fromRoom.SelectedItem.ToString();
            equipment.Items.Clear();
            quantity.Text = "";
            loadEquipmentToComboBox(roomName);
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
