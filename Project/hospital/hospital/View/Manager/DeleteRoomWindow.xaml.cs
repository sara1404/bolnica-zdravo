using Controller;
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
    /// Interaction logic for DeleteRoomWindow.xaml
    /// </summary>
    public partial class DeleteRoomWindow : Window
    {
        private RoomController roomController;
        public DeleteRoomWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            roomController = app.roomController;
        }

        private void Delete_Room_Click(object sender, RoutedEventArgs e)
        {
            var viewRoomsWindow = Application.Current.Windows.OfType<ManagerRoomsWindow>().FirstOrDefault();
            Room room = (Room)viewRoomsWindow.dataGridRooms.SelectedItem;
            try
            {
                roomController.DeleteById(room.id);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
            this.Close();
        }

        private void Cancel_Delete_Room(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
