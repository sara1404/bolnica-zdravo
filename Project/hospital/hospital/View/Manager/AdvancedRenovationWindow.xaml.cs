﻿using Controller;
using hospital.Controller;
using hospital.Model;
using hospital.View.Manager;
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
    /// Interaction logic for AdvancedRenovationWindow.xaml
    /// </summary>
    public partial class AdvancedRenovationWindow : Window
    {
        private RoomController roomController;
        private ScheduledAdvancedRenovationController scheduledAdvancedRenovationController;
        public AdvancedRenovationWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            roomController = app.roomController;
            scheduledAdvancedRenovationController = app.scheduledAdvancedRenovationController;
            listViewRooms.ItemsSource = roomController.FindAll();
            scheduleBtn.IsEnabled = false;
        }

        private void Show_Appointments_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int renovationDuration = Int32.Parse(durationRenovation.Text);
                try
                {
                    CheckTypeOfAdvancedRenovation(renovationDuration);
                }
                catch (Exception ex)
                {
                    DisplayError();
                }
            }
        }

        private void CheckTypeOfAdvancedRenovation(int renovationDuration)
        {
            if (listViewRooms.SelectedItems.Count >= 2)
                ListIntervalsWhenMerge(renovationDuration);
            else
                ListIntervalsWhenSplit(renovationDuration);
        }

        private void ListIntervalsWhenMerge(int renovationDuration)
        {
            List<Room> rooms = new List<Room>();
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForMergingRooms(rooms, renovationDuration);
        }

        private void ListIntervalsWhenSplit(int renovationDuration)
        {
            Room room = (Room)listViewRooms.SelectedItem;
            renovationListView.ItemsSource = scheduledAdvancedRenovationController.FindIntervalsForSplitingRoom(room, renovationDuration);
        }

        private void DisplayError()
        {
            durationRenovation.Text = "Invalid input";
            durationRenovation.Foreground = Brushes.Red;
        }

        private void Close_Window(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Schedule_Advanced_Renovation(object sender, RoutedEventArgs e)
        {
            if (mergeBtn.IsChecked == true)
            {
                MergeRoomsWindow mergeRoomsWindow = new MergeRoomsWindow();
                mergeRoomsWindow.floor.Text = ((Room)listViewRooms.SelectedItems[0]).floor.ToString();
                if (mergeRoomsWindow.ShowDialog() == false && IsMergeFormFilled(mergeRoomsWindow.room))
                {
                    CreateMergeRenovation(new List<Room>(), (TimeInterval)renovationListView.SelectedItem, mergeRoomsWindow.room);
                }

            }
            else if (splitBtn.IsChecked == true) {
                SplitRoomWindow splitRoomWindow = new SplitRoomWindow();
                splitRoomWindow.floor.Text = (((Room)listViewRooms.SelectedItems[0]).floor.ToString());
                if (splitRoomWindow.ShowDialog() == false && IsSplitFormFilled(splitRoomWindow.rooms)) {
                    Console.WriteLine((TimeInterval)renovationListView.SelectedItem);
                    CreateSplitRenovation(splitRoomWindow.rooms, (TimeInterval)renovationListView.SelectedItem);
                }
            }
        }

        private bool IsMergeFormFilled(Room room)
        {
            if (room == null) return false;
            if (room._Name == null || room.id == null || room._Purpose == null)
            {
                return false;
            }
            return true;
        }

        private bool IsSplitFormFilled(List<Room> rooms)
        {
            if (rooms == null  || rooms[0] == null || rooms[1] == null ) return false;
            if (rooms[0]._Name == null || rooms[0].id == null || rooms[0]._Purpose == null || rooms[1]._Name == null || rooms[1].id == null || rooms[1]._Purpose == null)
            {
                return false;
            }
            return true;
        }

        //private void Schedule_Advanced_Renovation(object sender, RoutedEventArgs e)
        //{
        //    List<Room> rooms = new List<Room>();
        //    TimeInterval interval = (TimeInterval)renovationListView.SelectedItem;

        //    if (listViewRooms.SelectedItems.Count >= 2)
        //        ScheduleMergingRooms(rooms, interval);
        //    else
        //        ScheduleSplitingRoom(rooms, interval);
        //}

        //private void ScheduleMergingRooms(List<Room> rooms, TimeInterval interval)
        //{
        //    try
        //    {
        //        ValidateId(newCode.Text);
        //        ValidateFloor();
        //        CreateMergeRenovation(rooms, interval);
        //        Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void CreateMergeRenovation(List<Room> rooms, TimeInterval interval, Room resultRoom)
        {
            rooms.Add((Room)listViewRooms.SelectedItems[0]);
            rooms.Add((Room)listViewRooms.SelectedItems[1]);
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(GenerateId().ToString(),
                resultRoom, interval, description.Text, rooms, "merge");
            scheduledAdvancedRenovationController.Create(newRenovation);
        }


        //private void ScheduleSplitingRoom(List<Room> rooms, TimeInterval interval)
        //{
        //    try
        //    {
        //        ValidateId(newCode.Text.Split(',')[0]);
        //        ValidateId(newCode.Text.Split(',')[1]);
        //        ValidateFloor();
        //        CreateSplitRenovation(rooms, interval);
        //        this.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void CreateSplitRenovation(List<Room> rooms, TimeInterval interval)
        {
            Room room = (Room)listViewRooms.SelectedItem;
            ScheduledAdvancedRenovation newRenovation = new ScheduledAdvancedRenovation(GenerateId().ToString(),
                room, interval, description.Text, rooms, "split");
            scheduledAdvancedRenovationController.Create(newRenovation);
        }

        private int GenerateId()
        {
            if (scheduledAdvancedRenovationController.FindAll().Count == 0)
                return 0;
            return Int32.Parse(scheduledAdvancedRenovationController.FindAll()[scheduledAdvancedRenovationController.FindAll().Count - 1]._Id) + 1;
        }

        //private void CheckRoomFloor(List<Room> rooms, Room room)
        //{
        //    if (rooms[0].floor != rooms[1].floor || rooms[0].floor != room.floor)
        //        throw new Exception("Rooms must be on the same floor!");
        //}

        private void ValidateId(string id)
        {
            if (roomController.FindRoomById(id) != null)
                throw new Exception("Room with this id already exists!");
        }

        //private void ValidateFloor()
        //{
        //    int value;
        //    bool isValid = Int32.TryParse(floor.Text, out value);

        //    if (!isValid)
        //        throw new Exception("Floor should be a number!");

        //    if (value < 0)
        //        throw new Exception("Floor should be positive!");
        //}


        private void IsFormFilled(object sender, SelectionChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilled(object sender, TextChangedEventArgs e)
        {
            IsFormFilledValidation();
        }

        private void IsFormFilledValidation()
        {
            if (listViewRooms.SelectedItems.Count != 0 && renovationListView.SelectedItems.Count != 0 && description.Text != "" && (mergeBtn.IsChecked == true || splitBtn.IsChecked == true))
            {
                scheduleBtn.IsEnabled = true;
                return;
            }
            scheduleBtn.IsEnabled = false;
        }

        private void IsFormFiled(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsFormFilledValidation();
        }
    }
    }