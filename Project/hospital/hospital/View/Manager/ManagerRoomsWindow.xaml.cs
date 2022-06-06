using Controller;
using hospital.VM;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerRoomsWindow.xaml
    /// </summary>
    public partial class ManagerRoomsWindow : Window
    {

        public ManagerRoomsWindow()
        {
            this.DataContext = new RoomWindowViewModel();
            InitializeComponent();
        }
    }
}
