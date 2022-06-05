using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public interface IRoomRepository
    {
        void Create(Room room);
        Room FindById(string id );
        Room FindByName(string name);
        Room FindByPurpose(string purpose);
        List<Room> FindRoomsByPurpose(string purpose);
        ObservableCollection<Room> FindRoomsByEquipmentType(string type);
        ObservableCollection<Room> FindRoomsByEquipmentQuantity(int quantity);
        ObservableCollection<Room> FindRoomsByEquipmentTypeAndQuantity(string type, int quantity);
        ref ObservableCollection<Room> FindAll();
        bool UpdateById(string id, Room room);
        bool DeleteById(string id);
        void LoadRoomData();
        void WriteRoomData();
    }
}
