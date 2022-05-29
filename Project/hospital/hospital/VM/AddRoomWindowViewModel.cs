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
    public class AddRoomWindowViewModel
    {
        public ICommand AddRoomCommand => new AddRoomCommand(this);
        public ICommand CancelRoomCommand => new CancelRoomCommand();

        public string name { get; set; }
        public string id { get; set; }
        public string purpose { get;  set; }
        public string floor { get; set; }

        private RoomController roomController;

        public AddRoomWindowViewModel() {
            App app = Application.Current as App;
            roomController = app.roomController;
        }
        public void AddNewRoom()
        {
            Room newRoom = new Room(name, purpose, Int32.Parse(floor), id);
            try
            {
                roomController.Create(newRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }

    public class AddRoomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private AddRoomWindowViewModel addRoomWindowViewModel;

        public AddRoomCommand(AddRoomWindowViewModel vm) {
            addRoomWindowViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            addRoomWindowViewModel.AddNewRoom();
            new CancelRoomCommand().Execute("");
        }
    }

    public class CancelRoomCommand : ICommand
    {
        private AddRoomWindow window;
        public event EventHandler CanExecuteChanged;

        public CancelRoomCommand() {
            RetrieveAddRoomWindow();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            window.Close();
        }

        private void RetrieveAddRoomWindow()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(AddRoomWindow))
                {
                    window = ((AddRoomWindow)win);
                }
            }
        }
    }

}
