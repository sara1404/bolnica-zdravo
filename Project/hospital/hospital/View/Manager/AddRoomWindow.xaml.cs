using Controller;
using Model;
using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using hospital.VM;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
        {
            this.DataContext = new AddRoomWindowViewModel();
            InitializeComponent();
        }
    }
}
