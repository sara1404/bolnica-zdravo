using Controller;
using hospital.View;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace hospital.VM
{

    public class DeleteRoomWindowViewModel
    {

        private RoomController roomController;
        public ICommand DeleteRoomCommand => new DeleteRoomCommand(this);
        public ICommand CancelDeleteRoomCommand => new CancelDeleteRoomCommand();
        private Room room;

        public DeleteRoomWindowViewModel(Room room) {
            App app = Application.Current as App;
            roomController = app.roomController;
            this.room = room;
        }

        public void DeleteRoom()
        {
            //var viewRoomsWindow = Application.Current.Windows.OfType<ManagerRoomsWindow>().FirstOrDefault();
            //Room room = (Room)viewRoomsWindow.dataGridRooms.SelectedItem;
            try
            {
                roomController.DeleteById(room.id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }

    public class DeleteRoomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private DeleteRoomWindowViewModel deleteRoomWindowViewModel;

        public DeleteRoomCommand(DeleteRoomWindowViewModel vm)
        {
            deleteRoomWindowViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            deleteRoomWindowViewModel.DeleteRoom();
            new CancelDeleteRoomCommand().Execute("");
        }
    }

    public class CancelDeleteRoomCommand : ICommand
    {
        private DeleteRoomWindow window;
        public event EventHandler CanExecuteChanged;

        public CancelDeleteRoomCommand()
        {
            RetrieveDeleteRoomWindow();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            window.Close();
        }

        private void RetrieveDeleteRoomWindow()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(DeleteRoomWindow))
                {
                    window = ((DeleteRoomWindow)win);
                }
            }
        }
    }


}
