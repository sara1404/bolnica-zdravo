using Controller;
using hospital.Controller;
using hospital.Model;
using hospital.View.Manager;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for BasicRenovationWindow.xaml
    /// </summary>
    public partial class BasicRenovationWindow : Window
    {
        private RoomController roomController;
        private ScheduledBasicRenovationController scheduledBasicRenovationController;
        private Timer timer;
        private bool timer_Elapsed = false;

        public BasicRenovationWindow()
        {
            App app = Application.Current as App;
            roomController = app.roomController;
            scheduledBasicRenovationController = app.scheduledBasicRenovationController;
            InitializeComponent();
            loadRooms();
            ScheduleBtn.IsEnabled = false;
        }

        public void loadRooms() {
            foreach (Room room in roomController.FindAll()) {
                roomRenovationListView.Items.Add(room._Name);
            }
        }

        private void Show_Appointments_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                ListAppointments();
            }
           
        }

        private void ListAppointments() {
            int renovationDuration = 0;
            try
            {
                renovationDuration = Int32.Parse(durationRenovation.Text);
            }
            catch (Exception ex)
            {
                durationRenovation.Foreground = Brushes.Red;
            }
            try
            {
                if (roomRenovationListView.SelectedItem == null) throw new Exception();
                Room room = roomController.FindRoomByName(roomRenovationListView.SelectedItem.ToString());
                renovationListView.ItemsSource = scheduledBasicRenovationController.FindFreeTimeIntervals(room, renovationDuration);
            }
            catch (Exception ex)
            {
                roomRenovationListView.Foreground = Brushes.Red;
            }
        }

        private void Schedule_Renovation_Click(object sender, RoutedEventArgs e)
        {
            SaveRenovation();
        }

        private void SaveRenovation() {
            Room room = roomController.FindRoomByName(roomRenovationListView.SelectedItem.ToString());
            TimeInterval interval = (TimeInterval)renovationListView.SelectedItem;
            string description = descriptionInput.Text;
            ScheduledBasicRenovation renovation = new ScheduledBasicRenovation(scheduledBasicRenovationController.FindAll().Count.ToString(), room, interval, description);
            scheduledBasicRenovationController.Create(renovation);
            this.Close();
        }

        private void FocusOnSaveButton()
        {
            ScheduleBtn.BorderThickness = new Thickness(5, 5, 5, 5);
            ScheduleBtn.BorderBrush = Brushes.Pink;
        }

        private void Cancel_Renovation_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Validate(object sender, SelectionChangedEventArgs e)
        {
            if (roomRenovationListView.SelectedIndex != -1 && renovationListView.SelectedIndex != -1 && descriptionInput.Text != "")
            {
                ScheduleBtn.IsEnabled = true;
                return;
            }
            ScheduleBtn.IsEnabled = false;
        }

        private void Validate(object sender, TextChangedEventArgs e)
        {
            if (roomRenovationListView.SelectedIndex != -1 && renovationListView.SelectedIndex != -1 && descriptionInput.Text != "")
            {
                ScheduleBtn.IsEnabled = true;
                return;
            }
            ScheduleBtn.IsEnabled = false;
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Start_Demo(object sender, RoutedEventArgs e)
        {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new SelectFromListBoxCommand(roomRenovationListView, 1),
                new FillTxtFieldCommand(durationRenovation, "0"),
                new ActionExecuteCommand(ListAppointments),
                new SelectFromListBoxCommand(renovationListView, 0),
                new FillTxtFieldCommand(descriptionInput, "k"),
                new FillTxtFieldCommand(descriptionInput, "kr"),
                new FillTxtFieldCommand(descriptionInput, "kre"),
                new FillTxtFieldCommand(descriptionInput, "krec"),
                new FillTxtFieldCommand(descriptionInput, "krece"),
                new FillTxtFieldCommand(descriptionInput, "krecen"),
                new FillTxtFieldCommand(descriptionInput, "krecenj"),
                new FillTxtFieldCommand(descriptionInput, "krecenje"),
                new ActionExecuteCommand(FocusOnSaveButton),
                new ActionExecuteCommand(SaveRenovation),

            };

            timer = new Timer((Object o) => TimerCallback(commands), null, 0,  1000);
        }
        private void TimerCallback(List<IDemoCommand> commands)
        {
            if (commands.Count == 0) return;
            commands[0].execute();
            commands.RemoveAt(0);
        }


    }
}
