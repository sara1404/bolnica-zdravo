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
        private Room room;
        public EditRoomWindow(Room room) 
        {
            InitializeComponent();
            App app = Application.Current as App;
            this.room = room;
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
            //var viewRoomsWindow = Application.Current.Windows.OfType<ManagerRoomsWindow>().FirstOrDefault();


            
            try
            {
                if (!Int32.TryParse(roomFloor.Text, out int res))
                {
                    MessageBox.Show("Not valid input for floor", "Error");
                    return;
                }
                Room newRoom = new Room(roomName.Text, roomPurpose.Text, res, roomId.Text);

                //roomController.DeleteById(room.id);
                //roomController.Create(newRoom);
                roomController.UpdateById(room.id, newRoom);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancel_Edit_Room_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
