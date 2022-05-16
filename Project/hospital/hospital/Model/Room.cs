using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model
{
    public class Room: INotifyPropertyChanged
    {
        public Room(string name, string purpose, int floor, string id)
        {
            this.name = name;
            this.purpose = purpose;
            this.floor = floor;
            this.id = id;
            equipment = new List<Equipment>();
        }

        public string _Name { 
            get
            {
                return name;
            }
            set 
            {
                name = value;
                OnPropertyChanged();
            } 
        }

        public string _Purpose
        {
            get
            {
                return purpose;
            }

            set
            {
                purpose = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged(string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string name;
        public string purpose;
        public int floor { get; set; }
        public string id { get; set; }
        public List<Equipment> equipment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddEquipment(Equipment equipment) {
            foreach (Equipment e in this.equipment) {
                if (e.type.Equals(equipment.type)) {
                    e.quantity += equipment.quantity;
                    return;
                }
            }
            this.equipment.Add(equipment);
        }


        public override string ToString()
        {
            return _Name;
        }
    }
}