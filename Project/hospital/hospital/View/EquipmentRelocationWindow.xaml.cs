using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Concurrent;
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
            scheduleBtn.IsEnabled = false;
        }

        private void Show_Appointmets_Click(object sender, KeyEventArgs e)
        {
            string relocationDuration = duration.Text;
            if (e.Key == Key.Enter)
            {
                try
                {
                    int duration = Int32.Parse(relocationDuration);
                    relocationListView.ItemsSource = scheduledRelocationController.FindRelocationIntervals(duration);
                }
                catch (Exception ex) {
                    duration.Foreground = Brushes.Red;
                    duration.Text = "Invalid input";
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Schedule_Relocation_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Int32.Parse(quantity.Text);
            }
            catch (Exception ex) {
                quantity.Text = "Invalid input";
                quantity.Foreground = Brushes.Red;
                return;
            }

            Room fromRoomSelected = roomController.FindRoomByName(fromRoom.SelectedItem.ToString());
            Room toRoomSelected = roomController.FindRoomByName(toRoom.SelectedItem.ToString());
            string equip = equipment.SelectedItem.ToString();
            int equipmentQuantity = Int32.Parse(quantity.Text);
            TimeInterval relocation = (TimeInterval)relocationListView.SelectedItem;
            string id = scheduledRelocationController.FindAll().Count.ToString();
            ScheduledRelocation scheduledRelocation = new ScheduledRelocation(id, fromRoomSelected, toRoomSelected, equip, equipmentQuantity, relocation);
            try
            {
                scheduledRelocationController.Create(scheduledRelocation);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            foreach (Equipment eq in fromRoomSelected.equipment) {
                if (eq.type.Equals(equip)) {
                    if (eq.quantity < equipmentQuantity) {
                        quantity.Text = "Unavailable amount";
                        quantity.Foreground = Brushes.Red;
                        return;
                    }
                    eq.quantity -= equipmentQuantity;
                    if (eq.quantity == 0) {
                        List<Equipment> equipTemp = fromRoomSelected.equipment.ToList();
                        equipTemp.Remove(eq);
                        fromRoomSelected.equipment = new ConcurrentBag<Equipment>(equipTemp);
                    }
                }
            }
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
            if (fromRoom.SelectedIndex != -1 && toRoom.SelectedIndex != -1 && equipment.SelectedIndex != -1 && !quantity.Text.Equals("") && relocationListView.SelectedIndex != -1)
                scheduleBtn.IsEnabled = true;
            string roomName = fromRoom.SelectedItem.ToString();
            equipment.Items.Clear();
            quantity.Text = "";
            loadEquipmentToComboBox(roomName);
        }

        private void Validate(object sender, SelectionChangedEventArgs e)
        {
            ValidateInputs();
        }

        private void Validate(object sender, TextChangedEventArgs e)
        {
            ValidateInputs();
        }

        private void ValidateInputs() {
            if (fromRoom.SelectedIndex != -1 && toRoom.SelectedIndex != -1 && equipment.SelectedIndex != -1 && !quantity.Text.Equals("") && relocationListView.SelectedIndex != -1)
            {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }
    }
}
