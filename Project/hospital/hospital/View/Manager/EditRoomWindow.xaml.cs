using Controller;
using hospital.VM;
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
    public partial class EditRoomWindow : Window
    {
        public EditRoomWindow(Room room) 
        {
            this.DataContext = new EditRoomWindowViewModel(room);
            InitializeComponent();
        }
    }
}
