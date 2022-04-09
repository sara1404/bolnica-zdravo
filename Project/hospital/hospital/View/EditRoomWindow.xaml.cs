using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    /// 
    public partial class EditRoomWindow : Window, INotifyPropertyChanged
    {
        private RoomController roomController;

        public EditRoomWindow() 
        {
            InitializeComponent();
            App app = Application.Current as App;
            roomController = app.roomController;

        }

        public event PropertyChangedEventHandler PropertyChanged;
       


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Edit_Room_Click(object sender, RoutedEventArgs e)
        {
            var viewRoomsWindow = Application.Current.Windows.OfType<ManagerRoomsWindow>().FirstOrDefault();
            Room room = (Room)viewRoomsWindow.dataGridRooms.SelectedItem;
            String oldId = room.id;
            room.id = roomId.Text;
            room.name = roomName.Text;
            room.purpose = roomPurpose.Text;
            room.floor = Int32.Parse(roomFloor.Text);
            try
            {
                roomController.DeleteById(oldId);
                roomController.Create(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warehouse cant be edited", "Error");
            }
            this.Close();
        }
    }
}
