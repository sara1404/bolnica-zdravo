using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerRoomsWindow.xaml
    /// </summary>
    public partial class ManagerRoomsWindow : Window
    {

        //public ObservableCollection<Room> rooms { get; set; }
        private RoomController roomController;

        public ManagerRoomsWindow()
        {
            Console.WriteLine("otvorili prozor");
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            roomController = app.roomController;
            dataGridRooms.ItemsSource =  roomController.FindAll();
        }

        private void Add_Room_Click(object sender, RoutedEventArgs e)
        {
            new AddRoomWindow().Show();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            int selectedRow = dataGridRooms.SelectedIndex;
            Console.WriteLine(selectedRow);
            if(e.Key == Key.Enter && selectedRow != -1)
            {
                new EditRoomWindow().Show();
            }
        }
    }
}
