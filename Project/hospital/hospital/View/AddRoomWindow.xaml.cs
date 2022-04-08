using Controller;
using Model;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        
        private RoomController roomController;




        public AddRoomWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            roomController = app.roomController;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_New_Room_Click(object sender, RoutedEventArgs e)
        {
            string name = roomName.Text;
            string id = roomId.Text;
            string purpose = roomPurpose.Text;
            string floor = roomFloor.Text;
            Room newRoom = new Room(name, purpose, Int32.Parse(floor), id);
            roomController.Create(newRoom);
        }


        private void Cancel_New_Room_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
