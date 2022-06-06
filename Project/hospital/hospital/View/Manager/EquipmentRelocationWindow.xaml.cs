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
                    MessageBox.Show("Invalid input for duration!");
                }
            }
        }

        private void Schedule_Relocation_Click(object sender, RoutedEventArgs e)
        {
            Room fromRoomSelected = roomController.FindRoomByName(fromRoom.SelectedItem.ToString());
            Room toRoomSelected = roomController.FindRoomByName(toRoom.SelectedItem.ToString());
            string equip = equipment.SelectedItem.ToString();
            TimeInterval relocation = (TimeInterval)relocationListView.SelectedItem;
            string id = scheduledRelocationController.FindAll().Count.ToString();
            try
            {
                ValidateQuantity();
                ScheduledRelocation scheduledRelocation = new ScheduledRelocation(id, fromRoomSelected, toRoomSelected, equip, Int32.Parse(quantity.Text), relocation);
                scheduledRelocationController.Create(scheduledRelocation);
                MoveEquipmentFromOriginalRoom(fromRoomSelected, equip, Int32.Parse(quantity.Text));
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Relocation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MoveEquipmentFromOriginalRoom(Room fromRoomSelected, string equip, int equipmentQuantity) {
            foreach (Equipment eq in fromRoomSelected.equipment)
            {
                if (eq.type.Equals(equip))
                {
                    try
                    {
                        ValidateQuantityInput(eq, equipmentQuantity);
                        SubstractQuantity(eq, equipmentQuantity, fromRoomSelected);
                    }
                    catch(Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SubstractQuantity(Equipment eq, int equipmentQuantity, Room fromRoomSelected) {
            eq.quantity -= equipmentQuantity;
            if (eq.quantity == 0)
            {
                List<Equipment> equipTemp = fromRoomSelected.equipment.ToList();
                equipTemp.Remove(eq);
                fromRoomSelected.equipment = new List<Equipment>(equipTemp);
            }
        }

        private void ValidateQuantityInput(Equipment eq, int equipmentQuantity) {
            if (eq.quantity < equipmentQuantity)
            {
                throw new Exception("Room doesn't have enough equipment!");
            }
        }

        private void ValidateQuantity()
        {
            int value;
            bool isValid = Int32.TryParse(quantity.Text, out value);

            if (!isValid)
                throw new Exception("Quantity should be a number!");

            if (value < 0)
                throw new Exception("Quantity should be positive!");
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

        private void equipmentChosen(object sender, SelectionChangedEventArgs e)
        {
            if (equipment.SelectedItem == null) return;
            
            foreach (Room room in roomController.FindAll())
            {
                if (room._Name.Equals(fromRoom.SelectedItem.ToString()))
                {
                    DisplayRoomEquipment(room);
                }
            }
            ValidateInputs();
        }

        private void DisplayRoomEquipment(Room room) {
            foreach (Equipment eq in room.equipment)
            {
                if (eq.type.Equals(equipment.SelectedItem.ToString()))
                    quantity.Text = eq.quantity.ToString();
            }
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
