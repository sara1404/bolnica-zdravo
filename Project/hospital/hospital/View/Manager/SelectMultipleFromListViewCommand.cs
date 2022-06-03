using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class SelectMultipleFromListViewCommand : IDemoCommand
    {
        private ListView roomListView;
        private RoomController roomController;

        public SelectMultipleFromListViewCommand(ListView roomListView, RoomController roomController)
        {
            this.roomListView = roomListView;
            this.roomController = roomController;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate {
                roomListView.SelectedItems.Add(roomController.FindAll()[10]);
                roomListView.SelectedItems.Add(roomController.FindAll()[11]);
            });
        }
    }
}
